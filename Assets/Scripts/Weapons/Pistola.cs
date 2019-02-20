using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : Arma { 
    void Start() {
        inInventory = true;
        isAuto = false;
        damage = 1;
        maxAmmo = 12;
        currentAmmo = maxAmmo;
        currentClips = 5;
        HUDManager.instance.setClips(currentClips);
        reloadTime = 2.0f;
        smoke = GetComponentInChildren<ParticleSystem>();
    }
}
