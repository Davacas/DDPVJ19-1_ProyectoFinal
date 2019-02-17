using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //Modelos, cámaras y animaciones
    public static PlayerController instance;
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

    //Propiedades del personaje.
    public int maxLife;
    public int currentLife;
    public int maxShield;
    public int currentShield;
    public int currentMoney;
    public bool shopping;

    void Awake () {
        shopping = false;
        instance = this;
        jugador = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        cuerpo = GetComponent<Rigidbody>();
        camara = Camera.main;
        
        //Inicializando variables propiedades del jugador.
        maxLife = 100;
        currentLife = maxLife;
        maxShield = 100;
        currentShield = maxShield;
        currentWeapon = 0;
        currentMoney = 0;
        HUDManager.instance.setMoney(currentMoney);

        alive = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (alive && !shopping) {
            Move();
            Rotar();

            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                inventory[currentWeapon].Shoot();
            }
            if (Input.GetKey(KeyCode.Mouse0) && inventory[currentWeapon].isAuto) {
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

    //MODIFICAR ESTE MÉTODO AL AGREGAR MÁS ARMAS.
    void ChangeWeapon() {
        Debug.Log("Cambio de arma.");
        if (currentWeapon == inventory.Length-1) { //Si es la última arma, se cambia a la primera.
            inventory[currentWeapon].Hide();
            currentWeapon = 0;
            inventory[currentWeapon].Withdraw();
        }
        else if (inventory[currentWeapon + 1].inInventory) {
            inventory[currentWeapon].Hide();
            currentWeapon++;
            inventory[currentWeapon].Withdraw();
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
        inventory[currentWeapon].gameObject.SetActive(false);
        camara.GetComponent<Animator>().SetTrigger("Die");
        GameManager.instance.ShowDeathScreen();
    }
}
