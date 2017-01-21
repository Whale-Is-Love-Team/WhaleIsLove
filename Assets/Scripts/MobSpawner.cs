using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject[] waves;                // The enemy prefab to be spawned.
    [SerializeField]
    private float spawnTime;            // How long between each spawn.

    // Use this for initialization
    void Start () {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        // Find a random index between zero and one less than the number of spawn points.
        int wave = Random.Range(0, waves.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(waves[wave], this.transform.position, this.transform.rotation);
    }
}
