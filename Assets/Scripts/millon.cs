using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class millon : MonoBehaviour {

    private PlayerStats playerStatsComp;

	// Use this for initialization
	void Start () {
        playerStatsComp = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
	}
	
	public void btnMillon() {
        playerStatsComp.silverAmount += 10000000;
    }
}
