﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PitchCalib : MonoBehaviour {

    public Text pitchValue;
    public Text pitchText;
    public GameObject image;
    public Sprite spr_whale;
    public Sprite spr_narwhal;
    public float threshold = 1f;
    public float recordTime = 5f;

    protected MicroHandler _mic;
    protected List<int> _values;
    protected float _recordStopAt;
    protected int _currentPitch = 0;
    protected bool _lowPitchSaved = false;
    protected bool _calibrated = false;
    protected SpriteRenderer _renderer;

	void Start () {
        _mic = MicroHandler.Instance;
        _values = new List<int>();
        _renderer = image.GetComponent<SpriteRenderer>();
        _recordStopAt = Time.realtimeSinceStartup + recordTime;
        image.SetActive(true);
        _renderer.sprite = spr_whale;
	}
	
	void Update () {
        if (_calibrated) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene("main");
            }

            return;
        }

        if(Time.realtimeSinceStartup > _recordStopAt) {
            if (!_lowPitchSaved)
                RecordLowPitch();
            else
                RecordHighPitch();
        }

        if (_mic.loudness > threshold)
            _values.Add(Mathf.RoundToInt(_mic.pitch));

        _values.Sort(delegate (int pitchA, int pitchB) {
            if (pitchA < pitchB) return -1;
            else if (pitchA > pitchB) return 1;
            else return 0;
        });


        if (_values.Count > 0) {
           _currentPitch = _values[Mathf.FloorToInt(_values.Count / 2)];
           pitchValue.text = _currentPitch + " Hz";
        }
	}

    protected void RecordLowPitch() {
        GameManager.Instance.lowPitch = _currentPitch;
        pitchText.text = "See the narwhal ?\nGive a high pitch !";
        _currentPitch = 0;
        _values.Clear();
        _recordStopAt = Time.realtimeSinceStartup + recordTime;
        _lowPitchSaved = true;
        _renderer.sprite = spr_narwhal;
    }

    protected void RecordHighPitch() {
        GameManager.Instance.hightPitch = _currentPitch;
        pitchText.text = "Whale scream calibrated.\nThank you !\nPress Spacebar to continue;";
        _calibrated = true;
        image.SetActive(false);
    }
}