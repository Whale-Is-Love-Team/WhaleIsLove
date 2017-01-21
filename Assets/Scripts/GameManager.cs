using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    protected static GameManager s_Instance;
    public static GameManager Instance {
        get {
            return s_Instance;
        }
    }

    protected bool _timeUpdatedThisTime = false;
    protected int _currentKillStep;
    protected int _comboIndex;
    protected float _comboLostAt;

    public void Awake() {
        if (s_Instance == null)
            s_Instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(gameObject);
        Running = true;
        Score = 0;

        _comboIndex = 0;
        _currentKillStep = 0;
    }

    public void Update() {
        if (!Running)
            return;

        var currentTime = Time.realtimeSinceStartup;
        var floorTime = Mathf.FloorToInt(currentTime);
        if (floorTime != 0 && floorTime % 10 == 0) {
            if(!_timeUpdatedThisTime) {
                Score += (int) (10 * Combo);
                _timeUpdatedThisTime = true;
            }
        }
        else
            _timeUpdatedThisTime = false;

        if(_currentKillStep > 0 && currentTime > _comboLostAt) {
            ResetCombo();
            Debug.Log("combo timeout");
        }

        if(_currentKillStep >= killStepsBeforeCombo[_comboIndex] && _comboIndex < killStepsBeforeCombo.Length - 1) {
            _comboIndex++;
            Debug.Log(_comboIndex);
        }
    }

    public int[] killStepsBeforeCombo;
    public float[] comboStep;
    public float comboTimeout = 1f;

    public int Score { get; set; }
    public float Combo {
        get {
            return comboStep[_comboIndex];
        }
    }
    public bool Running { get; set; }

    public void IncreaseKillCount() {
        _currentKillStep++;
        _comboLostAt = Time.realtimeSinceStartup + comboTimeout;
        Debug.Log(_currentKillStep);
    }

    public void ResetCombo() {
        _currentKillStep = 0;
        _comboIndex = 0;
    }
}
