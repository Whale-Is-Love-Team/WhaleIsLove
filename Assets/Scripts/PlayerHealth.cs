﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    protected int life;
    [SerializeField]
    protected int invulFrameCount = 12;

    protected int _frameCounter;
    protected bool _invulnerability = false;

    public int Life {
        get { return life; }
    }

    public void RecieveDamage(int damages) {
        var gm = GameManager.Instance;
        if (!gm.Running)
            return;

        if (_invulnerability)
            return;

        this.GetComponent<Animator>().SetTrigger("hit");
        StartCoroutine("Red");
        _invulnerability = true;
        _frameCounter = 0;

        life -= damages;
        gm.ResetCombo();
        if(life <= 0) {
            gm.Running = false;  
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }

    public void Update() {
        if (_invulnerability) {
            if (_frameCounter >= invulFrameCount)
                _invulnerability = false;
            else
                _frameCounter++;
        }

        if(life <= 0) {
            GameManager.Instance.Running = false;
        }
    }

    IEnumerator Red()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, f, f, 1); ;
            yield return new WaitForSeconds(.01f);
        }
        for (float f = 0; f <= 1f; f += 0.1f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, f, f, 1); ;
            yield return new WaitForSeconds(.01f);
        }
    }
}
