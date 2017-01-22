using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour {

    [System.Serializable]
    public class Wave
    {
        public GameObject wave;
        public float cooldown;
    }

    [SerializeField]
    private Wave[] waves;

    // Use this for initialization
    void Start () {
        StartCoroutine("SpawnWave");
    }

    IEnumerator SpawnWave()
    {
        while (GameManager.Instance.Running){
            // Find a random index between zero and one less than the number of spawn points.
            int wave = Random.Range(0, waves.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate(waves[wave].wave, this.transform.position, this.transform.rotation);

            yield return new WaitForSeconds(waves[wave].cooldown);
        }
    }
}
