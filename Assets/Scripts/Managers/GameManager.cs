using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    //Audio
    private AudioSource gameAudio;
    public AudioClip hover;
    public AudioClip clic;

    //Variables para transición de salida.
    public GameObject deathScreen;
    public Image fadePanel;
    public Color startColor;
    public Color endColor;
    private float fadeTime;
    public static GameManager instance;
    public bool playing;
    public bool dead;
    public bool inMenu;
    public bool watchingStory;

    //Variables para puntaje
    public GameObject scoreScreen;
    public TextMeshProUGUI stateText;
    public TextMeshProUGUI objectivesText;
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI timeText;
    public float gameTime;
    
    void Awake() {
        gameAudio = GetComponent<AudioSource>();
        DataManager.instance.GetData();
        fadeTime = 3.0f;
        instance = this;
        playing = false;
        watchingStory = true;
    }

    void Start() {
        StoryManager.instance.ShowStory();
    }

    void Update() {
        if (playing && !inMenu && !watchingStory) {
            gameTime += Time.deltaTime;
        }
        else if (!playing && !inMenu && !watchingStory) {
            gameTime += 0;
            ShowResults();
        }
    }

    public void FadeScreen(bool fading) {
        if (fading) {
            fadeTime -= Time.deltaTime;
            fadeTime /= 3;
            fadePanel.color = Color.Lerp(startColor, endColor, fadeTime);
        }
        if (fadeTime <= 0) {
            fading = false;
            playing = true;
        }
    }

    public void ShowDeathScreen() {
        deathScreen.SetActive(true);
    }

    public void LoadMenu() {
        gameAudio.PlayOneShot(clic);
        SceneManager.LoadScene(0);
        inMenu = true;
        playing = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadSinglePlayer() {
        gameAudio.PlayOneShot(clic);
        gameTime = 0.0f;
        playing = true;
        inMenu = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(1);
    }

    public void LoadMultiPlayer() {
        gameAudio.PlayOneShot(clic);
    }
    
    public void Hover() {
        gameAudio.PlayOneShot(hover);
    }

    public void Clic() {
        gameAudio.PlayOneShot(clic);
    }

    public void ExitGame() {
        FadeScreen(true);
        gameAudio.PlayOneShot(clic);
        Application.Quit();
    }

    void ShowResults() {
        FadeScreen(true);
        StartCoroutine(ShowResultsScreen());
    }

    IEnumerator ShowResultsScreen() {
        yield return new WaitForSeconds(1.0f);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        AudioManager.instance.SetSFXVolume(-80.0f);
        AudioManager.instance.SetMusicVolume(-80.0f);
        scoreScreen.gameObject.SetActive(true);
        if (dead) {
            stateText.SetText("Muerto");
            objectivesText.SetText(ObjectivesManager.instance.objectivesCompleted.ToString());
        }
        else if (ObjectivesManager.instance.objectivesCompleted < 8 && !dead) {
            stateText.SetText("Escapó");
            objectivesText.SetText(ObjectivesManager.instance.objectivesCompleted.ToString());
        }
        else {
            stateText.SetText("¡Héroe!");
            objectivesText.SetText("¡Todos!");
        }
        timeText.SetText(FormatTime(gameTime));
        killsText.SetText(EnemyManager.instance.kills.ToString());
    }

    public string FormatTime(float time) {
        float minutes = Mathf.Floor(gameTime / 60);
        int seconds = Mathf.RoundToInt(gameTime % 60);

        if (minutes < 10 && seconds < 10) {
            return ("0" + minutes + ":0" + seconds);
        }
        else if (minutes < 10 && seconds >= 10) {
            return ("0" + minutes + ":" + seconds);
        }
        else if (minutes >= 10 && seconds < 10) {
            return (minutes + ":0" + seconds);
        }
        else if (minutes >= 10 && seconds >= 10) {
            return (minutes + ":" + seconds);
        }
        else {
            return "";
        }
        
    }
}
