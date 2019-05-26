using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    
    public fadeInScene fadeScenePanel;
    public string MainMenuScene = "MainMenu";
    public PlayerStats playerStatsComp;

    private void Start() {
        playerStatsComp = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
    }

    public void retry() {
        //Boton retry al tener GameOver, volvemos a cargar la escena actual
        //independientemente de la escena actual

        playerStatsComp.silverAmount += playerStatsComp.totalSilver;
        playerStatsComp.totalSilver = 0;
        playerStatsComp.Save();

        fadeScenePanel.fadeTo(SceneManager.GetActiveScene().name);
        playerStatsComp.reloadValues();
        //posibles problemas con la luz de la escena
        //en la ventana de lighting Window/lighting/settings
        //desactivar la opcion de global maps, Auto generate
        //reconstruir luces cada ves que se agregue algun elemento de luz
    }

    public void menu() {
        playerStatsComp.silverAmount += playerStatsComp.totalSilver;
        playerStatsComp.totalSilver = 0;
        playerStatsComp.Save();
        fadeScenePanel.fadeTo(MainMenuScene);
        playerStatsComp.reloadValues();
        playerStatsComp.justOne = true;
    }

}
