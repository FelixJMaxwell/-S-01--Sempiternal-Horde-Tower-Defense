using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseSpeed : MonoBehaviour {

    public Sprite[] cambios;
    public Button increaseSpeedBtn;
    public float contadorDeIncTiempo;
    public bool pausedMenu;

    public void incrementarContador() {
        contadorDeIncTiempo += 1;
    }

    private void Start() {
        contadorDeIncTiempo = 1;
        increaseSpeedBtn.image.sprite = cambios[0];
    }

    private void Update() {
        if (pausedMenu) {
            return;
        }

        if (contadorDeIncTiempo == 1) {
            increaseSpeedBtn.image.sprite = cambios[0];
            Time.timeScale = 1f;
        } else if (contadorDeIncTiempo == 2) {
            increaseSpeedBtn.image.sprite = cambios[1];
            Time.timeScale = 2f;
        } else if (contadorDeIncTiempo == 3) {
            increaseSpeedBtn.image.sprite = cambios[2];
            Time.timeScale = 3f;
        } else if (contadorDeIncTiempo == 4) {
            contadorDeIncTiempo = 1;
        }
    }

}