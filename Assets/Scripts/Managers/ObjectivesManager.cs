using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectivesManager : MonoBehaviour {
    private AudioSource objectivesAudio;
    public AudioClip completedAudio;
    public TextMeshProUGUI objDesc;
    public static ObjectivesManager instance;
    public int objectivesCompleted;
    public int currentObjective;
    
    public GameObject []objects;
    public Vector3[] pos;
    float timeLeft = 300.0f;

    void Start() {
        objectivesAudio = GetComponent<AudioSource>();
        currentObjective = 0;
        instance = this;
        StartObjective1();
        SpawnObjects(objects);
    }

    void Update () {
        if (currentObjective == 6) {
            timeLeft -= Time.deltaTime;
            objDesc.SetText("Derrota alienígenas por " + (int)timeLeft + " segundos mientras el personal evacúa la facultad.");
        }
    }
    
    void SpawnObjects(GameObject[] objects) {
        //Método que pone objetos de misión en posiciones diferentes entre sí.
        List<GameObject> insts = new List<GameObject>();
        foreach (var obj in objects) {
            insts.Add((GameObject) Instantiate(obj, pos[Random.Range(0, pos.Length)], Quaternion.identity));
        }
        foreach (var obj in insts) {
            foreach (var objCmp in insts) {
                if (obj != objCmp && obj.transform.position == objCmp.transform.position) {
                    objCmp.transform.position = pos[Random.Range(0, pos.Length)];
                }
            }
        }
    }

    public void EnableObjective(string texto) {
        currentObjective++;
        objectivesCompleted = currentObjective - 1;
        objDesc.SetText(texto);
        if (objectivesCompleted > 0) {
            objectivesAudio.PlayOneShot(completedAudio);
        }
    }

    public void StartObjective1() {
        EnableObjective("Conseguir un libro de circuitos eléctricos. Seguro hay alguno tirado en alguna parte de esta facultad.");
    }    

    public void StartObjective2() {
        EnableObjective("Ir con Don Rata y comprar un multímetro.");
    }

    public void StartObjective3() {
        EnableObjective("Ir al panel eléctrico de la facultad y analizarlo.");
    }

    public void StartObjective4() {
        EnableObjective("Conseguir un fusible. Seguro hay alguno tirado en alguna parte de esta facultad.");
    }    

    public void StartObjective5() {
        EnableObjective("Ir al panel eléctrico de la facultad y repararlo.");
    }

    public void StartObjective6() {
        EnableObjective("Derrota alienígenas por 300 segundos mientras el personal evacúa la facultad.");
        StartCoroutine(killAliens());
        EnemyManager.instance.IncreaseSpawn();
    }

    public void StartObjective7() {
        EnableObjective("Escapa de la facultad por alguna de sus salidas.");
    }

    IEnumerator killAliens() {
        yield return new WaitForSeconds(5.0f);
        StartObjective7();
    }

    public void EndGame() {
        EnableObjective("¡Objetivos completados!");
    }
}
