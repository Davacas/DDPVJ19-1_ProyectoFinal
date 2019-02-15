using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTesseract : MonoBehaviour {
    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            Destroy(this.gameObject);
            HUDManager.instance.setMoney(++PlayerController.instance.currentMoney);
        }
    }
}
