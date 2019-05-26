using UnityEngine;

public class CameraControllerMainMenu : MonoBehaviour {

    private bool disableMouseCameraMovement = true;
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 5f;

    //maximo y minimo en altura, para acerca o alejar la camara
    
    public static float minY = 0f;
    public static float maxY = 0f;
    public static float minX = 0f;
    public static float maxX = 0f;
    public static float minZ = 0f;
    public static float maxZ = 0f;

    // Update is called once per frame
    void Update() {
        if (GameManager.GameIsOver) {
            this.enabled = false;
            return;
        }
        //ESTA PARTE DEL CODIGO PARA DEJAR DE MOVER LA CAMARA CON UNA TECLA DEBE SER REMOVIDA
        //si presionamos la tecla antes del numero 1 encima de todas las letras, 
        //se deshabilita el movimiento de la camara
        if (Input.GetKeyDown(KeyCode.Backslash)) {
            disableMouseCameraMovement = !disableMouseCameraMovement;
        }
            
        //por lo anterior lo que sigue despues del siguiente if, ya no se ejecutara
        //IDEA, mover los or (||) para el mouse en un condicional independiente, para no deshabilitar el
        //movimiento con WASD
        if (!disableMouseCameraMovement)
            return;
        //movimiento general de la camara usando WASD o el mouse en su defecto
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness) {
            transform.Translate(Vector3.up * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness) {
            transform.Translate(Vector3.down * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness) {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness) {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        foreach (Touch touch in Input.touches) {
            if (touch.phase == TouchPhase.Moved) {
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                if (touchDeltaPosition.y > 0) {
                    transform.Translate(Vector3.up * panSpeed * Time.deltaTime, Space.World);
                } else if (touchDeltaPosition.y < 0) {
                    transform.Translate(Vector3.down * panSpeed * Time.deltaTime, Space.World);
                }
            }
        }

        Vector3 cameraPosition = transform.position;
        //limitando un maximo y minimo, para el acercamiendo o alejamiento de la camara
        //asi no atravesamos el piso o subimos tan alto que los objetos desaparescan
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, minY, maxY);
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, minX, maxX);
        cameraPosition.z = Mathf.Clamp(cameraPosition.z, minZ, maxZ);
        transform.position = cameraPosition;
    }

    public void touchDentro() {
        disableMouseCameraMovement = false;
    }

    public void touchFuera() {
        disableMouseCameraMovement = true;
    }
}
