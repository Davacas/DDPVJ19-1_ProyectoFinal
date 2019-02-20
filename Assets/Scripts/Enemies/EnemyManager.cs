using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    //Variables para instanciado de enemigos.
    public GameObject alien;
    public GameObject alienRapido;
    public GameObject[] spawnPositions;
    public static EnemyManager instance;
    public float spawnTime;

    //Variables sobre spawneo de enemigos
    private float probAlienRapido;
    public int currentEnemies;
    public int kills;
    private int maxEnemies;

    void Awake() {
        instance = this;
        kills = 0;
        maxEnemies = 20;
        probAlienRapido = 0.75f;
        spawnTime = 20.0f;
        InvokeRepeating("SpawnAlien", 0.0f, spawnTime);
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

    public void IncreaseSpawn() {
        spawnTime = 5.0f;
        CancelInvoke();
        InvokeRepeating("SpawnAlien", 0.0f, spawnTime);
    }
}
