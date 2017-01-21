using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    protected Text _scoreDisplay;

	void Start () {
        _scoreDisplay = GetComponent<Text>();
	}
	
	void Update () {
        var currentScore = GameManager.Instance.Score.ToString();
        var currentCombo = GameManager.Instance.Combo;
        var scoreLength = currentScore.Length;
        if(scoreLength > 3) {
            currentScore = currentScore.Insert(scoreLength - 3, ",");
        }

        if(currentCombo > 1) {
            currentScore += "\nX";
            currentScore += currentCombo.ToString(); 
        }

        _scoreDisplay.text = currentScore;
	}
}
