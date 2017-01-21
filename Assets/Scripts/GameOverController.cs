using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

public class GameOverController : MonoBehaviour {

    [SerializeField]
    private UnityEngine.UI.Text textGui = null;
    [SerializeField]
    private string filepath = "";
    [SerializeField]
    private int highscoresSize = 1;
    [SerializeField]
    private KeyValuePair<int, string>[] highscoreTable;

    // Use this for initialization
    void Start () {
        textGui = this.GetComponent<UnityEngine.UI.Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (textGui != null /* && GameManager.Instance != null */) textGui.text = "SCORE: " + GameManager.Instance.Score.ToString();
    }

    private KeyValuePair<int, string>[] getHighscores(string filename)
    {
        KeyValuePair<int, string>[] highscores = new KeyValuePair<int, string>[highscoresSize];
        for (int n = 0; n < highscoresSize; n++) highscores[n] = new KeyValuePair<int, string>(0, "");
        try
        {
            string line;
            int n = 0;
            StreamReader reader = new StreamReader(filename, Encoding.Default);
            using (reader)
            {
                do
                {
                    line = reader.ReadLine();
                    if (line != null)
                    {
                        string[] entries = line.Split('=');
                        if (entries.Length == 2 && Regex.IsMatch(entries[1], @"\d"))
                        {
                            highscores[n] = new KeyValuePair<int, string>(Convert.ToInt32(entries[1]), entries[0]);
                            n++;
                        }
                    }
                }
                while (line != null && n < highscoresSize);
                reader.Close();
                return highscores;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("{0}\n", e.Message);
            return null;
        }
        return null;
    }
}
