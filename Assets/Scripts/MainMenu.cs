using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public static MainMenu instance;
    private AudioSource menuAudio;
    public AudioClip hover;
    public AudioClip clic;
    public Image fade;


    void Awake() {
        menuAudio = GetComponent<AudioSource>();
        instance = this;
    }

    public void LoadMenu() {
        SceneManager.LoadScene(0);
        Cursor.visible = true;
    }

    public void LoadSinglePlayer() {
        fade.gameObject.SetActive(true);
        fade.CrossFadeAlpha(1.0f, 1.0f, false);
        menuAudio.PlayOneShot(clic);
        SceneManager.LoadScene(1);
        Cursor.visible = false;
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
