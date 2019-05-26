using UnityEngine;

public class mainMenu : MonoBehaviour {

    public string levelToLoad = "LevelTest";
    public fadeInScene fadeScenePanel;
    public GameObject PanelPrincipal;
    public GameObject LevelSelectionPanel;
    public GameObject skillTreePanel;
    public GameObject optionsPanel;
    public GameObject GlobalCanvas;
    public PlayerStats playerStatsComp;

    private void Start() {
        playerStatsComp = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
    }

    public void play() {
        PanelPrincipal.SetActive(false);
        var canvasRenderMode = GlobalCanvas.GetComponent<Canvas>();
        canvasRenderMode.renderMode = RenderMode.WorldSpace;
        /*
        CameraControllerMainMenu.maxX = 0;
        CameraControllerMainMenu.minX = 0;
        CameraControllerMainMenu.maxY = 0;
        CameraControllerMainMenu.minY = -10f;
        */
        LevelSelectionPanel.SetActive(true);
    }

    public void skillTree() {
        PanelPrincipal.SetActive(false);
        skillTreePanel.SetActive(true);
    }

    public void backtoMenuStandart() {
        LevelSelectionPanel.SetActive(false);
        var canvasRenderMode = GlobalCanvas.GetComponent<Canvas>();
        canvasRenderMode.renderMode = RenderMode.ScreenSpaceCamera;
        CameraControllerMainMenu.maxX = 0;
        CameraControllerMainMenu.minX = 0;
        CameraControllerMainMenu.maxY = 0;
        CameraControllerMainMenu.minY = 0;
        PanelPrincipal.SetActive(true);
    }

    public void Options() {
        PanelPrincipal.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void skillTreeMenuBtn() {

        skillTree skillComp = skillTreePanel.GetComponent<skillTree>();

        //agreegar aqui las demas torretas para guardar valores



        skillTreePanel.SetActive(false);
        playerStatsComp.valoresUnitarios = skillComp.valoresUnitarios;
        playerStatsComp.valoresUsuario = skillComp.valoresUsuario;
        playerStatsComp.vTorretaCubo = skillComp.vTorretaCubo;
        playerStatsComp.vTorretaMG = skillComp.vTorretaMG;
        playerStatsComp.vTorretaSniper = skillComp.vTorretaSniper;
        playerStatsComp.vTorretaTrapecio = skillComp.vTorretaTrapecio;
        playerStatsComp.vTorretaAoEBase = skillComp.vTorretaAoEBase;
        playerStatsComp.vTorretaAoEBuff = skillComp.vTorretaAoEBuff;
        playerStatsComp.vTorretaAoEDmg = skillComp.vTorretaAoEDmg;
        playerStatsComp.vTorretaAoESlow = skillComp.vTorretaAoESlow;
        playerStatsComp.vTorretaPesadaB = skillComp.vTorretaPesadaB;
        playerStatsComp.vTorretaCanon = skillComp.vTorretaCanon;
        playerStatsComp.vTorretaMortero = skillComp.vTorretaMortero;
        playerStatsComp.vTorretaMissileLauncher = skillComp.vTorretaMissileLauncher;
        playerStatsComp.Save();
        PanelPrincipal.SetActive(true);
    }

    public void quit() {
        Debug.Log("Quit...");
        Application.Quit();
    }
}