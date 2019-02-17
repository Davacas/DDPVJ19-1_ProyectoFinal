using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour {
    public Image lifeBar;
    public Image shieldBar;
    public Image shieldFrame;
    public Image ammoBar;
    public Image crosshair;
    public TextMeshProUGUI clipsText;
    public TextMeshProUGUI moneyText;
    public static HUDManager instance;
    public Color enemyAimColor;
    public Color envAimColor;

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

    public void setHighCrosshairAlpha() {
        crosshair.color = enemyAimColor;
    }

    public void setLowCrosshairAlpha() {
        crosshair.color = envAimColor;
    }

    public void setAmmoLevel(int ammo, int maxAmmo) {
        ammoBar.fillAmount = (float)ammo / (float)maxAmmo;
    }

    public void setClips(int clips) {
        clipsText.text = clips.ToString();
    }

    public void setMoney(int money) {
        moneyText.text = "= "+ money.ToString();
    }
}
