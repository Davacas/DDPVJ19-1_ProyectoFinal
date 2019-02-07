using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //Modelos, cámaras y animaciones
    private Animator animacion;
    private Camera camara;
    private CharacterController jugador;
    public GameObject modelo;
    private Rigidbody cuerpo;

    //Controladores de movimiento del personaje.
    public float speed, speedRot;
    Vector3 movimiento;
    bool firstjump;
    float rotX;

    //Relacionados con disparos
    private RaycastHit hit;
    public GameObject muzzleFlash;

    //Manejo de audio
    private AudioSource audioSource;
    public AudioClip disparo;

    //Manejo de HUD
    public HUDBarsManager barsController;

    //Barras del HUD
    private int maxLife;
    private int currentLife;
    private int maxShield;
    private int currentShield;
    private int maxAmmo;
    private int currentAmmo;

    void Start () {
        jugador = GetComponent<CharacterController>();
        cuerpo = GetComponent<Rigidbody>();
        camara = Camera.main;
        animacion = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        //Inicializando variables para manejo de HUD.
        maxLife = 100;
        currentLife = maxLife;
        maxShield = 100;
        currentShield = maxShield;
        maxAmmo = 12;
        currentAmmo = maxAmmo;
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        Rotar();
        
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Disparar();
        }
        
    }

    void Move() {
        movimiento = Vector3.zero;
        movimiento.x = Input.GetAxis("Horizontal") * speed;
        movimiento.z = Input.GetAxis("Vertical") * speed;
        jugador.Move(transform.TransformDirection(movimiento));
    }

    void Rotar() {
        transform.Rotate(0, Input.GetAxis("Mouse X") * speedRot, 0);

        rotX = Input.GetAxis("Mouse Y");
        if (rotX > 0 && (camara.transform.localEulerAngles.x <= 90 ||camara.transform.localEulerAngles.x > 290)) {
            camara.transform.Rotate(-rotX * speedRot, 0, 0);
        }
        else if (rotX < 0 && (camara.transform.localEulerAngles.x < 80 || camara.transform.localEulerAngles.x >= 270)) {
            camara.transform.Rotate(-rotX * speedRot, 0, 0);
        }        
    }

    void TakeDamage() {
        if (currentShield > 0) {
            currentShield -= 10;
            barsController.setShieldLevel(currentShield, maxShield);
        }
        else {
            currentLife -= 10;
            barsController.setLifeLevel(currentLife, maxLife);
        }
        if (currentLife <= 0) {
            Morir();
        }
    }

    void Disparar() {
        animacion.SetTrigger("Disparo");
        if (!animacion.GetCurrentAnimatorStateInfo(0).IsName("Disparo")) {
            barsController.setAmmoLevel(--currentAmmo, maxAmmo);
            DetectHit();
            audioSource.PlayOneShot(disparo);
            muzzleFlash.transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
            muzzleFlash.SetActive(true);
            StartCoroutine(HideFlash());
        }
    }

    void Morir() {
        Debug.Log("Te moriste :(");
    }

    IEnumerator HideFlash() {
        yield return new WaitForSeconds(0.1f);
        muzzleFlash.SetActive(false);
    }

    //Detectar si se disparó a un enemigo
    void DetectHit() {
        if (Physics.Raycast(camara.transform.position, camara.transform.forward, out hit, 500, 1<<9)) { //La capa 9 es la de enemigos.
            hit.transform.SendMessage("TakeDamage");
        }
    }

    
}
