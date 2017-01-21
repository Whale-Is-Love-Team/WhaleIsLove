using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour {

    [SerializeField]
    protected GameObject[] hearts;

    public int life = 3;

	void Update () {
		for(var i = 0; i < hearts.Length; i++) {
            if(life > i) {
                hearts[i].GetComponent<LifeHeart>().isEmpty = false;
            }
            else
                hearts[i].GetComponent<LifeHeart>().isEmpty = true;
        }
    }
}
