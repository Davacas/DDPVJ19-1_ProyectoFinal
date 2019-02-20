using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public static MainMenu instance;
    private AudioSource menuAudio;
    public AudioClip hover;
    public AudioClip clic;
    public Image fade;
    public Color startColor;
    public Color endColor;
    private float timeLeft = 3.0f;
    private bool loading;


    void Start() {
        menuAudio = GetComponent<AudioSource>();
        instance = this;
        loading = false;
        AudioManager.instance.SetMusicVolume(0);
        AudioManager.instance.SetSFXVolume(0);
    }

    void Update() {
        FadeScreen();
    }

    void FadeScreen() {
        if (loading) {
            fade.enabled = true;
            timeLeft -= Time.deltaTime;
            timeLeft /= 3;
            fade.color = Color.Lerp(startColor, endColor, timeLeft);
        }
    }

    public void LoadMenu() {
        SceneManager.LoadScene(0);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadSinglePlayer() {
        loading = true;
        menuAudio.PlayOneShot(clic);
        SceneManager.LoadScene(1);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadMultiPlayer() {
        menuAudio.PlayOneShot(clic);
        Debug.Log("Presionaste Multiplayer.");
    }

    public void Options() {
        menuAudio.PlayOneShot(clic);
        Debug.Log("Presionaste Opciones.");
    }

    public void ExitGame() {
        menuAudio.PlayOneShot(clic);
        Application.Quit();
    }

    public void Hover() {
        menuAudio.PlayOneShot(hover);
    }
}
