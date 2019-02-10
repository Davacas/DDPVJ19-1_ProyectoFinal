using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    //Modelos, cámaras y animaciones
    private Camera camara;
    private CharacterController jugador;
    private Rigidbody cuerpo;

    //Controladores de movimiento del personaje.
    private bool alive;
    public float speed, speedRot;
    Vector3 movimiento;
    bool firstjump;
    float rotX;

    //Manejo de inventario
    public Arma[] inventory;
    private int currentWeapon;

    //Manejo de audio
    private AudioSource audioSource;
    public AudioClip[] damage;

    //Manejo de HUD
    private int maxLife;
    private int currentLife;
    private int maxShield;
    private int currentShield;

    void Start () {
        jugador = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        cuerpo = GetComponent<Rigidbody>();
        camara = Camera.main;
        
        //Inicializando variables para manejo de HUD.
        maxLife = 100;
        currentLife = maxLife;
        maxShield = 100;
        currentShield = maxShield;
        currentWeapon = 0;

        alive = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (alive) {
            Move();
            Rotar();

            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                inventory[currentWeapon].Shoot();
            }
            if (Input.GetKeyDown(KeyCode.R)) {
                inventory[currentWeapon].Reload();
            }
            if (Input.GetKeyDown(KeyCode.Tab)) {
                ChangeWeapon();
            }
        }        
    }

    void Move() {
        movimiento = Vector3.zero;
        movimiento.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        movimiento.z = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        jugador.Move(transform.TransformDirection(movimiento));
    }

    void Rotar() {
        transform.Rotate(0, Input.GetAxis("Mouse X") * speedRot * Time.deltaTime, 0);

        rotX = Input.GetAxis("Mouse Y");
        if (rotX > 0 && (camara.transform.localEulerAngles.x <= 90 ||camara.transform.localEulerAngles.x > 290)) {
            camara.transform.Rotate(-rotX * speedRot * Time.deltaTime, 0, 0);
        }
        else if (rotX < 0 && (camara.transform.localEulerAngles.x < 80 || camara.transform.localEulerAngles.x >= 270)) {
            camara.transform.Rotate(-rotX * speedRot * Time.deltaTime, 0, 0);
        }        
    }

    void ChangeWeapon() {
        //Si aún hay armas en el inventario, se cambia a la siguiente arma. Si no, se regresa a la primera.
        
        if (currentWeapon < inventory.Length - 1) {
            currentWeapon++;
        }
        else {
            currentWeapon = 0;
        }
    }

    void TakeDamage(string tipoEnemigo) {
        if (alive) {
            if (currentShield > 0) {
                if (tipoEnemigo == "normal") currentShield -= 10;
                else if (tipoEnemigo == "fast") currentShield -= 5;
                HUDManager.instance.setShieldLevel(currentShield, maxShield);
            }
            else {
                if (tipoEnemigo == "normal") currentLife -= 10;
                else if (tipoEnemigo == "fast") currentLife -= 5;
                HUDManager.instance.setLifeLevel(currentLife, maxLife);
            }
            audioSource.PlayOneShot(damage[Random.Range(0, 2)]);

            if (currentLife <= 0) {
                Morir();
            }
        }
    }

    void Morir() {
        alive = false;
        inventory[0].gameObject.SetActive(false);
        camara.transform.Rotate(90.0f, 0.0f, 90.0f);
        GameManager.instance.ShowDeathScreen();
    }
}
