using UnityEngine;
using UnityEngine.UI;

public class playerScore : MonoBehaviour {

    public Text Score;
    public PlayerStats playerStatsComp;

    private void Start() {
        playerStatsComp = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
    }
    public void Update() {
        Score.text = "Puntos: " + playerStatsComp.playerScore.ToString();
    }
}
