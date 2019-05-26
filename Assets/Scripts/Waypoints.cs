using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour {

    //Nuevo wavespawner
    //public Transform[] points;

    //Antiguo waveSpawner
    public static Transform[] points;

    private void Awake(){
        //contamos la cantidad de hijos dentro dentro del objeto actual
        //estos son los puntos "camino" que se usan para redirigir a los enemigos
        points = new Transform[transform.childCount];

        for (int i = 0; i < points.Length; i++)
        {
            points [i] = transform.GetChild(i);
        }
    }
}
