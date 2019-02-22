using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FastEnemyController : MonoBehaviour {
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

    //Materiales
    public Renderer rend;
    public Material fadeMaterial;
    private bool fade;
    private float currentTime;
    private float fadeTime;

    void Start() {
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAgent.Warp(EnemyManager.instance.spawnPositions[Random.Range(0, 5)].transform.position);
        enemyAgent.enabled = true;
        enemyAnimator = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        alive = true;
        fade = false;
        currentTime = 0.0f;
        fadeTime = 3.0f;
        currentLife = 3;
    }

    void Update() {
        //Si el enemigo sigue vivo, busca o ataca.
        if (alive) {
            if (Physics.CheckSphere(transform.position + transform.forward * 3 + transform.up, 2.0f, 1 << 11)) {
                Atacar();
            }
            else if (Physics.CheckCapsule(transform.position + transform.up,
                transform.forward * 10 + transform.forward * 3 + transform.up, 2.0f, 1 << 11)) {
                JumpAttack();
            }
            else {
                MoveTowardsPlayer();
            }
        }
        if (fade) {
            Fade();
        }
    }

    void MoveTowardsPlayer() {
        enemyAnimator.SetFloat("Speed", enemyAgent.velocity.magnitude);
        enemyAgent.SetDestination(player.transform.position);
    }
    
    //Métodos que ejecutan efectos visuales y auditivos del ataque. 
    void JumpAttack() {
        if (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("JumpAttack")) {
            GetComponent<Rigidbody>().AddForce(transform.up*200 + transform.forward*200, ForceMode.Impulse);
            if (!enemyAudio.isPlaying) enemyAudio.PlayOneShot(attackAudio);
            enemyAnimator.SetFloat("Speed", -1);
            enemyAnimator.SetTrigger("JumpAttack");
        }
    }

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
        if (alive) {
            if (Physics.CheckSphere(transform.position + transform.forward * 3 + transform.up, 2.0f, 1 << 11)) {
                player.SendMessage("TakeDamage", "fast");
            }
            enemyAgent.isStopped = false;
        }
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

    void Fade() {
        rend.material = fadeMaterial;
        if (currentTime <= fadeTime) {
            currentTime += Time.deltaTime;
            rend.material.SetFloat("_FadeValue", Mathf.Lerp(0.0f, 1.0f, currentTime / fadeTime));
        }
    }

    void Morir() {
        if (alive) {
            alive = false;
            enemyAgent.isStopped = true;
            enemyAnimator.SetFloat("Speed", -1);
            enemyAnimator.SetTrigger("Die");
            EnemyManager.instance.currentEnemies--;
            EnemyManager.instance.kills++;
        }
        StartCoroutine(StartFade());
        TesseractManager.instance.SpawnTesseract(transform.position);
        Destroy(this.gameObject, 3.0f);
    }

    IEnumerator StartFade() {
        yield return new WaitForSeconds(1.0f);
        fade = true;
    }
}
