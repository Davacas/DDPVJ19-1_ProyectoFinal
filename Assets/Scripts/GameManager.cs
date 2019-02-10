using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    //public GameObject pauseScreen;
    public GameObject deathScreen;
    public static GameManager instance;


    void Awake() {
        instance = this;
    }

    public void ShowDeathScreen() {
        Invoke("LoadMenu", 5.0f);
        deathScreen.SetActive(true);
    }

    public void LoadMenu() {
        MainMenu.instance.LoadMenu();
    }
}
