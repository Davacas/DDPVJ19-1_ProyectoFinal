using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47 : Arma{
    void Start() {
        inInventory = true;
        isAuto = true;
        damage = 2;
        maxAmmo = 30;
        currentAmmo = maxAmmo;
        currentClips = 5;
        HUDManager.instance.setClips(currentClips);
        reloadTime = 3.0f;
        smoke = GetComponentInChildren<ParticleSystem>();
    }
}
