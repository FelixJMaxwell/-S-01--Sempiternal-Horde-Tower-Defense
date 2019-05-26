using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour {

    public Text moneyText;
    public PlayerStats playerStatsComp;

    private void Start() {
        playerStatsComp = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
    }
    // Update is called once per frame
    void Update () {
        moneyText.text = "$" + playerStatsComp.Money.ToString();
	}
}
