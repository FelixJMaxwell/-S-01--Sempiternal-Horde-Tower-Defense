using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* using BayatGames.SaveGameFree; */

public class PlayerStats : MonoBehaviour {

    [Header("Valores Persistentes")]
    //public float goldAmount = 0;
    public float silverAmount = 0;
    public string[] nivelesDesbloqueados;

    [Header("Valores reiniciables")]
    public float lives;
    //public float startGold = 5;
    public float startSilver = 450;
    public float multiplicadorDeDificultad = 0;
    public float userMultiplicadorDeDificultad = 0;
    public float startLives = 100;
    public float startMoney = 400;
    public float extraMoney;
    public float Money;
    public int cantidadHordas;
    public float playerScore;
    public int scoreUpdate = 0;
    private static bool playerStatsInstance = false;
    public GameObject playerStatsGO;
    public GameObject panelHabilidades;

    public float totalSilver;
    public bool justOne = false;
    public bool totalSilverOne = false;
    public GameObject desbloqueoComp;

    public float plataPorHorda = 6;
    private int customHeight;

    //Valores guardados para torretas
    public float[] valoresUnitarios;
    public float[] valoresUsuario;
    public float[] vTorretaCubo;
    public float[] vTorretaMG;
    public float[] vTorretaSniper;
    public float[] vTorretaTrapecio;
    public float[] vTorretaAoEBase;
    public float[] vTorretaAoEBuff;
    public float[] vTorretaAoEDmg;
    public float[] vTorretaAoESlow;
    public float[] vTorretaPesadaB;
    public float[] vTorretaCanon;
    public float[] vTorretaMortero;
    public float[] vTorretaMissileLauncher;

    public bool alternarZoom = false;
    public bool alternarMov = false;
    public float velocidadCamara = 0.01f;

    private int contador = 7;
    private int sumaPrevia = 0;

    public GameObject togZoom;
    public GameObject togMov;
    public GameObject sliderCamMov;

    private void Awake() {
        if (playerStatsInstance == false) {
            playerStatsInstance = true;
            DontDestroyOnLoad(playerStatsGO);
        } else {
            DestroyImmediate(playerStatsGO);
        }
    }

    private void OnApplicationQuit() {
        Save();
    }

    public void Save() {
        saveLoadManager.savePlayer(this);
    }

