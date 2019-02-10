using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : Arma { 
    void Start() {
        inInventory = true;
        damage = 1;
        maxAmmo = 12;
        currentAmmo = maxAmmo;
        smoke = GetComponentInChildren<ParticleSystem>();
        //animacion = GetComponentInChildren<Animator>();
    }

    public override void Shoot() {
        if (currentAmmo <= 0) {
            Reload();
        }
        else if (!animacion.GetCurrentAnimatorStateInfo(0).IsName("Disparo")) {
            animacion.SetTrigger("Disparo");
            ShowFlash();
            smoke.Play();
            audioSource.PlayOneShot(shotSound);
            currentAmmo--;
            DetectHit();            
        }
        HUDManager.instance.setAmmoLevel(currentAmmo, maxAmmo);
    }   

    public override void Reload() {
        currentAmmo = maxAmmo;
        //animacion.SetTrigger("Reload");
        audioSource.PlayOneShot(reloadSound);
        HUDManager.instance.setAmmoLevel(currentAmmo, maxAmmo);
    }
}
