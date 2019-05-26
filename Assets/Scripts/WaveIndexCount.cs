using UnityEngine;
using UnityEngine.UI;

public class WaveIndexCount : MonoBehaviour {

    public Text WaveIndexScore;
    public PlayerStats playerStatsComp;

    private void Start() {
        playerStatsComp = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
    }

    public void Update() {
        WaveIndexScore.text = "Horda: " + playerStatsComp.cantidadHordas.ToString();
    }
}
