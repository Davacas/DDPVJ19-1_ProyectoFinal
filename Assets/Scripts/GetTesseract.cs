using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTesseract : MonoBehaviour {
    private AudioSource tessAudio;
    public AudioClip tessGrab;
    private MeshRenderer model;
    private BoxCollider tessCollider;

    void Start() {
        tessAudio = GetComponent<AudioSource>();
        tessCollider = GetComponent<BoxCollider>();
        model = GetComponent<MeshRenderer>();
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            tessAudio.PlayOneShot(tessGrab);
            tessCollider.enabled = false;
            model.enabled = false;
            Destroy(this.gameObject, 3.0f);
            HUDManager.instance.setMoney(++PlayerController.instance.currentMoney);
        }
    }
}
