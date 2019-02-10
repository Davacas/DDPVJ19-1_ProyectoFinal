using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Arma : MonoBehaviour {
    //Atributos del arma.
    protected int damage;
    protected int maxAmmo;
    protected int currentAmmo;
    protected bool inInventory;

    //Efectos visuales.
    public Animator animacion;
    public GameObject modelo;
    public GameObject muzzleFlash;
    public GameObject blood;
    public GameObject debris;
    public ParticleSystem smoke;

    //Efectos de sonido.
    protected AudioSource audioSource;
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip[] ricochet;

    //Ayuda para lógica de disparos
    private Camera camara;
    private RaycastHit hit;

    void Awake() {
        camara = Camera.main;
        audioSource = GetComponent<AudioSource>();
    }

    public abstract void Shoot();

    public abstract void Reload();

    //Detectar si se disparó a un enemigo o al escenario.
    protected void DetectHit() {
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
            var debrisInstance = Instantiate(debris, hit.point, hit.transform.localRotation);
            debrisInstance.transform.LookAt(camara.transform);
            debrisInstance.transform.GetComponent<ParticleSystem>().Play();
            Destroy(debrisInstance, 0.5f);
        }
    }

    protected void ShowFlash() {
        muzzleFlash.transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        muzzleFlash.SetActive(true);
        StartCoroutine(HideFlash());
    }

    IEnumerator PlayRicochetSound() {
        yield return new WaitForSeconds(0.2f);
        audioSource.PlayOneShot(ricochet[Random.Range(0, 4)]);
    }

    IEnumerator HideFlash() {
        yield return new WaitForSeconds(0.1f);
        muzzleFlash.SetActive(false);
    }
}
