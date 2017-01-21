using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour {

    [SerializeField]
    protected GameObject[] hearts;
    [SerializeField]
    protected PlayerHealth pHealth;

	void Update () {
		for(var i = 0; i < hearts.Length; i++) {
            if(pHealth.Life > i) {
                hearts[i].GetComponent<LifeHeart>().isEmpty = false;
            }
            else
                hearts[i].GetComponent<LifeHeart>().isEmpty = true;
        }
    }
}
