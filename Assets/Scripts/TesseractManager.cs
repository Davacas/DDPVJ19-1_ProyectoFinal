using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesseractManager : MonoBehaviour {
    public static TesseractManager instance;
    public GameObject tesseract;
    
    void Start() {
        instance = this;
    }

    public void SpawnTesseract(Vector3 pos) {
        Instantiate(tesseract, pos + Vector3.up * 2, Quaternion.Euler(90f, 0f, 0f));
    }
}