    void Start() {

        //cargar las otras torretas en playerstats para guardar y cargar valores
        valoresUnitarios = new float[100];
        valoresUsuario = new float[4];

        vTorretaCubo = new float[6];
        vTorretaMG = new float[6];
        vTorretaSniper = new float[6];
        vTorretaTrapecio = new float[6];
        vTorretaAoEBase = new float[6];
        vTorretaAoEBuff = new float[5];
        vTorretaAoEDmg = new float[5];
        vTorretaAoESlow = new float[5];
        vTorretaPesadaB = new float[6];
        vTorretaCanon = new float[6];
        vTorretaMortero = new float[6];
        vTorretaMissileLauncher = new float[7];

        float[] loadedFloatStats = saveLoadManager.loadFloatPlayer();

        if (loadedFloatStats != null) {
            //startGold = loadedFloatStats[0];
            startSilver = loadedFloatStats[1];
            if (loadedFloatStats[2] == 1) {
                nivelesDesbloqueados[0] = "Level01";
            }
            if (loadedFloatStats[3] == 2) {
                nivelesDesbloqueados[1] = "Level02";
            }
            if (loadedFloatStats[4] == 3) {
                nivelesDesbloqueados[2] = "Level03";
            }
            if (loadedFloatStats[5] == 4) {
                nivelesDesbloqueados[3] = "Level04";
            }
            if (loadedFloatStats[6] == 5) {
                nivelesDesbloqueados[4] = "Level05";
            }

            for (int i = 0; i < valoresUnitarios.Length; i++) {
                valoresUnitarios[i] = loadedFloatStats[ 7 + i ];
                contador++;
            }
            sumaPrevia = contador;
            for (int i = 0; i < valoresUsuario.Length; i++) {
                valoresUsuario[i] = loadedFloatStats[ sumaPrevia + i ];
                contador++;
            }
            sumaPrevia = contador;
            for (int i = 0; i < vTorretaCubo.Length; i++) {
                vTorretaCubo[i] = loadedFloatStats[sumaPrevia + i];
                contador++;
            }
            sumaPrevia = contador;
            for (int i = 0; i < vTorretaMG.Length; i++) {
                vTorretaMG[i] = loadedFloatStats[sumaPrevia + i];
                contador++;
            }
            sumaPrevia = contador;
            for (int i = 0; i < vTorretaSniper.Length; i++) {
                vTorretaSniper[i] = loadedFloatStats[sumaPrevia + i];
                contador++;
            }
            sumaPrevia = contador;
            for (int i = 0; i < vTorretaTrapecio.Length; i++) {
                vTorretaTrapecio[i] = loadedFloatStats[sumaPrevia + i];
                contador++;
            }
            sumaPrevia = contador;
            for (int i = 0; i < vTorretaAoEBase.Length; i++) {
                vTorretaAoEBase[i] = loadedFloatStats[sumaPrevia + i];
                contador++;
            }
            sumaPrevia = contador;
            for (int i = 0; i < vTorretaAoEBuff.Length; i++) {
                vTorretaAoEBuff[i] = loadedFloatStats[sumaPrevia + i];
                contador++;
            }
            sumaPrevia = contador;
            for (int i = 0; i < vTorretaAoEDmg.Length; i++) {
                vTorretaAoEDmg[i] = loadedFloatStats[sumaPrevia + i];
                contador++;
            }
            sumaPrevia = contador;
            for (int i = 0; i < vTorretaAoESlow.Length; i++) {
                vTorretaAoESlow[i] = loadedFloatStats[sumaPrevia + i];
                contador++;
            }
            sumaPrevia = contador;
            for (int i = 0; i < vTorretaPesadaB.Length; i++) {
                vTorretaPesadaB[i] = loadedFloatStats[sumaPrevia + i];
                contador++;
            }
            sumaPrevia = contador;
            for (int i = 0; i < vTorretaCanon.Length; i++) {
                vTorretaCanon[i] = loadedFloatStats[sumaPrevia + i];
                contador++;
            }
            sumaPrevia = contador;
            for (int i = 0; i < vTorretaMortero.Length; i++) {
                vTorretaMortero[i] = loadedFloatStats[sumaPrevia + i];
                contador++;
            }
            sumaPrevia = contador;
            for (int i = 0; i < vTorretaMissileLauncher.Length; i++) {
                vTorretaMissileLauncher[i] = loadedFloatStats[sumaPrevia + i];
                contador++;
            }
            sumaPrevia = 0;
            contador = 7;

            if (loadedFloatStats[180] == 0) {
                CameraController.alternarZoom = false;
                alternarZoom = false;
                if (togZoom != null) {
                    togZoom.GetComponent<Toggle>().isOn = false;
                }
            } else {
                CameraController.alternarZoom = true;
                alternarZoom = true;
                if (togZoom != null) {
                    togZoom.GetComponent<Toggle>().isOn = true;
                }
            }

            if (loadedFloatStats[181] == 0) {
                CameraController.alternarMov = false;
                alternarMov = false;
                if (togMov != null) {
                    togMov.GetComponent<Toggle>().isOn = false;
                }
            } else {
                CameraController.alternarMov = true;
                alternarMov = true;
                if (togMov != null) {
                    togMov.GetComponent<Toggle>().isOn = true;
                }
            }

            CameraController.newPanSpeed = loadedFloatStats[182];
            velocidadCamara = loadedFloatStats[182];
            if (sliderCamMov != null) {
                sliderCamMov.GetComponent<Slider>().value = loadedFloatStats[182];
            }
        }

        GameObject mainMenuCanvasTemp = GameObject.Find("MainMenuCanvas");

        if (SceneManager.GetActiveScene().name == "MainMenu") {
            for (int i = 0; i < mainMenuCanvasTemp.transform.childCount; i++) {
                if (mainMenuCanvasTemp.transform.GetChild(i).name.Contains("PanelHabilidades")) {
                    panelHabilidades = mainMenuCanvasTemp.transform.GetChild(i).gameObject;
                } else if (mainMenuCanvasTemp.transform.GetChild(i).name.Contains("PanelIniciarJuego")) {
                    if (mainMenuCanvasTemp.transform.GetChild(i).transform.gameObject.activeInHierarchy) {
                        customHeight = mainMenuCanvasTemp.transform.GetChild(i).transform.GetComponentInChildren<EasyGoogleMobileAds>().customHeight = Screen.height;
                    }
                }
            }
        }

        desbloqueoComp = GameObject.Find("MainMenu");

        multiplicadorDeDificultad = userMultiplicadorDeDificultad;
        //goldAmount = startGold;
        silverAmount = startSilver;

        startMoney += valoresUsuario[0];
        startLives += valoresUsuario[3];

        Money = startMoney;
        lives = startLives;
        playerScore = scoreUpdate;
        cantidadHordas = 0;
    }

