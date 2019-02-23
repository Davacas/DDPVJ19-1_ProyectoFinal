using System.Collections;
using UnityEngine;
using TMPro;

public class StoryManager : MonoBehaviour {
    public AudioSource music;
    public AudioClip storyMusic;
    public GameObject[] msgs;
    public GameObject storyPanel;
    public GameObject hudPanel;
    public static StoryManager instance;

    private int messages = 0;

    void Start() {
        instance = this;
        ShowStory();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.watchingStory) {
            StartGame();
        }
    }

    public void ShowStory() {
        PauseManager.instance.ShowPause();
        PauseManager.instance.mainPanel.SetActive(false);
        storyPanel.SetActive(true);
        hudPanel.SetActive(false);

        music.PlayOneShot(storyMusic);
        InvokeRepeating("ScrollMessage", 0.0f, 2.589f);
    }

    void ScrollMessage() {
        messages++;
        Vector3 offset = new Vector3(0.0f, 100.0f, 0.0f);
        foreach (var msg in msgs) {
            msg.transform.localPosition += offset;
            if (msg.transform.localPosition.y < -250 || msg.transform.localPosition.y > 350) {
                msg.SetActive(false);
            }
            else {
                msg.SetActive(true);
            }
        }
        if (messages > 24) {
            StartGame();
        }
    }

    void StartGame() {
        CancelInvoke();
        PauseManager.instance.ClosePause();
        PauseManager.instance.mainPanel.SetActive(true);
        GameManager.instance.playing = true;
        GameManager.instance.watchingStory = false;
        EnemyManager.instance.StartSpawning();
        storyPanel.SetActive(false);
        hudPanel.SetActive(true);
        music.Stop();
        music.Play();
    }
}
