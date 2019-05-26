using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //elemento a ser tomado desde otros metodos para revisar si el juego a terminado
    public static bool GameIsOver;
    //en el inspector asignamos la parte UI que muestra la pantalla de final del juego
    //junto al score y cantidad de horas sobrevividas
    public GameObject gameOverUI;
    public PlayerStats playerStatsComp;

    //en el metodo start revisamos que GameIsOver sea falso la primera ves que iniciamos el juego
    private void Start() {
        playerStatsComp = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        GameIsOver = false;
    }

    // Update is called once per frame
    void Update () {

        if (GameIsOver) {
            return;
        }
        
		if(playerStatsComp.lives <= 0) {
            EndGame();
        }
	}

    void EndGame() {
        GameIsOver = true;
        if (gameOverUI != null) {
            gameOverUI.SetActive(true);
        }
    }
}
