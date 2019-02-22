using UnityEngine;

public class Pistola : Arma { 
    void Start() {
        inInventory = true;
        isAuto = false;
        damage = 1;
        maxAmmo = 12;
        currentAmmo = maxAmmo;
        currentClips = 5;
        HUDManager.instance.setAmmoLevel(currentAmmo, maxAmmo);
        HUDManager.instance.setClips(currentClips);
        reloadTime = 2.0f;
        smoke = GetComponentInChildren<ParticleSystem>();
    }
}
