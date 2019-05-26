using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectsContainer : MonoBehaviour {

    public GameObject[] efectos;

    private void Start() {
        InvokeRepeating("buscarEfectos", 0, 5.0f);
    }

    void buscarEfectos() {
        efectos = GameObject.FindGameObjectsWithTag("Effect");

        foreach (GameObject efecto in efectos) {
            efecto.transform.parent = this.transform;
        }
    }
}