using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    protected static GameManager s_Instance;
    public static GameManager Instance {
        get {
            return s_Instance;
        }
    }

    public void Awake() {
        if (s_Instance == null)
        {
            s_Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this);

        Running = true;
    }

    public bool Running { get; set; }
    public int Score { get; set; }
}
