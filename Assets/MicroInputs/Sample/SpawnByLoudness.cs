using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnByLoudness : MonoBehaviour {

    public GameObject audioInputObject;
    public float threshold = 1.0f;
    public GameObject objectToSpawn;
    MicroHandler micIn;
    void Start() {
        if (objectToSpawn == null)
            Debug.LogError("You need to set a prefab to Object To Spawn -parameter in the editor!");
        if (audioInputObject == null)
            audioInputObject = gameObject;
        micIn = gameObject.GetComponent<MicroHandler>();
    }

    void Update() {
        float p = micIn.pitch;
        float l = micIn.loudness;
        if(l > threshold) {
            if (p < 200) {
                Debug.Log("Tir court");
            }
            else if (p > 200) {
                Debug.Log("Tir long");
            }
        }
    }
}
