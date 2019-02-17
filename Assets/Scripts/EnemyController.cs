using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    //Para seguir y atacar al jugador
    private NavMeshAgent enemyAgent;
    private GameObject player;
    private RaycastHit VisionHit;
    public GameObject tesseract;

    //Manejo de audio
    private AudioSource enemyAudio;
    public AudioClip attackAudio;
    public AudioClip[] damageAudio;

    //Propiedades del enemigo
    private Animator enemyAnimator;
    private float currentLife;
    private bool alive;

    void Start() {
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAgent.Warp(EnemyManager.instance.spawnPositions[Random.Range(0, 5)].transform.position);
        enemyAgent.enabled = true;
        enemyAnimator = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        alive = true;
        currentLife = 5;
    }

    void Update() {
        //Si el enemigo sigue vivo, busca o ataca.
        if (alive) {
            if (Physics.CheckSphere(transform.position + transform.forward * 3 + transform.up, 2.0f, 1 << 11)) {
                Atacar();
            }
            else {
                MoveTowardsPlayer();
            }
        }
    }

    void OnDrawGizmos () {
        Gizmos.DrawWireSphere(transform.position + transform.forward*3 + transform.up, 2.0f);
    }

    void MoveTowardsPlayer() {
        enemyAnimator.SetFloat("Speed", enemyAgent.velocity.magnitude);
        enemyAgent.SetDestination(player.transform.position);
    }
    
    //Método que ejecuta efectos visuales y auditivos del ataque. 
    void Atacar() {
        enemyAgent.isStopped = true;
        if (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
            if (!enemyAudio.isPlaying) enemyAudio.PlayOneShot(attackAudio);
            enemyAnimator.SetFloat("Speed", -1);
            enemyAnimator.SetTrigger("Attack");
        }
    }
    //Método lanzado en un frame específico de la animación. Hace la lógica del ataque.
    void HitPlayer() {
        if (Physics.CheckSphere(transform.position + transform.forward * 3 + transform.up, 2.0f, 1 << 11)) {
            player.SendMessage("TakeDamage", "normal");
        }
        enemyAgent.isStopped = false;
    }

    void TakeDamage() {
        enemyAudio.PlayOneShot(damageAudio[Random.Range(0, damageAudio.Length)]);
        enemyAnimator.SetFloat("Speed", -1);
        if (alive) {
            currentLife -= 1;
            if (currentLife <= 0) {
                Morir();
            }
            else {
                enemyAnimator.SetTrigger("TakeDamage");
            }
        }
    }

    void Morir() {
        if (alive) {
            alive = false;
            enemyAgent.isStopped = true;
            enemyAnimator.SetFloat("Speed", -1);
            enemyAnimator.SetTrigger("Die");
            EnemyManager.instance.currentEnemies--;
        }
        TesseractManager.instance.SpawnTesseract(transform.position);
        Destroy(this.gameObject, 3.0f);
    }
}
