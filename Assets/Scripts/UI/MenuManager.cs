using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public void Jouer_OnClick()
    {
        SceneManager.LoadScene("main");
    }

    public void Quitter_OnClick()
    {
        Application.Quit();
    }

    public void Options_OnClick()
    {

    }

    public void Credits_OnClick()
    {

    }
}
