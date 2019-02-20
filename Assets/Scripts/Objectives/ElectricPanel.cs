using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ElectricPanel : MonoBehaviour {
    private AudioSource epAudio;
    public AudioClip hoverAudio;
    public AudioClip clicAudio;
    public TextMeshProUGUI interactText;
    public GameObject epPanel;
    public Image[] pieces;
    private float[] rots;
    private bool finishedPuzzle;
    private bool changeObjective;
    private PlayerController player;

    // Start is called before the first frame update
    void Start() {
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
        if (ObjectivesManager.instance.currentObjective == 5 && epPanel.activeSelf) {
            finishedPuzzle = CheckPuzzle();
            if (finishedPuzzle && changeObjective) {
                StartCoroutine(EndObjective5());
                AllWhite();
                changeObjective = false;
            }
        }
    }

    bool CheckPuzzle() {
        int piz = 0;
        foreach (var piece in pieces) { //Por cada pieza
            piz++;
            if (piece.transform.rotation != Quaternion.identity) {  //Si una pieza está rotada
                Debug.Log(piz.ToString() + piece.transform.rotation.ToString() + Quaternion.identity.ToString());
                return false;           //Se regresa false.
            }
        }
        return true; //Si nnguna pieza está rotada, se regesa true.
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            switch (ObjectivesManager.instance.currentObjective) {
                case 3:
                    interactText.SetText("Presiona 'E' para analizar el panel.");
                    interactText.gameObject.SetActive(true);
                    break;
                case 5:
                    interactText.SetText("Presiona 'E' para reparar el panel.");
                    interactText.gameObject.SetActive(true);
                    break;
            }
        }
    }

    void OnTriggerStay(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            if (Input.GetKeyDown(KeyCode.E)) {
                switch (ObjectivesManager.instance.currentObjective) {
                    case 3:
                        ObjectivesManager.instance.StartObjective4();
                        break;
                    case 5:
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
        player.openPanel = true;
        epPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ClosePanel() {
        epPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        player.openPanel = false;
    }

    public void HoverPiece(int piece) {
        if (!finishedPuzzle) {
            epAudio.PlayOneShot(hoverAudio);
            pieces[piece].color = Color.gray;
        }
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

    IEnumerator EndObjective5() {
        yield return new WaitForSeconds(1.0f);
        ClosePanel();
        ObjectivesManager.instance.StartObjective6();
    }
}
