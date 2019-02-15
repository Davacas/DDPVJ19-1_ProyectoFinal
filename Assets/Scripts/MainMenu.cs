using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public static MainMenu instance;

    void Awake() {
        instance = this;
    }

    public void LoadMenu() {
        SceneManager.LoadScene(0);
        Cursor.visible = true;
    }

    public void LoadSinglePlayer() {
        SceneManager.LoadScene(1);
        Cursor.visible = false;
    }

    public void LoadMultiPlayer() {
        Debug.Log("Presionaste Multiplayer.");
    }

    public void Options() {
        Debug.Log("Presionaste Opciones.");
    }

    public void ExitGame() {
        Application.Quit();
    }
}
