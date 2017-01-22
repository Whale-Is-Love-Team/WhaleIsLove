using System.Collections;
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
    public GameObject spaceBar;
    public float threshold = 1f;
    public float recordTime = 5f;

    protected MicroHandler _mic;
    protected List<int> _values;
    protected float _recordStopAt;
    protected int _currentPitch = 0;
    protected bool _lowPitchSaved = false;
    protected bool _calibrated = false;
    protected Image _picto;

	void Start () {
        _mic = MicroHandler.Instance;
        _values = new List<int>();
        _picto = image.GetComponent<Image>();
        _recordStopAt = Time.realtimeSinceStartup + recordTime;
        image.SetActive(true);
        _picto.sprite = spr_whale;
	}
	
	void Update () {
        if (_calibrated) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene("Menu");
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
        pitchText.text = "See the narwhal? Give a high pitch!";
        _currentPitch = 0;
        _values.Clear();
        _recordStopAt = Time.realtimeSinceStartup + recordTime;
        _lowPitchSaved = true;
        _picto.sprite = spr_narwhal;
    }

    protected void RecordHighPitch() {
        GameManager.Instance.hightPitch = _currentPitch;
        pitchText.text = "Whale scream calibrated. Thank you!";
        spaceBar.SetActive(true);
        pitchValue.gameObject.SetActive(false);
        _calibrated = true;
        image.SetActive(false);
        if (GameManager.Instance.hightPitch <= GameManager.Instance.lowPitch) GameManager.Instance.hightPitch = GameManager.Instance.lowPitch + 150;
    }
}
