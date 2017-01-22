using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitBeforeLeave : MonoBehaviour {

    [SerializeField]
    private string nom_scene;
    [SerializeField]
    private float wait;

	// Use this for initialization
	void Start () {
        StartCoroutine("WaitSomeSeconds");
	}

    IEnumerator WaitSomeSeconds()
    {
        yield return new WaitForSeconds(wait);
        SceneManager.LoadScene(nom_scene);
    }
}
