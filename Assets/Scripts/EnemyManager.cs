using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    public GameObject alien;
    public GameObject alienRapido;
    public GameObject[] spawnPositions;

    void Start() {

    }
    
    void Update() {
        InvokeRepeating("SpawnAlien", 0, 10);
    }

    void SpawnAlien() {
        if (Random.Range(0, 1) >= 0.5) {
            Instantiate(alien, spawnPositions[Random.Range(0, 1)].transform);
        }
        else {
            Instantiate(alienRapido, spawnPositions[Random.Range(0, 1)].transform);
        }
    }
}
