using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Arma : MonoBehaviour {
    //Atributos del arma.
    protected int damage;
    protected int maxAmmo;
    protected int currentAmmo;
    public int currentClips;
    protected float reloadTime;
    protected float fireRate;
    public bool inInventory;
    public bool isAuto;
    public bool reloading;

    //Efectos visuales.
    public Animator animacion;
    public GameObject modelo;
    public GameObject muzzleFlash;
    public GameObject blood;
    public GameObject debris;
    public GameObject bulletHole;
    public ParticleSystem smoke;
    
    //Efectos de sonido.
    protected AudioSource audioSource;
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip emptySound;
    public AudioClip[] ricochet;

    //Ayuda para lógica de disparos
    private Camera camara;
    private RaycastHit hit;

    void Awake() {
        camara = Camera.main;
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot() {
        if ((currentAmmo <= 0) && (!reloading) && (currentClips > 0) && (currentAmmo < maxAmmo)) {
            Reload();
        }
        else if (!animacion.GetCurrentAnimatorStateInfo(0).IsName("Shooting") && !reloading && currentClips >= 0 && currentAmmo > 0) {
            animacion.SetTrigger("Shoot");
            PlayerController.instance.transform.Rotate(0, Random.Range(-1.0f, 1.0f), 0);
            camara.transform.Rotate(Random.Range(-1.0f, 1.0f), 0, 0);
            ShowFlash();
            smoke.Play();
            audioSource.PlayOneShot(shotSound);
            currentAmmo--;
            DetectHit();
        }
        else {
            audioSource.PlayOneShot(emptySound);
        }
        HUDManager.instance.setAmmoLevel(currentAmmo, maxAmmo);
    }

    public void Reload() {
        reloading = true;
        if (!animacion.GetCurrentAnimatorStateInfo(0).IsName("Reload") && (currentClips > 0)) {
            animacion.SetTrigger("Reload");
            audioSource.PlayOneShot(reloadSound);
            StartCoroutine(FillAmmo());
        }
    }

    public void Withdraw() {
        gameObject.SetActive(true);
        //animacion.SetTrigger("Withdraw");
        HUDManager.instance.setAmmoLevel(currentAmmo, maxAmmo);
        HUDManager.instance.setClips(currentClips);
    }

    public void Hide() {
        reloading = false;
        //animacion.SetTrigger("Hide");
        //StartCoroutine(Disable());
        gameObject.SetActive(false);
    }

    //Detectar si se disparó a un enemigo o al escenario.
    void DetectHit() {
        //Se detecta si se le dió a un enemigo.
        if (Physics.Raycast(camara.transform.position, camara.transform.forward, out hit, 500, 1 << 9)) { //La capa 9 es la de enemigos.
            hit.transform.SendMessage("TakeDamage");
            var bloodInstance = Instantiate(blood, hit.point, hit.transform.localRotation);
            bloodInstance.transform.LookAt(camara.transform);
            bloodInstance.transform.GetComponent<ParticleSystem>().Play();
            Destroy(bloodInstance, 0.5f);
        }
        //Se detecta si se le dió al escenario.
        else if (Physics.Raycast(camara.transform.position, camara.transform.forward, out hit, 500, 1 << 10)) { //La capa 10 es la del escenario.
            StartCoroutine(PlayRicochetSound());
            var bulletHoleInstance = Instantiate(bulletHole, hit.point + hit.normal*0.01f, Quaternion.FromToRotation(Vector3.up, hit.normal));
            var debrisInstance = Instantiate(debris, hit.point, hit.transform.localRotation);
            debrisInstance.transform.LookAt(camara.transform);
            debrisInstance.transform.GetComponent<ParticleSystem>().Play();
            Destroy(bulletHoleInstance, 10.0f);
            Destroy(debrisInstance, 0.5f);
        }
    }
    
    void ShowFlash() {
        muzzleFlash.transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        muzzleFlash.SetActive(true);
        StartCoroutine(HideFlash());
    }

    IEnumerator PlayRicochetSound() {
        yield return new WaitForSeconds(0.2f);
        audioSource.PlayOneShot(ricochet[Random.Range(0, 4)]);
    }

    IEnumerator HideFlash() {
        yield return new WaitForSeconds(0.05f);
        muzzleFlash.SetActive(false);
    }

    IEnumerator FillAmmo() {
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        currentClips--;
        HUDManager.instance.setAmmoLevel(currentAmmo, maxAmmo);
        HUDManager.instance.setClips(currentClips);
        reloading = false;
    }

    IEnumerator Disable() {
        yield return new WaitForSeconds(0.0f);
        gameObject.SetActive(false);
    }
}
