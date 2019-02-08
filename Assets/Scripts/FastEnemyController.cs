using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FastEnemyController : MonoBehaviour {
    //Para seguir y atacar al jugador
    private NavMeshAgent enemyAgent;
    private GameObject player;
    private RaycastHit VisionHit;

    //Manejo de audio
    private AudioSource enemyAudio;
    public AudioClip attackAudio;

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
        currentLife = 3;
    }

    void Update() {
        //Si el enemigo sigue vivo, busca o ataca.
        if (alive) {
            if (Physics.CheckSphere(transform.position + transform.forward * 3 + transform.up, 2.0f, 1 << 11)) {
                Atacar();
                Debug.Log("AtaqueNormal");
            }
            else if (Physics.CheckCapsule(transform.position + transform.up,
                transform.forward * 10 + transform.forward * 3 + transform.up, 2.0f, 1 << 11)) {
                JumpAttack();
                Debug.Log("AtaqueSalto");
            }
            else {
                MoveTowardsPlayer();
            }
        }
    }

    void MoveTowardsPlayer() {
        enemyAnimator.SetFloat("Speed", enemyAgent.velocity.magnitude);
        enemyAgent.SetDestination(player.transform.position);
    }
    
    //Métodos que ejecutan efectos visuales y auditivos del ataque. 
    void JumpAttack() {
        if (alive && !enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("JumpAttack")) {
            GetComponent<Rigidbody>().AddForce(transform.up*200 + transform.forward*200, ForceMode.Impulse);
            if (!enemyAudio.isPlaying) enemyAudio.PlayOneShot(attackAudio);
            enemyAnimator.SetFloat("Speed", -1);
            enemyAnimator.SetTrigger("JumpAttack");
        }
    }

    void Atacar() {
        enemyAgent.isStopped = true;
        if (alive && !enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
            if (!enemyAudio.isPlaying) enemyAudio.PlayOneShot(attackAudio);
            enemyAnimator.SetFloat("Speed", -1);
            enemyAnimator.SetTrigger("Attack");
        }
    }
    //Método lanzado en un frame específico de la animación. Hace la lógica del ataque.
    void HitPlayer() {
        if (Physics.CheckSphere(transform.position + transform.forward * 3 + transform.up, 2.0f, 1 << 11)) {
            player.SendMessage("TakeDamage");
        }
        enemyAgent.isStopped = false;
    }

    void TakeDamage() {
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
            Debug.Log("Enemigo muerto.");
            enemyAnimator.SetTrigger("Die");
            EnemyManager.instance.currentEnemies--;
        }
        alive = false;
        Destroy(this.gameObject, 3.0f);
    }
}
