using UnityEngine;
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
                if (ObjectivesManager.instance.currentObjective == 8) {
                    ObjectivesManager.instance.EndGame();
                }
                PlayerController.instance.alive = false;    //Para que no se pueda mover ni ser atacado.
                GameManager.instance.playing = false;       //Para mostrar pantalla con resultados.
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
