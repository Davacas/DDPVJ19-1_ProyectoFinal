using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ElectricPanel : MonoBehaviour {
    //Audio
    private AudioSource epAudio;
    public AudioClip hoverAudio;
    public AudioClip clicAudio;
    //Visuales
    public TextMeshProUGUI interactText;
    public GameObject epPanel;
    public Image[] pieces;
    private float[] rots;
    public Light[] lights;
    //Otros
    private bool finishedPuzzle;
    private bool changeObjective;
    public bool epPanelOpen;
    private PlayerController player;
    public static ElectricPanel instance;

    // Start is called before the first frame update
    void Start() {
        epPanelOpen = false;
        instance = this;
        finishedPuzzle = false;
        changeObjective = true;
        player = PlayerController.instance;
        epAudio = GetComponent<AudioSource>();
        rots = new float[4] { 0.0f, 90.0f, 180.0f, 270.0f };
        foreach (var piece in pieces) {
            piece.transform.Rotate(0.0f, 0.0f, rots[Random.Range(0, rots.Length)]);
        }
    }

    void Update() { //Hacer función que detecte si se acabó el puzzle.
        if (ObjectivesManager.instance.currentObjective == 6 && epPanel.activeSelf) {
            finishedPuzzle = CheckPuzzle();
            if (finishedPuzzle && changeObjective) {
                StartCoroutine(EndObjective6());
                AllWhite();
                changeObjective = false;
            }
        }
    }

    void EnableLights() {
        foreach(var light in lights) {
            light.gameObject.SetActive(true);
        }
    }

    bool CheckPuzzle() {
        foreach (var piece in pieces) { //Por cada pieza
            if (piece.transform.rotation.eulerAngles.z >= 0.1 || piece.transform.rotation.eulerAngles.z <= -0.1) {  //Si una pieza está rotada
                return false;           //Se regresa false.
            }
        }
        print("Resuelto.");
        return true; //Si nnguna pieza está rotada, se regesa true.
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            switch (ObjectivesManager.instance.currentObjective) {
                case 4:
                    interactText.SetText("Presiona 'E' para analizar el panel.");
                    interactText.gameObject.SetActive(true);
                    break;
                case 6:
                    interactText.SetText("Presiona 'E' para reparar el panel.");
                    interactText.gameObject.SetActive(true);
                    break;
                default:
                    interactText.SetText("Este es el panel eléctrico de la facultad.");
                    break;
            }
        }
    }

    void OnTriggerStay(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            if (Input.GetKeyDown(KeyCode.E)) {
                switch (ObjectivesManager.instance.currentObjective) {
                    case 4:
                        ObjectivesManager.instance.StartObjective5();
                        break;
                    case 6:
                        OpenEPanel();
                        break;
                }
            }
        }
    }

    void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            interactText.gameObject.SetActive(false);
        }
    }

    public void OpenEPanel() {
        epPanelOpen = true;
        epPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ClosePanel() {
        epPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        epPanelOpen = false;
    }

    public void HoverPiece(int piece) {
        if (!finishedPuzzle) {
            epAudio.PlayOneShot(hoverAudio);
            pieces[piece].color = Color.gray;
        }
        print(pieces[piece].transform.rotation.eulerAngles.z);
    }

    public void ExitHover(int piece) {
        if (!finishedPuzzle) {
            pieces[piece].color = Color.white;
        }
    }

    public void RotatePiece(int piece) {
        if (!finishedPuzzle) {
            epAudio.PlayOneShot(clicAudio);
            pieces[piece].transform.Rotate(0.0f, 0.0f, -90.0f);
        }
    }

    void AllWhite() {
        foreach(var piece in pieces) {
            piece.color = Color.white;
        }
    }

    IEnumerator EndObjective6() {
        yield return new WaitForSeconds(1.0f);
        ClosePanel();
        EnableLights();
        ObjectivesManager.instance.StartObjective7();
    }
}
