using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeoutController : MonoBehaviour {

    [SerializeField]
    private float lifetime = 0;
    private float endtime = 0;

	// Use this for initialization
	void Start () {
        endtime = Time.time + lifetime;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > endtime)
        {
            Destroy(gameObject);
        }
	}
}
