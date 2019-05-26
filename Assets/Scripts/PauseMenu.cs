using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseUI;
    public string MainMenuScene = "MainMenu";
    public GameObject fadeScenePanelGO;
    public fadeInScene fadeScenePanel;
    public GameObject increaseSpeed;
    public PlayerStats playerStatsComp;
    public WaveSpawner waveSpawnerComp;
    private GameObject playBtns;

    public GameObject PrincipalPausePanel;
    public GameObject OptionsPauseMenu;

    private void Awake() {
        playBtns = GameObject.Find("PlayButtons");

        GameObject tempMainMenuCanvas = GameObject.Find("MainMenuCanvas");
        increaseSpeed = GameObject.Find("PlayButtons");

        if (tempMainMenuCanvas != null) {
            for (int e = 0; e < tempMainMenuCanvas.transform.childCount; e++) {
                if (tempMainMenuCanvas.transform.GetChild(e).name.Contains("Scene")) {
                    fadeScenePanelGO = tempMainMenuCanvas.transform.GetChild(e).gameObject;
                    fadeScenePanelGO.SetActive(true);
                    fadeScenePanel = fadeScenePanelGO.GetComponent<fadeInScene>();
                }
            }
        }
        playerStatsComp = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        waveSpawnerComp = GameObject.Find("GameManager").GetComponent<WaveSpawner>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))  {
            pauseToggle();
        }
    }

    public void pauseToggle() {
        pauseUI.SetActive(!pauseUI.activeSelf);

        if (pauseUI.activeSelf) {
            //Paramos el juego o lo aceleramos/ralentizamos dependiendo del valor al que iguales timeScale
            //arreglar tambien Time.fixedDeltaTime para arreglar el como va el reloj del juego
            Time.timeScale = 0f;
            increaseSpeed.GetComponent<IncreaseSpeed>().pausedMenu = true;
        } else {
            Time.timeScale = 1f;
            increaseSpeed.GetComponent<IncreaseSpeed>().pausedMenu = false;
        }
    }

    public void retryLevel() {
        pauseToggle();
        fadeScenePanel.fadeTo(SceneManager.GetActiveScene().name);
        playerStatsComp.reloadValues();
    }

    public void menu() {
        pauseToggle();

        if (playBtns.GetComponent<IncreaseSpeed>().contadorDeIncTiempo >= 2) {
            playBtns.GetComponent<IncreaseSpeed>().contadorDeIncTiempo = 1;
        }

        playerStatsComp.justOne = true;
        fadeScenePanel.fadeTo(MainMenuScene);
        playerStatsComp.reloadValues();
    }

    public void OptionsBtn() {
        PrincipalPausePanel.gameObject.SetActive(false);
        OptionsPauseMenu.gameObject.SetActive(true);
    }

    public void backOptionsBtn() {
        PrincipalPausePanel.gameObject.SetActive(true);
        OptionsPauseMenu.gameObject.SetActive(false);
    }
}
