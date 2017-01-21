using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour {

    [SerializeField]
    private UnityEngine.UI.Text textGui = null;

	// Use this for initialization
	void Start () {
        GameManager.Instance.Score = 2051;
        textGui = this.GetComponent<UnityEngine.UI.Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (textGui != null) textGui.text = GameManager.Instance.Score.ToString();
    }
}
