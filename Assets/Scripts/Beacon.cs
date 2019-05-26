using UnityEngine;
using UnityEngine.SceneManagement;

public class Beacon : MonoBehaviour {

    public static float offset = 1.2f;
    public static bool alreadyChecked = false;

    public float rango = 1;

    private Vector3 screenPoint;
    private string nivelActual;

    private void Start() {
        rango = 3.5f;

        nivelActual = SceneManager.GetActiveScene().name;

        if (alreadyChecked) {
            return;
        } else {
            Beacon.alreadyChecked = true;
            if (nivelActual == "Level01") {
                offset = 1.2f;
            } else if (nivelActual == "Level02" || nivelActual == "Level03" || nivelActual == "Level04") {
                offset = 0;
            } else if (nivelActual == "Level05") {
                offset = 0.1f;
            }
        }
    }

    private void OnMouseDown() {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDrag() {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);// + offset;
        transform.position = curPosition;
        //la siguiente asignacion es para que en caso de que con el cogido de arriba, si el beacon queda "flotando"
        //su posicion se regrese a 0 en Y
        transform.position = new Vector3(transform.position.x, offset, transform.position.z);
    }

    private void Update() {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, rango);
        foreach (Collider collider in collidersInRange) {
            float distanciaEntreColliders = Vector3.Distance(collider.transform.position, transform.position);
            if (distanciaEntreColliders<= rango) {
                if (collider.tag == "Enemy" && collider.GetComponent<Enemy>().experienciaParaElBeacon == "") {
                    collider.GetComponent<Enemy>().experienciaParaElBeacon = transform.name;
                }
            }
            if (distanciaEntreColliders > rango) {
                if (collider.tag == "Enemy") {
                    collider.GetComponent<Enemy>().experienciaParaElBeacon = "";
                }
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        //se dibuja en la escena de Unity un circulo que muestra el rango, alrededor
        Gizmos.DrawWireSphere(transform.position, rango);
    }
}