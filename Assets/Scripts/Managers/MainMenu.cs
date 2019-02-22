using System.Collections;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour {
    private bool loading;
    public MenuTexts texts;
    public TextMeshProUGUI descText;
    public GameObject optionsPanel;

    void Start() {
        loading = false;
        GameManager.instance.inMenu = true;
        DefaultDesc();
    }

    void Update() {
        if (loading) {
            GameManager.instance.FadeScreen(true);
        }
    }

    public void DefaultDesc() {
        optionsPanel.SetActive(false);
        descText.SetText(texts.IntroText);
        descText.alignment = TextAlignmentOptions.Center;
    }

    public void HoverSinglePlayer() {
        GameManager.instance.Hover();
        optionsPanel.SetActive(false);
        descText.SetText(texts.SinglePlayerHover);
        descText.alignment = TextAlignmentOptions.Center;
    }

    public void HoverMultiPlayer() {
        GameManager.instance.Hover();
        optionsPanel.SetActive(false);
        descText.SetText(texts.MultiPlayerHover);
        descText.alignment = TextAlignmentOptions.Center;
    }

    public void HoverOptions() {
        GameManager.instance.Hover();
        descText.SetText(texts.OptionsHover);
        optionsPanel.SetActive(false);
        descText.alignment = TextAlignmentOptions.Center;
    }

    public void HoverExit() {
        GameManager.instance.Hover();
        optionsPanel.SetActive(false);
        descText.SetText(texts.ExitHover);
    }

    public void LoadSinglePlayer() {
        loading = true;
        StartCoroutine(DelayLoad());
    }

    public void ShowOptions() {
        optionsPanel.SetActive(true);
        descText.alignment = TextAlignmentOptions.TopLeft;
    }

    public void QuitGame() {
        loading = true;
        GameManager.instance.ExitGame();
    }

    IEnumerator DelayLoad() {
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.LoadSinglePlayer();

    } 
}
