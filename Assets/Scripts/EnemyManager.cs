using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    //Variables para instanciado de enemigos.
    public GameObject alien;
    public GameObject alienRapido;
    public GameObject[] spawnPositions;
    public static EnemyManager instance;

    //Variables sobre spawneo de enemigos
    private float probAlienRapido;
    public int currentEnemies;
    private int maxEnemies;

    void Awake() {
        instance = this;
        maxEnemies = 20;
        probAlienRapido = 0.75f;
        InvokeRepeating("SpawnAlien", 0.0f, 20.0f);
    }

    void SpawnAlien() {
        //Si no se ha llegado al máximo de enemigos, se instancía otro.
        if (currentEnemies < maxEnemies) {
            if (Random.Range(0.0f, 1.0f) <= probAlienRapido) {
                Instantiate(alienRapido, spawnPositions[Random.Range(0, 5)].transform);
            }
            else {
                var enemigo = Instantiate(alien, spawnPositions[Random.Range(0, 5)].transform);
            }
        }
        currentEnemies ++;
    }
}
