using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectivesManager : MonoBehaviour {
    public TextMeshProUGUI objDesc;
    public static ObjectivesManager instance;

    void Start() {
        instance = this;
        StartObjective1();
    }

    public void StartObjective1() {
        objDesc.SetText("Ir con Don Rata y comprar un multímetro.");
    }

    public void EndObjective1() {
        StartObjective2();
    }

    public void StartObjective2() {
        objDesc.SetText("Aún no sé qué más hacer.");
    }
}