    private void conseguirReferencia() {
        justOne = false;
        GameObject mainMenuCanvasTemp = GameObject.Find("MainMenuCanvas");
        for (int i = 0; i < mainMenuCanvasTemp.transform.childCount; i++) {
            if (mainMenuCanvasTemp.transform.GetChild(i).name.Contains("PanelHabilidades")) {
                panelHabilidades = mainMenuCanvasTemp.transform.GetChild(i).gameObject;
            } else if (mainMenuCanvasTemp.transform.GetChild(i).name.Contains("PanelIniciarJuego")) {
                if (mainMenuCanvasTemp.transform.GetChild(i).transform.gameObject.activeInHierarchy) {
                    customHeight = mainMenuCanvasTemp.transform.GetChild(i).transform.GetComponentInChildren<EasyGoogleMobileAds>().customHeight = Screen.height;
                }
            } else if (mainMenuCanvasTemp.transform.GetChild(i).name.Contains("PanelOptions")) {
                GameObject tempPanelOptions = mainMenuCanvasTemp.transform.GetChild(i).gameObject;
                for (int e = 0; e < tempPanelOptions.transform.childCount; e++) {
                    if (tempPanelOptions.transform.GetChild(e).name.Contains("OptionsMenu")) {
                        GameObject tempOptions = tempPanelOptions.transform.GetChild(e).gameObject;
                        GameObject tempExtra = tempOptions.transform.GetChild(0).gameObject;
                        GameObject temPIz = tempExtra.transform.GetChild(0).gameObject;
                        for (int o = 0; o < temPIz.transform.childCount; o++) {
                            if (temPIz.transform.GetChild(o).name.Contains("TCZ")) {
                                togZoom = temPIz.transform.GetChild(o).gameObject;
                            } else if (temPIz.transform.GetChild(o).name.Contains("TCM")) {
                                togMov = temPIz.transform.GetChild(o).gameObject;
                            } else if (temPIz.transform.GetChild(o).name.Contains("SCS")) {
                                sliderCamMov = temPIz.transform.GetChild(o).gameObject;
                            }
                        }
                    }
                }
            }
        }

        desbloqueoComp = GameObject.Find("MainMenu");
        desbloqueoComp.GetComponent<desbloqueoDeNiveles>().exitFor = false;

        Beacon.alreadyChecked = false;

        sliderCamMov.GetComponent<Slider>().value = velocidadCamara;

        if (alternarMov) {
            togMov.GetComponent<Toggle>().isOn = true;
        } else {
            togMov.GetComponent<Toggle>().isOn = false;
        }

        if (alternarZoom) {
            togZoom.GetComponent<Toggle>().isOn = true;
        } else {
            togZoom.GetComponent<Toggle>().isOn = false;
        }
    }

    private void Update() {

        if (SceneManager.GetActiveScene().name == "MainMenu" && justOne) {
            conseguirReferencia();
            Save();
        }

        if (GameManager.GameIsOver && totalSilverOne) {
            totalSilverOne = false;
            gainedSilver();
        }

        Money = Mathf.Clamp(Money, 0, Mathf.Infinity);
    }

    public void gainedSilver() {
        totalSilver = cantidadHordas * (plataPorHorda + valoresUsuario[2]);
    }

    public void reloadValues() {
        Money = startMoney;
        lives = startLives;
        playerScore = 0;
        cantidadHordas = 0;
    }

    public void alternaZoom(bool tog) {
        if (tog) {
            alternarZoom = true;
        } else {
            alternarZoom = false;
        }
    }

    public void alternaCameraMovement(bool tog) {
        if (tog) {
            alternarMov = true;
        } else {
            alternarMov = false;
        }
    }

    public void cameraSpeed(float nuevaVelocidad) {
        velocidadCamara = nuevaVelocidad;
    }
}
