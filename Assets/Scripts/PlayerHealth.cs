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
        var gm = GameManager.Instance;
        if (!gm.Running)
            return;

        life -= damages;
        gm.ResetCombo();
        if(life <= 0) {
            Debug.Log("Game Over");
            gm.Running = false;  
        }
    }

    public void Update() {
        if(life <= 0) {
            GameManager.Instance.Running = false;
        }
    }
}
