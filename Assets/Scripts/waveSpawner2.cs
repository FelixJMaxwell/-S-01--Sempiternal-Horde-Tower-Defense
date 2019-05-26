using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class waveSpawner2 : MonoBehaviour {

	[System.Serializable]
    public class waveInformation {
        public GameObject enemyPrefab;
        public int countOfEnemys;
        public float rateOfSpawn;
    }

    [Header("Configuraciones spawn and Waypoints")]
    public Transform[] spawnPoint;
    public GameObject[] caminos;

    [Header("Informacion sobre la horda")]
    public waveInformation[] waves = new waveInformation[2];

    public static int enemysAlive = 0;

    private int spawnPointSelected = 0;
    private int waveIndex = 0;

    IEnumerator spawnWave() {
        waveInformation wave = waves[waveIndex];
        enemysAlive = wave.countOfEnemys;

        if (spawnPoint.Length == 1) {
            spawnPointSelected = 0;
        } else {
            spawnPointSelected = Random.Range(0, spawnPoint.Length);
        }

        wave.enemyPrefab.GetComponent<enemyMovement>().camino = caminos[spawnPointSelected];

        for (int i = 0; i < wave.countOfEnemys; i++) {
            spawnEnemy(wave.enemyPrefab, spawnPoint[spawnPointSelected]);
            yield return new WaitForSeconds(1f / wave.rateOfSpawn);
        }

        waveIndex++;

        if (waveIndex == waves.Length) {
            waveIndex = 0;
        }
    }

    void spawnEnemy(GameObject enemyPrefab, Transform spawnPoint) {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, enemyPrefab.transform.rotation);
    }

    public void spawnNow() {
        StartCoroutine(spawnWave());
    }
}