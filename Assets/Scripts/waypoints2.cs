using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoints2 : MonoBehaviour {
    public static Transform[] pointsWaypoint2;

    private void Awake() {
        //contamos la cantidad de hijos dentro dentro del objeto actual
        //estos son los puntos "camino" que se usan para redirigir a los enemigos
        pointsWaypoint2 = new Transform[transform.childCount];

        for (int i = 0; i < pointsWaypoint2.Length; i++) {
            pointsWaypoint2[i] = transform.GetChild(i);
        }
    }
}