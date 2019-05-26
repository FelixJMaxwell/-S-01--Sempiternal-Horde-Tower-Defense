using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour {

    public Text livesText;
    public PlayerStats playerStatsComp;

    private void Start() {
        playerStatsComp = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
    }
    // Update is called once per frame
    void Update () {
        livesText.text = "Vidas: " + playerStatsComp.lives.ToString();
	}
}
