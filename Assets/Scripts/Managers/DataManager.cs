using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour {
    public static DataManager instance;

    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider mouseSlider;

    private float musicVolume;
    private float sfxVolume;
    public float mouseSesitivity;

    void Awake() {
        instance = this;
    }

    void AssignData() {
        AudioManager.instance.SetMusicVolume(musicVolume);
        AudioManager.instance.SetSFXVolume(sfxVolume);
        if (PlayerController.instance != null) {
            PlayerController.instance.speedRot = mouseSesitivity;
        }
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;
        mouseSlider.value = mouseSesitivity;
    }

    public void GetData() {
        if (PlayerPrefs.HasKey("MusicVolume")) {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
            mouseSesitivity = PlayerPrefs.GetFloat("MouseSensitivity");            
        }
        else {
            musicVolume = 0.0f;
            sfxVolume = 0.0f;
            mouseSesitivity = 50.0f;
        }
        AssignData();
    }

    public void SaveData() { 
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSlider.value);
        PlayerPrefs.Save();
        GetData();
    }
}
