using UnityEngine;

public class GetTesseract : MonoBehaviour {
    private AudioSource tessAudio;
    public AudioClip tessGrab;
    private MeshRenderer model;
    private BoxCollider tessCollider;
    private Light tessLight;

    void Start() {
        tessAudio = GetComponent<AudioSource>();
        tessCollider = GetComponent<BoxCollider>();
        model = GetComponent<MeshRenderer>();
        tessLight = GetComponentInChildren<Light>();
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            tessAudio.PlayOneShot(tessGrab);
            tessCollider.enabled = false;
            model.enabled = false;
            tessLight.enabled = false;
            Destroy(this.gameObject, 1.0f);
            HUDManager.instance.setMoney(++PlayerController.instance.currentMoney);
        }
    }
}
