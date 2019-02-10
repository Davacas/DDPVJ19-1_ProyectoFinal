using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
    public Image lifeBar;
    public Image shieldBar;
    public Image shieldFrame;
    public Image ammoBar;
    public static HUDManager instance;

    // Start is called before the first frame update
    void Awake() {
        instance = this;
    }

    public void setLifeLevel(int life, int maxLife) {
        lifeBar.fillAmount = (float)life / (float)maxLife;
    }

    public void setShieldLevel(int shield, int maxShield) {
        shieldBar.fillAmount = (float)shield / (float)maxShield;
        if (shieldBar.fillAmount <= 0) {
            shieldFrame.gameObject.SetActive(false);
        }
        else {
            shieldFrame.gameObject.SetActive(true);
        }
    }

    public void setAmmoLevel(int ammo, int maxAmmo) {
        ammoBar.fillAmount = (float)ammo / (float)maxAmmo;
    }

}
