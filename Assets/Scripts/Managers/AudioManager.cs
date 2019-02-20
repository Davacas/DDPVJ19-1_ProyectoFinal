using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;
    public AudioMixer audioMixer;
    private float musicVolume;
    private float sfxVolume;
    
    void Awake() {
        instance = this;
        AudioManager.instance.SetMusicVolume(0);
        AudioManager.instance.SetSFXVolume(0);
    }

    void Start() {
        audioMixer.GetFloat("MusicVolume", out musicVolume);
        audioMixer.GetFloat("SFXVolume", out sfxVolume);
    }

    public void SetMusicVolume(float volume) {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume) {
        audioMixer.SetFloat("SFXVolume", volume);
    }
}