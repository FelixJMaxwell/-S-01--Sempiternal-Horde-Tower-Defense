using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Enemy))]
public class enemyMovement : MonoBehaviour {

    /*
     Separados el script de Enemy
     los atributos no publicos que tienen que ver con el movimiento
     van en este, mientras que los atributos publicos van en Enemy.cs

    ++++++
        ir a Edit / Project Settings / Script Execution Order
        agregar este script para asegurar que Enemy.cs se ejecuta antes que este.
        tiene que ver con la velocidad del enemigo actual al que estan apuntando las torretas para
        ralentizarlos.
         */

    //objetivo a donde llegara el enemigo
    private Transform target;
    //indice asignado a cada waypoint
    private int wayPointIndex = 0;
    public GameObject enemyHPBar;
    public static int pathToFollow;

    private Enemy enemy;
    string nombreEscenaActual;
    float random;
    public PlayerStats playerStatsComp;

    public GameObject camino;
    public Waypoints caminoComp;

    private void Start() {
        //Nuevo waveSpawner
        //caminoComp = camino.GetComponent<Waypoints>();
        //Nuevo wavespawner

        random = Random.value;
        playerStatsComp = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        Scene escenaActual = SceneManager.GetActiveScene();
        nombreEscenaActual = escenaActual.name;

        //waypoints asignados al objetivo como un array
        enemy = GetComponent<Enemy>();

        //NuevoWaveSpawner
        //target = caminoComp.points[1];
        //NuevoWaveSpawner

        target = Waypoints.points[0];

        if (enemy.GetComponent<Animator>()) {
            if (random <= 0.5) {
                enemy.GetComponent<Animator>().SetBool("cambio", false);
            } else {
                enemy.GetComponent<Animator>().SetBool("cambio", true);
            }
        }
    }

    private void LateUpdate() {
        enemyHPBar.transform.LookAt(Camera.main.transform);
    }

    private void FixedUpdate() {
        //direccion hacia donde se encuentra el primer waypoint
        Vector3 directionToWaypoint = target.position - transform.position;
        //Para hacer que el enemigo mire hacia el siguiente waypoint
        enemy.transform.LookAt(target);
        //movimiento del enemigo hacia el waypoint
        transform.Translate(directionToWaypoint.normalized * enemy.speed * Time.deltaTime, Space.World);
        //condicion en caso de que la distancia del enemigo con el waypoint objetivo sea menor a 0.2f unidades
        //si la condicion se cumple, obtenemos la direccion del siguiene waypoint y repetimos
        if (Vector3.Distance(transform.position, target.position) <= 0.2f) {
            GetNextWaypoint();
        }
        //si la torreta ralentiza al enemigo, al poner esto aca
        //cuando salimos del rango de la torreta, obtenemos la velocidad inicial de nuevo
        enemy.speed = enemy.startSpeed;
    }

    //funcion para obtener el siguiente waypoint
    //desde el array de waypoints, con su respectivo indice
    void GetNextWaypoint() {
        if (wayPointIndex >= Waypoints.points.Length - 1) {               /* nuevoWaveSpawner */ //caminoComp.points.Length - 1) {
            endPath();
            wayPointIndex = 0;
            return;
        }
        wayPointIndex++;
        target =   Waypoints.points[wayPointIndex];               /* nuevoWaveSpawner */ //caminoComp.points[wayPointIndex];
    }

    void endPath() {
        playerStatsComp.lives--;
        WaveSpawner.enemiesAlive--;
        Destroy(gameObject);
    }
}
