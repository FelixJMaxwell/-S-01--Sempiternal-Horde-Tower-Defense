using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour {

    //en el inspector asignamos el objeto de text de la UI
    //donde se desplegara el numero de hordas sobrevividas
    public Text roundsText;
    public PlayerStats playerStatsComp;

    //metodo OnEnable() para cuando el objeto es activado
    //asi cambiamos sus propiedades en lugar de hacerlo en el metodo Start
    void OnEnable() {
        playerStatsComp = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        playerStatsComp.totalSilverOne = true;
        //asignamos la cantidad de hordas sobrevividas con la informacion
        //del PlayerStats.cs
        StartCoroutine(animatedNumOfWavesSurvived());
    }

    IEnumerator animatedNumOfWavesSurvived() {
        roundsText.text = "0";
        int round = 0;

        yield return new WaitForSeconds(.2f);

        while(round < playerStatsComp.cantidadHordas) {
            round++;
            roundsText.text = round.ToString();
            yield return new WaitForSeconds(0.01f);
        }
    }
}