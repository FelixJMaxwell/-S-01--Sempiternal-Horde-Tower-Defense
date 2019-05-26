using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animatedNumber : MonoBehaviour {

    public Text animatedText;
    public PlayerStats playerStatsComp;

    private void Start() {
        playerStatsComp = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
    }

    private void OnEnable() {
        StartCoroutine(animatedTextCount());
    }

    IEnumerator animatedTextCount() {
        animatedText.text = "0";
        float moneyGained = 0;

        yield return new WaitForSeconds(.1f);

        while (moneyGained < playerStatsComp.totalSilver) {
            moneyGained++;
            animatedText.text = moneyGained.ToString();
            yield return new WaitForSeconds(0.01f);
        }
    }
}