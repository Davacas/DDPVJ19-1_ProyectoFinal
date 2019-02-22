using UnityEngine;

public class GetBook : MonoBehaviour {
    private MeshRenderer[] model;
    private BoxCollider bookCollider;
    private Light bookLight;
    public static GetBook instance;

    void Start() {
        bookCollider = GetComponent<BoxCollider>();
        model = GetComponentsInChildren<MeshRenderer>();
        bookLight = GetComponentInChildren<Light>();
        instance = this;
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player" && ObjectivesManager.instance.currentObjective == 2) {
            bookCollider.enabled = false;
            foreach (var mesh in model) {
                mesh.enabled = false;
            }
            bookLight.enabled = false;
            Destroy(this.gameObject, 1.0f);
            ObjectivesManager.instance.StartObjective3();
        }
    }
}
