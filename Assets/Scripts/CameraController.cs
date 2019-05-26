using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

    public static bool alternarZoom = false;
    public static bool alternarMov = false;
    public static float newPanSpeed = 0.01f;

    private bool disableMouseCameraMovement = true;
    public float panSpeed = 0.01f;
    public float zoomSpeed = 1;

    //maximo y minimo en altura, para acerca o alejar la camara
    public float minY = 2f;
    public float maxY = 20f;

    public float minX = 2f;
    public float maxX = 20f;

    public float minZ = 2f;
    public float maxZ = 20f;

    public int indiceDeRotacion = 0;

    public Vector3 posicion1;
    public Vector3 posicion2;
    public Vector3 posicion3;
    public Vector3 posicion4;

    private PlayerStats pStatsComp;

    public Toggle togMov;
    public Toggle togZoom;
    public Slider camSpeed;

    private void Start() {
        pStatsComp = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();

        Input.simulateMouseWithTouches = true;

        if (alternarMov) {
            panSpeed = -0.01f;
            togMov.isOn = true;
        } else {
            panSpeed = 0.01f;
            togMov.isOn = false;
        }

        if (alternarZoom) {
            zoomSpeed = -1;
            togZoom.isOn = true;
        } else {
            zoomSpeed = 1;
            togZoom.isOn = false;
        }

        panSpeed = newPanSpeed;
        camSpeed.value = panSpeed;
    }

    private void Update() {
        if (GameManager.GameIsOver) {
            this.enabled = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Backslash)) {
            disableMouseCameraMovement = !disableMouseCameraMovement;
        }

        if (!disableMouseCameraMovement) {
            return;
        }

        if (indiceDeRotacion == 0) {
            
            foreach (Touch touch in Input.touches) {
                if (Input.touchCount == 1) {
                    if (touch.phase == TouchPhase.Moved) {
                        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                        if (touchDeltaPosition.x > 0 && touchDeltaPosition.y > 0) {
                            transform.position += new Vector3(touchDeltaPosition.x * panSpeed, 0, touchDeltaPosition.y * panSpeed);
                        } else if (touchDeltaPosition.x < 0 && touchDeltaPosition.y > 0) {
                            transform.position += new Vector3(touchDeltaPosition.x * panSpeed, 0, touchDeltaPosition.y * panSpeed);
                        } else if (touchDeltaPosition.x < 0 && touchDeltaPosition.y < 0) {
                            transform.position += new Vector3(touchDeltaPosition.x * panSpeed, 0, touchDeltaPosition.y * panSpeed);
                        } else if (touchDeltaPosition.x > 0 && touchDeltaPosition.y < 0) {
                            transform.position += new Vector3(touchDeltaPosition.x * panSpeed, 0, touchDeltaPosition.y * panSpeed);
                        }
                    }
                }

                if (Input.touchCount == 2) {
                    if (Input.GetTouch(0).phase == TouchPhase.Moved) {
                        transform.Translate(0, touch.deltaPosition.x * zoomSpeed * Time.deltaTime, 0, Space.World);
                    }
                }
            }
        }

        if(indiceDeRotacion == 1) {
            foreach (Touch touch in Input.touches) {
                if (Input.touchCount == 1) {
                    if (touch.phase == TouchPhase.Moved) {
                        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                        if (touchDeltaPosition.x > 0 && touchDeltaPosition.y > 0) {
                            transform.position += new Vector3(touchDeltaPosition.y * panSpeed, 0, ((touchDeltaPosition.x * panSpeed) * -1));
                        } else if (touchDeltaPosition.x < 0 && touchDeltaPosition.y > 0) {
                            transform.position += new Vector3(touchDeltaPosition.y * panSpeed, 0, ((touchDeltaPosition.x * panSpeed) * -1));
                        } else if (touchDeltaPosition.x < 0 && touchDeltaPosition.y < 0) {
                            transform.position += new Vector3(touchDeltaPosition.y * panSpeed, 0, ((touchDeltaPosition.x * panSpeed) * -1));
                        } else if (touchDeltaPosition.x > 0 && touchDeltaPosition.y < 0) {
                            transform.position += new Vector3(touchDeltaPosition.y * panSpeed, 0, ((touchDeltaPosition.x * panSpeed) * -1));
                        }
                    }
                }

                if (Input.touchCount == 2) {
                    if (Input.GetTouch(0).phase == TouchPhase.Moved) {
                        transform.Translate(0, touch.deltaPosition.x * zoomSpeed * Time.deltaTime, 0, Space.World);
                    }
                }
            }
        }

        if (indiceDeRotacion == 2) {
            foreach (Touch touch in Input.touches) {
                if (Input.touchCount == 1) {
                    if (touch.phase == TouchPhase.Moved) {
                        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                        if (touchDeltaPosition.x > 0 && touchDeltaPosition.y > 0) {
                            transform.position += new Vector3((touchDeltaPosition.x * panSpeed) * -1, 0, (touchDeltaPosition.y * panSpeed) * -1);
                        } else if (touchDeltaPosition.x < 0 && touchDeltaPosition.y > 0) {
                            transform.position += new Vector3((touchDeltaPosition.x * panSpeed) * -1, 0, (touchDeltaPosition.y * panSpeed) * -1);
                        } else if (touchDeltaPosition.x < 0 && touchDeltaPosition.y < 0) {
                            transform.position += new Vector3((touchDeltaPosition.x * panSpeed) * -1, 0, (touchDeltaPosition.y * panSpeed) * -1);
                        } else if (touchDeltaPosition.x > 0 && touchDeltaPosition.y < 0) {
                            transform.position += new Vector3((touchDeltaPosition.x * panSpeed) * -1, 0, (touchDeltaPosition.y * panSpeed) * -1);
                        }
                    }
                }

                if (Input.touchCount == 2) {
                    if (Input.GetTouch(0).phase == TouchPhase.Moved) {
                        transform.Translate(0, touch.deltaPosition.x * zoomSpeed * Time.deltaTime, 0, Space.World);
                    }
                }
            }
        }

        if (indiceDeRotacion == 3) {
            foreach (Touch touch in Input.touches) {
                if (Input.touchCount == 1) {
                    if (touch.phase == TouchPhase.Moved) {
                        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                        if (touchDeltaPosition.x > 0 && touchDeltaPosition.y > 0) {
                            transform.position += new Vector3((touchDeltaPosition.y * panSpeed) * -1, 0, touchDeltaPosition.x * panSpeed);
                        } else if (touchDeltaPosition.x < 0 && touchDeltaPosition.y > 0) {
                            transform.position += new Vector3((touchDeltaPosition.y * panSpeed) * -1, 0, touchDeltaPosition.x * panSpeed);
                        } else if (touchDeltaPosition.x < 0 && touchDeltaPosition.y < 0) {
                            transform.position += new Vector3((touchDeltaPosition.y * panSpeed) * -1, 0, touchDeltaPosition.x * panSpeed);
                        } else if (touchDeltaPosition.x > 0 && touchDeltaPosition.y < 0) {
                            transform.position += new Vector3((touchDeltaPosition.y * panSpeed) * -1, 0, touchDeltaPosition.x * panSpeed);
                        }
                    }
                }

                if (Input.touchCount == 2) {
                    if (Input.GetTouch(0).phase == TouchPhase.Moved) {
                        transform.Translate(0, touch.deltaPosition.x * zoomSpeed * Time.deltaTime, 0, Space.World);
                    }
                }
            }
        }

        //asignando el componente Transform.position(XYZ) del objeto actual (camara)
        Vector3 cameraPosition = transform.position;
        //limitando un maximo y minimo, para el acercamiendo o alejamiento de la camara
        //asi no atravesamos el piso o subimos tan alto que los objetos desaparescan
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, minY, maxY);
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, minX, maxX);
        cameraPosition.z = Mathf.Clamp(cameraPosition.z, minZ, maxZ);
        transform.position = cameraPosition;
    }

    public void alternaZoom(bool tog) {
        if (tog) {
            zoomSpeed = -1;
            pStatsComp.alternarZoom = true;
        } else {
            zoomSpeed = 1;
            pStatsComp.alternarZoom = false;
        }
    }

    public void alternaCameraMovement(bool tog) {
        if (tog) {
            panSpeed = -0.01f;
            pStatsComp.alternarMov = true;
        } else {
            panSpeed = 0.01f;
            pStatsComp.alternarMov = false;
        }
    }

    public void cameraSpeed(float nuevaVelocidad) {
        panSpeed = nuevaVelocidad;
        pStatsComp.velocidadCamara = nuevaVelocidad;
    }

    public void rotarCameraMasNoventa() {
        transform.Rotate(0, 90, 0, Space.World);
        indiceDeRotacion += 1;

        if (indiceDeRotacion == 4) {
            indiceDeRotacion = 0;
        }

        if (SceneManager.GetActiveScene().name == "Level01") {
            if (indiceDeRotacion == 0) {
                transform.position = posicion1;
                minY = 4f;
                maxY = 15f;
                minX = -10f;
                maxX = 16f;
                minZ = 1f;
                maxZ = 30f;
            } else if (indiceDeRotacion == 1) {
                transform.position = posicion2;
                minY = 2f;
                maxY = 20f;
                minX = -12f;
                maxX = 17f;
                minZ = 7f;
                maxZ = 32.5f;
            } else if (indiceDeRotacion == 2) {
                transform.position = posicion3;
                minY = 4f;
                maxY = 15f;
                minX = -7f;
                maxX = 14f;
                minZ = 7.5f;
                maxZ = 37f;
            } else if (indiceDeRotacion == 3) {
                transform.position = posicion4;
                minY = 2f;
                maxY = 20f;
                minX = -6f;
                maxX = 22f;
                minZ = 7f;
                maxZ = 32.5f;
            }
        } else if (SceneManager.GetActiveScene().name == "Level02" || SceneManager.GetActiveScene().name == "Level03") {
            if (indiceDeRotacion == 0) {
                transform.position = posicion1;
                minY = 4f;
                maxY = 15f;
                minX = -6.75f;
                maxX = 12f;
                minZ = -1f;
                maxZ = 17f;
            } else if (indiceDeRotacion == 1) {
                transform.position = posicion2;
                minY = 2f;
                maxY = 15f;
                minX = -12f;
                maxX = 10f;
                minZ = 5f;
                maxZ = 22f;
            } else if (indiceDeRotacion == 2) {
                transform.position = posicion3;
                minY = 4f;
                maxY = 15f;
                minX = -7f;
                maxX = 10f;
                minZ = 7.5f;
                maxZ = 27f;
            } else if (indiceDeRotacion == 3) {
                transform.position = posicion4;
                minY = 2f;
                maxY = 15f;
                minX = -4f;
                maxX = 16f;
                minZ = 5f;
                maxZ = 23f;
            }
        } else if (SceneManager.GetActiveScene().name == "Level04") {
            if (indiceDeRotacion == 0) {
                transform.position = posicion1;
                minY = 4f;
                maxY = 15f;
                minX = -3f;
                maxX = 23f;
                minZ = -2f;
                maxZ = 25f;
            } else if (indiceDeRotacion == 1) {
                transform.position = posicion2;
                minY = 2f;
                maxY = 20f;
                minX = -6.5f;
                maxX = 14f;
                minZ = 5f;
                maxZ = 22f;
            } else if (indiceDeRotacion == 2) {
                transform.position = posicion3;
                minY = 4f;
                maxY = 15f;
                minX = 1f;
                maxX = 20f;
                minZ = 11f;
                maxZ = 32f;
            } else if (indiceDeRotacion == 3) {
                transform.position = posicion4;
                minY = 2f;
                maxY = 20f;
                minX = 6f;
                maxX = 27.5f;
                minZ = 9f;
                maxZ = 23f;
            }
        } else if (SceneManager.GetActiveScene().name == "Level05") {
            if (indiceDeRotacion == 0) {
                transform.position = posicion1;
                minY = 4f;
                maxY = 15f;
                minX = 4f;
                maxX = 16f;
                minZ = -2.5f;
                maxZ = 20f;
            } else if (indiceDeRotacion == 1) {
                transform.position = posicion2;
                minY = 2f;
                maxY = 20f;
                minX = -4.5f;
                maxX = 10f;
                minZ = 5f;
                maxZ = 18f;
            } else if (indiceDeRotacion == 2) {
                transform.position = posicion3;
                minY = 4f;
                maxY = 15f;
                minX = 2.5f;
                maxX = 14f;
                minZ = 10f;
                maxZ = 27f;
            } else if (indiceDeRotacion == 3) {
                transform.position = posicion4;
                minY = 2f;
                maxY = 20f;
                minX = 6f;
                maxX = 25f;
                minZ = 8f;
                maxZ = 15f;
            }
        }
    }
}