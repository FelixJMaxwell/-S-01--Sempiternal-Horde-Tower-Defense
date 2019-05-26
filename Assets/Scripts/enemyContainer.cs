using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyContainer : MonoBehaviour {

    public GameObject[] enemys;

    private void Start() {
        InvokeRepeating("buscarEfectos", 0, 5.0f);
    }

    void buscarEfectos() {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemys) {
            enemy.transform.parent = transform;
        }
    }
}
