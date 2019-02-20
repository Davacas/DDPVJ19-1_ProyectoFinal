using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    //public GameObject pauseScreen;
    public GameObject deathScreen;
    private AudioSource gameAudio;
    public AudioClip hover;
    public AudioClip clic;

    //Variables para transición de salida.
    public Image exitPanel;
    public Color startColor;
    public Color endColor;
    private float timeLeft = 3.0f;
    public static GameManager instance;
    public bool exit;
    public bool dead;

    //Variables para puntaje
    public GameObject scoreScreen;
    public TextMeshProUGUI stateText;
    public TextMeshProUGUI objectivesText;
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI timeText;
    public float gameTime;
    
    void Awake() {
        gameAudio = GetComponent<AudioSource>();
        gameTime = 0.0f;
        instance = this;
        exit = false;
    }

    void Update() {
        if (!exit) {
            gameTime += Time.deltaTime;
        }
        else {
            gameTime += 0;
            FadeScreen();
        }
    }

    void FadeScreen() {
        if (exit) {
            timeLeft -= Time.deltaTime;
            timeLeft /= 3;
            exitPanel.color = Color.Lerp(startColor, endColor, timeLeft);
        }
        if (timeLeft <= 0) {
            exit = false;
            StartCoroutine(ShowResults());
        }
    }

    public void ShowDeathScreen() {
        deathScreen.SetActive(true);
    }

    public void LoadMenu() {
        gameAudio.PlayOneShot(clic);
        MainMenu.instance.LoadMenu();
    }

    public void LoadGame() {
        gameAudio.PlayOneShot(clic);
        MainMenu.instance.LoadMenu();
        MainMenu.instance.LoadSinglePlayer();
    }

    public void Pause() {

    }

    public void Hover() {
        gameAudio.PlayOneShot(hover);
    }

    IEnumerator ShowResults() {
        yield return new WaitForSeconds(1.0f);

        float minutes = Mathf.Floor(gameTime / 60);
        int seconds = Mathf.RoundToInt(gameTime % 60);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        AudioManager.instance.SetSFXVolume(-80.0f);
        AudioManager.instance.SetMusicVolume(-80.0f);
        scoreScreen.gameObject.SetActive(true);
        if (dead) {
            stateText.SetText("Muerto");
            objectivesText.SetText(ObjectivesManager.instance.objectivesCompleted.ToString());
        }
        else if (ObjectivesManager.instance.objectivesCompleted < 7 && !dead) {
            stateText.SetText("Escapó");
            objectivesText.SetText(ObjectivesManager.instance.objectivesCompleted.ToString());
        }
        else {
            stateText.SetText("¡Héroe!");
            objectivesText.SetText("¡Todos!");
        }
        if (minutes < 10 && seconds < 10) {
            timeText.SetText("0" + minutes +":0"+seconds);
        }
        else if (minutes < 10 && seconds >= 10) {
            timeText.SetText("0" + minutes + ":" + seconds);
        }
        else if (minutes >= 10 && seconds < 10) {
            timeText.SetText(minutes + ":0" + seconds);
        }
        else if (minutes >= 10 && seconds >= 10) {
            timeText.SetText(minutes + ":" + seconds);
        }
        killsText.SetText(EnemyManager.instance.kills.ToString());
    }
}
