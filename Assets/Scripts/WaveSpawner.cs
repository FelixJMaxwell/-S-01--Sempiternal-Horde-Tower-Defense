using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class WaveSpawner : MonoBehaviour {
    
    [System.Serializable]
    public class waveInformation {
        public GameObject enemyPrefab;
        public int countOfEnemies;
        public float RateOfSpawn;
    }

    [Header("Configuraciones")]
    public Transform spawnPoint;
    public bool firstWaveStopped = true;
    public GameObject MortarTower;
    
    [Header("Informacion sobre la horda")]
    public GameObject[] enemigosPrefab;
    public waveInformation[] waves = new waveInformation[10];
    
    public static float moreTime;
    public static int enemiesAlive = 0;

    public float porcentajeEnemigos = 0.3f;

    private int waveIndex = 0;
    private float countdown = 5f;
    private float timeBetweenWaves = 6f;
    private int incrementoDeLaHorda = 0;
    private bool nuevasHordas = false;
    private string nombreEscenaActual;
    private bool anotherBoolean = false;
    private PlayerStats playerStatsComp;
    private float[] valoresParaEnemigos = new float[4];
    private int porcentajeHordaActual = 0;

    private Button timerForWavesButton;
    private Text timerTextComp;

    private void Start() {
        Scene escenaActual = SceneManager.GetActiveScene();
        nombreEscenaActual = escenaActual.name;

        if (nombreEscenaActual != "MainMenu") {
            timerForWavesButton = GameObject.Find("TimesForWavesButton").GetComponent<Button>();
            timerTextComp = timerForWavesButton.GetComponentInChildren<Text>();
        }
        
        playerStatsComp = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();

        timeBetweenWaves = timeBetweenWaves + moreTime;

        //CountOfEnemys [0] = min ~ [1] = Max
        valoresParaEnemigos[0] = 10;
        valoresParaEnemigos[1] = 13;

        //RateOfSpawn
        valoresParaEnemigos[2] = 2.0f;
        valoresParaEnemigos[3] = 3.5f;
    }

    //Algoritmo para hordas
    void SempiternalHorde() {
        nuevasHordas = false;
        int contador = 0;

        for (contador = 0; contador < waves.Length; contador++) {
            //ENEMIGOS SIMPLES
            int comparacion = Random.Range(1, 10);
            int newComparacion = Random.Range(1, 10);

            if (comparacion == 1 || comparacion == 2) {
                waves[contador].enemyPrefab = enemigosPrefab[0];
                waves[contador].countOfEnemies = Random.Range(Mathf.FloorToInt(valoresParaEnemigos[0]), Mathf.FloorToInt(valoresParaEnemigos[1]));
                waves[contador].RateOfSpawn = Random.Range(valoresParaEnemigos[2], valoresParaEnemigos[3]);
            } else if (comparacion == 3 || comparacion == 4) {
                waves[contador].enemyPrefab = enemigosPrefab[1];
                waves[contador].countOfEnemies = Random.Range(Mathf.FloorToInt(valoresParaEnemigos[0]), Mathf.FloorToInt(valoresParaEnemigos[1]));
                waves[contador].RateOfSpawn = Random.Range(valoresParaEnemigos[2], valoresParaEnemigos[3]);
            } else if (comparacion == 5 || comparacion == 6) {
                waves[contador].enemyPrefab = enemigosPrefab[2];
                waves[contador].countOfEnemies = Random.Range(Mathf.FloorToInt(valoresParaEnemigos[0]), Mathf.FloorToInt(valoresParaEnemigos[1]));
                waves[contador].RateOfSpawn = Random.Range(valoresParaEnemigos[2], valoresParaEnemigos[3]);
            } else if (comparacion == 7 || comparacion == 8) {
                waves[contador].enemyPrefab = enemigosPrefab[3];
                waves[contador].countOfEnemies = Random.Range(Mathf.FloorToInt(valoresParaEnemigos[0]), Mathf.FloorToInt(valoresParaEnemigos[1]));
                waves[contador].RateOfSpawn = Random.Range(valoresParaEnemigos[2], valoresParaEnemigos[3]);
            } else if (comparacion == 9) {
                //JEFES
                if (newComparacion == 1 || newComparacion == 2) {
                    waves[contador].enemyPrefab = enemigosPrefab[4];
                    waves[contador].countOfEnemies = Random.Range(Mathf.FloorToInt(valoresParaEnemigos[0]) / 3, Mathf.FloorToInt(valoresParaEnemigos[1]) / 3);
                    waves[contador].RateOfSpawn = Random.Range(valoresParaEnemigos[2], valoresParaEnemigos[3]);
                } else if (newComparacion == 3 || newComparacion == 4) {
                    waves[contador].enemyPrefab = enemigosPrefab[5];
                    waves[contador].countOfEnemies = Random.Range(Mathf.FloorToInt(valoresParaEnemigos[0]) / 3, Mathf.FloorToInt(valoresParaEnemigos[1]) / 3);
                    waves[contador].RateOfSpawn = Random.Range(valoresParaEnemigos[2], valoresParaEnemigos[3]);
                } else if (newComparacion == 5 || newComparacion == 6) {
                    waves[contador].enemyPrefab = enemigosPrefab[6];
                    waves[contador].countOfEnemies = Random.Range(Mathf.FloorToInt(valoresParaEnemigos[0]) / 3, Mathf.FloorToInt(valoresParaEnemigos[1]) / 3);
                    waves[contador].RateOfSpawn = Random.Range(valoresParaEnemigos[2], valoresParaEnemigos[3]);
                } else if (newComparacion == 7 || newComparacion == 8) {
                    waves[contador].enemyPrefab = enemigosPrefab[7];
                    waves[contador].countOfEnemies = Random.Range(Mathf.FloorToInt(valoresParaEnemigos[0]) / 3, Mathf.FloorToInt(valoresParaEnemigos[1]) / 3);
                    waves[contador].RateOfSpawn = Random.Range(valoresParaEnemigos[2], valoresParaEnemigos[3]);
                } else if (newComparacion == 9) {
                    //enemigo especial
                    waves[contador].enemyPrefab = enemigosPrefab[8];
                    waves[contador].countOfEnemies = Random.Range((Mathf.FloorToInt(valoresParaEnemigos[0]) * 2), (Mathf.FloorToInt(valoresParaEnemigos[1]) * 2));
                    waves[contador].RateOfSpawn = Random.Range(valoresParaEnemigos[2] * 5, valoresParaEnemigos[3] * 10);
                }
            }
        }
    }

    //Se ejecuta una ves por frame(cuadro)
    //se ejecuta y vigila el contador para las hordas
    private void Update() {

        if (spawnPoint == null) {
            return;
        }

        if (anotherBoolean) {
            anotherBoolean = false;
            if (playerStatsComp.cantidadHordas != 0) {
                if (incrementoDeLaHorda % 50 == 0) {

                    valoresParaEnemigos[0] += Mathf.Floor(Random.Range(1, 3));
                    valoresParaEnemigos[1] += Mathf.Floor(Random.Range(1, 3));

                    if (valoresParaEnemigos[2] >= 7.5) {
                        return;
                    } else {
                        valoresParaEnemigos[2] += valoresParaEnemigos[2] + 0.25f;
                    }
                    if (valoresParaEnemigos[3] >= 15) {
                        return;
                    } else {
                        valoresParaEnemigos[3] += valoresParaEnemigos[2] + 0.3f;
                    }
                }
            }
        }
        
        if (firstWaveStopped == true) {
            return;
        }

        if (enemiesAlive > porcentajeHordaActual) {
            timerForWavesButton.enabled = false;
            return;
        } else if (enemiesAlive < porcentajeHordaActual) {
            timerForWavesButton.enabled = true;
            countdown -= Time.deltaTime;
        } else if (enemiesAlive == 0) {
            timerForWavesButton.enabled = true;
            countdown -= Time.deltaTime;
        }

        if (countdown <= 0f) {
            //Se establece la variable estadoLanzamiento en true para empezar a disparar morteros en el
            //momento en que los enemigos empizan a Spawnear
            MortarTower.GetComponent<Turret>().estadoLanzamiento = true;
            //llamada a la Coroutina para spawnear hordas
            StartCoroutine(spawnWave());
            //countdown, contador inicial para primera horda, despues se asigna timeBetweenWaves
            //que es el tiempo para la siguiente horda
            countdown = timeBetweenWaves;
            timerTextComp.text = string.Format("{0:00.00}", countdown);
            return;
        }

        //asignando el valor actual de countdown al texto de la UI
        //string.Format, para darle al countdown estilo de tiempo
        //clamp para limitar el valor de countdown entre 0 e infinito para evitar que sea en algun momento menor a cero
        countdown = Mathf.Clamp(countdown, 0, Mathf.Infinity);
        timerTextComp.text = string.Format("{0:00.00}", countdown);

        if (nuevasHordas) {
            SempiternalHorde();
        }
    }

    //metodo para spawnear hordas
    IEnumerator spawnWave() {
        //sumamos uno a la variable cantidadHordas de PlayerStats para llevar la cuenta de hordas
        if (playerStatsComp.lives <= 0) {
            yield break;
        } else {
            playerStatsComp.cantidadHordas++;
        }
        incrementoDeLaHorda += 1;

        //almacenamos la informacion de waves y le damos waveIndex para saber que objeto de la lista usaremos
        //lista a definida en el inspector de Unity, con las caracteristicas de los enemigos asi como la cantidad
        waveInformation wave = waves[waveIndex];
        enemiesAlive = wave.countOfEnemies;

        //Debug.Log("cantidad de enemigos en esta horda: " + wave.countOfEnemies);
        //Debug.Log("10% de enemigos de la horda: " + Mathf.FloorToInt(wave.countOfEnemies * 0.25f));
        porcentajeHordaActual = Mathf.FloorToInt(wave.countOfEnemies * porcentajeEnemigos);

        if (playerStatsComp.cantidadHordas > 1) {
            waves[waveIndex].enemyPrefab.GetComponent<Enemy>().increasePower = true;

            if (playerStatsComp.cantidadHordas % 3 == 0 ) {
                Enemy.increaseMultiplier += 1;
            }

        }

        for (int i = 0; i < wave.countOfEnemies; i++) {
            spawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f / wave.RateOfSpawn);
        }

        //Index de la horda
        waveIndex++;

        if (waveIndex == waves.Length) {
            waveIndex = 0;
            nuevasHordas = true;
            anotherBoolean = true;
        }
    }

    //metodo para posicionar al nuevo enemigo que aparecera
    void spawnEnemy(GameObject enemyPrefab) {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, enemyPrefab.transform.rotation);
    }
    
    public void spawnNow() {
        firstWaveStopped = false;
        countdown = timeBetweenWaves;
        timerTextComp.text = string.Format("{0:00.00}", countdown);
        StartCoroutine(spawnWave());
    }

}
 