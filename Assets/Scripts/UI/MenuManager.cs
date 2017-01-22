using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject menu;
    public GameObject credits;

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
        SceneManager.LoadScene("calibrage");
    }

    public void Credits_OnClick()
    {
        menu.SetActive(false);
        credits.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("QUIT");
            Application.Quit();
        }
        if (Input.anyKeyDown)
        {
            credits.SetActive(false);
            menu.SetActive(true);
        }
    }
}
