using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Get the micro inputs, analyze them and expose
/// them for the rest of the gameplay
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class MicroHandler : MonoBehaviour {

    protected AudioSource _source;
    protected string _micName;
    protected int qSamples = 1024;
    protected float[] spectrum;
    protected float fSample;
    protected float refValue = 0.1f;
    protected float rmsValue;
    protected float dbValue;
    protected float[] samples;


    public float sensitivity = 100;
    public float loudness = 0;
    public float pitch = 0;
    public float threshold = 0.02f;


    void Start () {
        var mics = Microphone.devices;
        if (mics.Length > 0) {
            spectrum = new float[qSamples];
            samples = new float[qSamples];
            fSample = AudioSettings.outputSampleRate;

            _source = GetComponent<AudioSource>();
            _micName = mics[0];
            _source.clip = Microphone.Start(_micName, true, 10, 48000);
            _source.mute = false;
            _source.loop = true;
            while(!(Microphone.GetPosition(null) > 0)) { }
            _source.Play();
        }
	}

    void Update() {
        loudness = GetAveragedVolume() * sensitivity;
        pitch = GetPitch();
    }

    /// <summary>
    /// Get average volume from mic input
    /// </summary>
    float GetAveragedVolume() {
        float[] data = new float[256];
        float a = 0;
        _source.GetOutputData(data, 0);
        foreach (float s in data) {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }

    /// <summary>
    /// Return the current pitch in Hz from
    /// the mic input
    /// </summary>
    protected float GetPitch() {
        // Get raw data
        _source.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        float maxV = 0;
        int maxN = 0;
        for (var i = 0; i < qSamples; i++) { // find max 
            if (spectrum[i] > maxV && spectrum[i] > threshold) {
                maxV = spectrum[i];
                maxN = i; // maxN is the index of max
            }
        }
        float freqN = maxN; // pass the index to a float variable
        if (maxN > 0 && maxN < qSamples - 1) { // interpolate index using neighbours
            double dL = spectrum[maxN - 1] / spectrum[maxN];
            double dR = spectrum[maxN + 1] / spectrum[maxN];
            freqN += (float)(0.5 * (dR * dR - dL * dL));
        }
        return freqN * (fSample / 2) / qSamples;
    }
}
