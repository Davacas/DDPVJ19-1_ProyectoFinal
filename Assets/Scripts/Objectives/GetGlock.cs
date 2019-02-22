using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGlock : MonoBehaviour {
    private MeshRenderer[] model;
    private BoxCollider glockCollider;
    private Light glockLight;
    public static GetGlock instance;
    public GameObject ammoPanel;
    private PlayerController player;

    void Start() {
        ammoPanel.SetActive(false);
        glockCollider = GetComponent<BoxCollider>();
        model = GetComponentsInChildren<MeshRenderer>();
        glockLight = GetComponentInChildren<Light>();
        instance = this;
        player = PlayerController.instance;
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player" && ObjectivesManager.instance.currentObjective == 1) {
            glockCollider.enabled = false;
            foreach (var mesh in model) {
                mesh.enabled = false;
            }
            player.inventory[0].Withdraw();
            glockLight.enabled = false;
            ammoPanel.SetActive(true);
            Destroy(this.gameObject, 1.0f);
            ObjectivesManager.instance.StartObjective2();
        }
    }
}
