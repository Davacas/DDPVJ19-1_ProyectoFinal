using UnityEngine;

public class GetFuse : MonoBehaviour {
    private MeshRenderer[] model;
    private BoxCollider fuseCollider;
    private Light fuseLight;
    public static GetFuse instance;
    /*
    private Vector3 pos;
    private float xPos;
    private float zPos;
    */

    void Start() {
        fuseCollider = GetComponent<BoxCollider>();
        model = GetComponentsInChildren<MeshRenderer>();
        fuseLight = GetComponentInChildren<Light>();
        instance = this;
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player" && ObjectivesManager.instance.currentObjective == 5) {
            fuseCollider.enabled = false;
            foreach (var mesh in model) {
                mesh.enabled = false;
            }            
            fuseLight.enabled = false;
            Destroy(this.gameObject, 1.0f);
            ObjectivesManager.instance.StartObjective6();
        }
    }
    
    //Intento fallido de método que asigna una posición nueva al objeto si está dentro de otro objeto.
    /*
    void Relocate() {
        int contador = 0;
        while (true) {
            if (!Physics.CheckSphere(transform.position, 1.0f, 1 << 0)) break;
            Debug.Log("Relocalizando");
            xPos = Random.Range(-725.0f, -175.0f);
            zPos = Random.Range(-375.0f, -75.0f);
            pos = new Vector3(xPos, 2.1f, zPos);
            transform.position = pos;
            Debug.Log(++contador);
        }
    }*/
}
