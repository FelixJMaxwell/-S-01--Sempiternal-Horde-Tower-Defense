using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torretasContainer : MonoBehaviour {

    public GameObject[] torretas;

    private void Start() {
        InvokeRepeating("buscarTorretas", 0, 5.0f);
    }

    void buscarTorretas() {
        torretas = GameObject.FindGameObjectsWithTag("Tower");

        foreach (GameObject tower in torretas) {
            tower.transform.SetParent(transform);
        }
    }
}
