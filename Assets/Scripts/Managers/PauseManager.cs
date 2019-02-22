using UnityEngine;
using TMPro;

public class PauseManager : MonoBehaviour {
    public GameObject pauseScreen;
    public GameObject mainPanel;
    public GameObject optionsPanel;
    public static PauseManager instance;
    public TextMeshProUGUI timeText;
    private GameManager GMInstance;
    public bool pausePanelOpen;

    void Start() {
        pausePanelOpen = false;
        GMInstance = GameManager.instance;
        instance = this;
    }

    void Update() {
        timeText.SetText(GMInstance.FormatTime(GMInstance.gameTime));
    }

    public void ShowPause() {
        pausePanelOpen = true;
        pauseScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ClosePause() {
        pausePanelOpen = false;
        pauseScreen.SetActive(false);
        if (ObjectivesManager.instance.currentObjective != 1) { 
            PlayerController.instance.inventory[PlayerController.instance.currentWeapon].Withdraw();
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ShowOptions() {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void BackToMain() {
        optionsPanel.SetActive(false);
        mainPanel.SetActive(true);        
    }

    public void CloseOptions() {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

}
