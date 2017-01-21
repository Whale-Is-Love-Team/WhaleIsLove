using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    protected int life;

    public int Life {
        get { return life; }
    }

    public void RecieveDamage(int damages) {
        if (!GameManager.Instance.Running)
            return;

        life -= damages;
        if(life <= 0) {
            Debug.Log("Game Over");
            GameManager.Instance.Running = false;  
        }
    }

    public void Update() {
        if(life <= 0) {
            GameManager.Instance.Running = false;
        }
    }
}
