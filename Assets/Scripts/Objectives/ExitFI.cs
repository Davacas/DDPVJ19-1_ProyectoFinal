using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExitFI : MonoBehaviour {
    public TextMeshProUGUI interactText;

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            interactText.gameObject.SetActive(true);
            interactText.SetText("Presiona <b>'E'</b> para salir de la facultad.");
        }
    }

    void OnTriggerStay(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (ObjectivesManager.instance.currentObjective == 7) {
                    ObjectivesManager.instance.EndGame();
                }
                PlayerController.instance.alive = false;
                GameManager.instance.exit = true;
                ObjectivesManager.instance.StopAllCoroutines();
            }
        }
    }

    void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            interactText.gameObject.SetActive(false);
        }
    }


}
