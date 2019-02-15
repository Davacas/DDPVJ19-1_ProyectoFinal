using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StoreManager : MonoBehaviour {
    //enum Armas { Pistola, Ak47 };
    public GameObject storePanel;
    private PlayerController player;

    void Start() {
        player = PlayerController.instance;
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            player.shopping = true;
            storePanel.SetActive(true);
            Cursor.visible = true;
        }
    }
    
    public void BuyAK() {
        if (player.currentMoney >= 30) {
            player.currentMoney -= 30;
            //player.inventory[Armas.Ak47].inInventory = true;
            player.inventory[1].inInventory = true;
        }
    }

    public void BuyLife() {
        if (player.currentMoney >= 1) {
            player.currentLife = player.maxLife;
        }
    }

    public void BuyShield() {
        if (player.currentMoney >= 2) {
            player.currentShield = player.maxShield;
        }
    }

    public void BuySpeed() {
        if (player.currentMoney >= 2) {
            StartCoroutine(ReturnSpeed());
        }
    }

    public void BuyPistolBullets() {
        if (player.currentMoney >= 1) {
            player.inventory[0].currentClips += 1;
        }
    }

    public void BuyAKBullets() {
        if (player.currentMoney >= 3) {
            player.inventory[1].currentClips += 1;
        }
    }

    public void CloseShop() {
        storePanel.SetActive(false);
        Cursor.visible = false;
        player.shopping = false;
    }

    IEnumerator ReturnSpeed() {
        yield return new WaitForSeconds(60.0f);
        player.speed /= 2;
    }
}
