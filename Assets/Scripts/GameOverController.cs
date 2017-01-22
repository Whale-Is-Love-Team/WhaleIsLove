using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour {

    [SerializeField]
    private UnityEngine.UI.Text textGui = null;
    [SerializeField]
    private GameObject[] states = null;
    [SerializeField]
    private UnityEngine.UI.InputField inputField = null;


    private int cur = 0;
    private bool blockState = false;

    // Use this for initialization
    void Start () {
        // textGui = this.GetComponent<UnityEngine.UI.Text>();
        for (int n = 0; n < states.Length; n++) states[n].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (cur < states.Length)
        {
            if (states[cur] != null) states[cur].SetActive(true);
            if (cur > 0 && states[cur - 1] != null) states[cur - 1].SetActive(false);

            if (cur == 0 && textGui != null && GameManager.Instance != null)
            {
                textGui.text = "SCORE: " + GameManager.Instance.Score.ToString();
                checkButtons();
            }
            else if (cur == 1)
            {
                if (Input.GetKeyUp("return") && inputField != null && !inputField.text.Equals(""))
                {
                    GameManager.Instance.scoreList.list.Add(new KeyValuePair<string, int>(inputField.text, GameManager.Instance.Score));
                    foreach (var pair in GameManager.Instance.scoreList.list) Debug.Log(pair.Key + " " + pair.Value);
                    GameManager.Instance.Score = 0;
                    UnityEngine.SceneManagement.SceneManager.LoadScene("highscores");
                }
            }
        }
    }

    void checkButtons()
    {
        if (Input.GetKeyUp("space") /*|| Input.GetButton("XboxA")*/) cur++;
    }
}
