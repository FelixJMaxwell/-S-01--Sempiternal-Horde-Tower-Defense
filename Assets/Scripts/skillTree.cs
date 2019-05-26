using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class skillTree : MonoBehaviour {

    public Button[] skillTreePanelBtns;
    public Text[] textosInformacion;
    public Sprite[] turretImgs;
    public TorretBlueprint[] torretas;
    

    ///SkillTreevalues
    /// <summary>
    /// 0.- mas Nivel para torretas
    /// 1.- mas experiencia
    /// 2.- reduccion costo upgrades    Limite: 90%
    /// 3.- fire rate
    /// 4.- rango                       Limite dependiendo de torreta
    /// 5.- mas da�o para torretas
    /// 6.- da�o por segundo
    /// 7.- ralentizacion               Limite 80%
    /// 8.- mas buff
    /// 9.- explosion range
    /// 10.- Mas dinero inicial
    /// 11.- Mas tiempo entre hordas
    /// 12.- Mas dinero de enemigos
    /// 13.- Conversion de score a silver
    /// 14.- Conversion de dinero sobrante a silver
    /// 15.- Mas vidas para el usuario
    /// </summary>
    //public float[,] skillTreeValues;
    public float[] costosBase;
    public float[] incremento;

    public Image imgTorreta;
    public int indice = 0;
    public PlayerStats playerStatsComp;
    public GameObject gameManagerGO;
    public GameObject mMenuGO;

    //Valores de torretas
    //Unitarios porq se suman por unidad, para hacer las multiplicaciones del costo al comprar mejoras
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

    private void Awake() {
        playerStatsComp = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        gameManagerGO = GameObject.Find("GameManager");
    }

    void Start() {
        indice = 0;

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

        //se igualan los valores de playerStats a los de skillTree
        //los de playerstats se cargan desde memoria del dispositivo
        //en caso de que se hayan guardado algun cambio
        valoresUnitarios = playerStatsComp.valoresUnitarios;
        valoresUsuario = playerStatsComp.valoresUsuario;
        vTorretaCubo = playerStatsComp.vTorretaCubo;
        vTorretaMG = playerStatsComp.vTorretaMG;
        vTorretaSniper = playerStatsComp.vTorretaSniper;
        vTorretaTrapecio = playerStatsComp.vTorretaTrapecio;
        vTorretaAoEBase = playerStatsComp.vTorretaAoEBase;
        vTorretaAoEBuff = playerStatsComp.vTorretaAoEBuff;
        vTorretaAoEDmg = playerStatsComp.vTorretaAoEDmg;
        vTorretaAoESlow = playerStatsComp.vTorretaAoESlow;
        vTorretaPesadaB = playerStatsComp.vTorretaPesadaB;
        vTorretaCanon = playerStatsComp.vTorretaCanon;
        vTorretaMortero = playerStatsComp.vTorretaMortero;
        vTorretaMissileLauncher = playerStatsComp.vTorretaMissileLauncher;

        //skillTreeValues = new float[20, 20];
        textosInformacion[0].text = "Mejoras para el usuario";
        textosInformacion[6].text = "Dinero en la partida: $" + (playerStatsComp.startMoney + valoresUsuario[0]);
        textosInformacion[7].text = "Dinero de enemigos: +" + valoresUsuario[1] + "%";

        if (valoresUsuario[2] == 0) {
            textosInformacion[8].text = "Plata ganada por horda: $" + playerStatsComp.plataPorHorda;
        } else {
            textosInformacion[8].text = "Plata ganada por horda: $" + (playerStatsComp.plataPorHorda + valoresUsuario[2]);
        }

        textosInformacion[1].text = "Mas vidas: " + (playerStatsComp.startLives + valoresUsuario[3]);

        skillTreePanelBtns[2].GetComponentInChildren<Text>().text = "Comprar mejora\nCosto: " + Mathf.Floor(((valoresUsuario[0] * incremento[10] + 1) - incremento[10]) * costosBase[10]);
        skillTreePanelBtns[3].GetComponentInChildren<Text>().text = "Comprar mejora\nCosto: " + Mathf.Floor(((valoresUsuario[1] * incremento[12] + 1) - incremento[12]) * costosBase[12]);
        skillTreePanelBtns[4].GetComponentInChildren<Text>().text = "Comprar mejora\nCosto: " + Mathf.Floor(((valoresUsuario[2] * incremento[14] + 1) - incremento[14]) * costosBase[14]);
        skillTreePanelBtns[5].GetComponentInChildren<Text>().text = "Comprar mejora\nCosto: " + Mathf.Floor(((valoresUsuario[3] * incremento[15] + 1) - incremento[15]) * costosBase[15]);
    }

    // Update is called once per frame
    void Update() {

        //textosInformacion[11].text = "Oro: " + playerStatsComp.goldAmount.ToString();
        textosInformacion[12].text = "Plata: " + playerStatsComp.silverAmount.ToString();
        
        if (indice == 0) {
            skillTreePanelBtns[0].interactable = false;
        } else if (indice == 12) {
            skillTreePanelBtns[1].interactable = false;
        } else {
            skillTreePanelBtns[0].interactable = true;
            skillTreePanelBtns[1].interactable = true;
        }

        if (indice == 0) {
            imgTorreta.gameObject.SetActive(false);
            textosInformacion[13].gameObject.SetActive(true);
            textosInformacion[13].text = "I";
        } else if (indice == 1) {
            imgTorreta.gameObject.SetActive(true);
            textosInformacion[13].gameObject.SetActive(false);
            imgTorreta.sprite = turretImgs[4];
        } else if (indice == 2) {
            imgTorreta.gameObject.SetActive(true);
            textosInformacion[13].gameObject.SetActive(false);
            imgTorreta.sprite = turretImgs[5];
        } else if (indice == 3) {
            imgTorreta.gameObject.SetActive(true);
            textosInformacion[13].gameObject.SetActive(false);
            imgTorreta.sprite = turretImgs[6];
        } else if (indice == 4) {
            imgTorreta.gameObject.SetActive(true);
            textosInformacion[13].gameObject.SetActive(false);
            imgTorreta.sprite = turretImgs[7];
        } else if (indice == 5) {
            imgTorreta.gameObject.SetActive(true);
            textosInformacion[13].gameObject.SetActive(false);
            imgTorreta.sprite = turretImgs[0];
        } else if (indice == 6) {
            imgTorreta.gameObject.SetActive(true);
            textosInformacion[13].gameObject.SetActive(false);
            imgTorreta.sprite = turretImgs[1];
        } else if (indice == 7) {
            imgTorreta.gameObject.SetActive(true);
            textosInformacion[13].gameObject.SetActive(false);
            imgTorreta.sprite = turretImgs[2];
        } else if (indice == 8) {
            imgTorreta.gameObject.SetActive(true);
            textosInformacion[13].gameObject.SetActive(false);
            imgTorreta.sprite = turretImgs[3];
        } else if (indice == 9) {
            imgTorreta.gameObject.SetActive(true);
            textosInformacion[13].gameObject.SetActive(false);
            imgTorreta.sprite = turretImgs[11];
        } else if (indice == 10) {
            imgTorreta.gameObject.SetActive(true);
            textosInformacion[13].gameObject.SetActive(false);
            imgTorreta.sprite = turretImgs[9];
        } else if (indice == 11) {
            imgTorreta.gameObject.SetActive(true);
            textosInformacion[13].gameObject.SetActive(false);
            imgTorreta.sprite = turretImgs[10];
        } else if (indice == 12) {
            imgTorreta.gameObject.SetActive(true);
            textosInformacion[13].gameObject.SetActive(false);
            imgTorreta.sprite = turretImgs[8];
        }

    }

    public void navegacionTienda() {
        string btnPresionado = EventSystem.current.currentSelectedGameObject.name;

        if (btnPresionado == skillTreePanelBtns[0].name) {
            ///ARRIBA ARRIBA ARRIBA ARRIBA ARRIBA ARRIBA ARRIBA 
            ///ARRIBA ARRIBA ARRIBA ARRIBA ARRIBA ARRIBA ARRIBA 
            ///ARRIBA ARRIBA ARRIBA ARRIBA ARRIBA ARRIBA ARRIBA 
            ///ARRIBA ARRIBA ARRIBA ARRIBA ARRIBA ARRIBA ARRIBA 
            indice -= 1;

            } else {
            ///ABAJO ABAJO ABAJO ABAJO ABAJO ABAJO ABAJO ABAJO
            ///ABAJO ABAJO ABAJO ABAJO ABAJO ABAJO ABAJO ABAJO
            ///ABAJO ABAJO ABAJO ABAJO ABAJO ABAJO ABAJO ABAJO
            ///ABAJO ABAJO ABAJO ABAJO ABAJO ABAJO ABAJO ABAJO
            ///ABAJO ABAJO ABAJO ABAJO ABAJO ABAJO ABAJO ABAJO

            indice += 1;
        }

        if (indice == 0) {
            //mejoras para el usuario
            textosInformacion[0].text = "Mejoras para el usuario";

            textosInformacion[2].gameObject.SetActive(false);
            textosInformacion[3].gameObject.SetActive(false);

            textosInformacion[6].text = "Dinero en la partida: $" + (playerStatsComp.startMoney + valoresUsuario[0]);
            textosInformacion[7].text = "Dinero de enemigos: +" + valoresUsuario[1] + "%";
            if (valoresUsuario[2] == 0) {
                textosInformacion[8].text = "Plata ganada por horda: $" + playerStatsComp.plataPorHorda;
            } else {
                textosInformacion[8].text = "Plata ganada por horda: $" + playerStatsComp.plataPorHorda + valoresUsuario[2];
            }
            textosInformacion[1].text = "Mas vidas: " + playerStatsComp.startLives + " +" + valoresUsuario[3];

            skillTreePanelBtns[6].gameObject.SetActive(false);
            skillTreePanelBtns[10].gameObject.SetActive(false);

            skillTreePanelBtns[2].GetComponentInChildren<Text>().text = "Comprar mejora\nCosto: " + Mathf.Floor(((valoresUnitarios[0] * incremento[10] + 1) - incremento[10]) * costosBase[10]);
            skillTreePanelBtns[3].GetComponentInChildren<Text>().text = "Comprar mejora\nCosto: " + Mathf.Floor(((valoresUnitarios[1] * incremento[12] + 1) - incremento[12]) * costosBase[12]);
            skillTreePanelBtns[4].GetComponentInChildren<Text>().text = "Comprar mejora\nCosto: " + Mathf.Floor(((valoresUnitarios[2] * incremento[14] + 1) - incremento[14]) * costosBase[14]);
            skillTreePanelBtns[5].GetComponentInChildren<Text>().text = "Comprar mejora\nCosto: " + Mathf.Floor(((valoresUnitarios[3] * incremento[15] + 1) - incremento[15]) * costosBase[15]);
        }

        if (indice >= 1) {
            Turret torretaComp = torretas[indice - 1].prefab.GetComponent<Turret>();

            if (indice == 1) {
                //Torreta Cubo 
                textosInformacion[0].text = torretaComp.nombreTorreta;

                textosInformacion[2].gameObject.SetActive(true);
                textosInformacion[3].gameObject.SetActive(true);

                textosInformacion[1].text = "Daño de la torreta: " + (torretaComp.startBulletDamage + vTorretaCubo[3]);
                textosInformacion[2].text = "Rango de la torreta: " + (torretaComp.startRango + vTorretaCubo[5]);
                textosInformacion[3].text = "Cadencia de tiro: " + (torretaComp.startFireRate + vTorretaCubo[4]) + "/Seg";
                textosInformacion[6].text = "Nivel actual: " + vTorretaCubo[0];
                textosInformacion[7].text = "Experiencia extra: " + vTorretaCubo[1] + "%";
                textosInformacion[8].text = "Reducir precio de mejoras: " + vTorretaCubo[2] + "%";

                skillTreePanelBtns[6].gameObject.SetActive(true);
                skillTreePanelBtns[10].gameObject.SetActive(true);

                skillTreePanelBtns[2].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[4] * incremento[0] + 1) - incremento[0]) * costosBase[0]);
                skillTreePanelBtns[3].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[5] * incremento[1] + 1) - incremento[1]) * costosBase[1]);
                skillTreePanelBtns[4].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[6] * incremento[2] + 1) - incremento[2]) * costosBase[2]);
                skillTreePanelBtns[5].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[7] * incremento[5] + 1) - incremento[5]) * costosBase[5]);
                skillTreePanelBtns[6].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[8] * incremento[3] + 1) - incremento[3]) * costosBase[3]);
                skillTreePanelBtns[10].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[9] * incremento[4] + 1) - incremento[4]) * costosBase[4]);
            } else if (indice == 2) {
                //Torreta MG
                textosInformacion[0].text = torretaComp.nombreTorreta;

                textosInformacion[1].text = "Daño de la torreta: " + (torretaComp.startBulletDamage + vTorretaMG[3]);
                textosInformacion[2].text = "Rango de la torreta: " + (torretaComp.startRango + vTorretaMG[5]);
                textosInformacion[3].text = "Cadencia de tiro: " + (torretaComp.startFireRate + vTorretaMG[4]) + "/Seg";
                textosInformacion[6].text = "Nivel actual: " + vTorretaMG[0];
                textosInformacion[7].text = "Experiencia extra: " + vTorretaMG[1] + "%";
                textosInformacion[8].text = "Reducir precio de mejoras: " + vTorretaMG[2] + "%";

                skillTreePanelBtns[2].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[10] * incremento[0] + 1) - incremento[0]) * costosBase[0]);
                skillTreePanelBtns[3].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[11] * incremento[1] + 1) - incremento[1]) * costosBase[1]);
                skillTreePanelBtns[4].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[12] * incremento[2] + 1) - incremento[2]) * costosBase[2]);
                skillTreePanelBtns[5].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[13] * incremento[5] + 1) - incremento[5]) * costosBase[5]);
                skillTreePanelBtns[6].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[14] * incremento[3] + 1) - incremento[3]) * costosBase[3]);
                skillTreePanelBtns[10].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[15] * incremento[4] + 1) - incremento[4]) * costosBase[4]);
            } else if (indice == 3) {
                //Torreta sniper
                textosInformacion[0].text = torretaComp.nombreTorreta;

                textosInformacion[1].text = "Daño de la torreta: " + (torretaComp.startBulletDamage + vTorretaSniper[3]);
                textosInformacion[2].text = "Rango de la torreta: " + (torretaComp.startRango + vTorretaSniper[5]);
                textosInformacion[3].text = "Cadencia de tiro: " + (torretaComp.startFireRate + vTorretaSniper[4]) + "/Seg";
                textosInformacion[6].text = "Nivel actual: " + vTorretaSniper[0];
                textosInformacion[7].text = "Experiencia extra: " + vTorretaSniper[1] + "%";
                textosInformacion[8].text = "Reducir precio de mejoras: " + vTorretaSniper[2] + "%";

                skillTreePanelBtns[2].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[16] * incremento[0] + 1) - incremento[0]) * costosBase[0]);
                skillTreePanelBtns[3].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[17] * incremento[1] + 1) - incremento[1]) * costosBase[1]);
                skillTreePanelBtns[4].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[18] * incremento[2] + 1) - incremento[2]) * costosBase[2]);
                skillTreePanelBtns[5].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[19] * incremento[5] + 1) - incremento[5]) * costosBase[5]);
                skillTreePanelBtns[6].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[20] * incremento[3] + 1) - incremento[3]) * costosBase[3]);
                skillTreePanelBtns[10].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[21] * incremento[4] + 1) - incremento[4]) * costosBase[4]);
            } else if (indice == 4) {
                //Torreta Trapecio
                textosInformacion[0].text = torretaComp.nombreTorreta;

                textosInformacion[1].gameObject.SetActive(true);
                textosInformacion[3].gameObject.SetActive(true);
                textosInformacion[4].gameObject.SetActive(false);
                textosInformacion[5].gameObject.SetActive(false);

                textosInformacion[1].text = "Daño de la torreta: " + (torretaComp.startBulletDamage + vTorretaTrapecio[3]);
                textosInformacion[2].text = "Rango de la torreta: " + (torretaComp.startRango + vTorretaTrapecio[5]);
                textosInformacion[3].text = "Cadencia de tiro: " + (torretaComp.startFireRate + vTorretaTrapecio[4]) + "/Seg";
                textosInformacion[6].text = "Nivel actual: " + vTorretaTrapecio[0];
                textosInformacion[7].text = "Experiencia extra: " + vTorretaTrapecio[1] + "%";
                textosInformacion[8].text = "Reducir precio de mejoras: " + vTorretaTrapecio[2] + "%";

                skillTreePanelBtns[5].gameObject.SetActive(true);
                skillTreePanelBtns[6].gameObject.SetActive(true);
                skillTreePanelBtns[7].gameObject.SetActive(false);
                skillTreePanelBtns[8].gameObject.SetActive(false);

                skillTreePanelBtns[2].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[22] * incremento[0] + 1) - incremento[0]) * costosBase[0]);
                skillTreePanelBtns[3].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[23] * incremento[1] + 1) - incremento[1]) * costosBase[1]);
                skillTreePanelBtns[4].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[24] * incremento[2] + 1) - incremento[2]) * costosBase[2]);
                skillTreePanelBtns[5].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[25] * incremento[5] + 1) - incremento[5]) * costosBase[5]);
                skillTreePanelBtns[6].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[26] * incremento[3] + 1) - incremento[3]) * costosBase[3]);
                skillTreePanelBtns[10].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[27] * incremento[4] + 1) - incremento[4]) * costosBase[4]);
            } else if (indice == 5) {
                //Torreta AoE Base
                textosInformacion[0].text = torretaComp.nombreTorreta;

                textosInformacion[1].gameObject.SetActive(false);
                textosInformacion[2].gameObject.SetActive(true);
                textosInformacion[3].gameObject.SetActive(false);
                textosInformacion[4].gameObject.SetActive(true);
                textosInformacion[5].gameObject.SetActive(true);
                textosInformacion[9].gameObject.SetActive(false);

                textosInformacion[2].text = "Rango de la torreta: " + (torretaComp.startRango + vTorretaAoEBase[5]);
                textosInformacion[4].text = "Daño por segundo: " + (torretaComp.startDanioPorSegundo + vTorretaAoEBase[3]) + "/seg";
                textosInformacion[5].text = "Porcentaje de ralentizacion: " + (torretaComp.StartPorcentajeDeRalentizacion + vTorretaAoEBase[4]) + "/Seg";
                textosInformacion[6].text = "Nivel actual: " + vTorretaAoEBase[0];
                textosInformacion[7].text = "Experiencia extra: " + vTorretaAoEBase[1] + "%";
                textosInformacion[8].text = "Reducir precio de mejoras: " + vTorretaAoEBase[2] + "%";

                skillTreePanelBtns[5].gameObject.SetActive(false);
                skillTreePanelBtns[6].gameObject.SetActive(false);
                skillTreePanelBtns[7].gameObject.SetActive(true);
                skillTreePanelBtns[8].gameObject.SetActive(true);
                skillTreePanelBtns[9].gameObject.SetActive(false);
                skillTreePanelBtns[10].gameObject.SetActive(true);
                skillTreePanelBtns[11].gameObject.SetActive(false);

                skillTreePanelBtns[2].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[28] * incremento[0] + 1) - incremento[0]) * costosBase[0]);
                skillTreePanelBtns[3].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[29] * incremento[1] + 1) - incremento[1]) * costosBase[1]);
                skillTreePanelBtns[4].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[30] * incremento[2] + 1) - incremento[2]) * costosBase[2]);
                skillTreePanelBtns[7].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[31] * incremento[6] + 1) - incremento[6]) * costosBase[6]);
                skillTreePanelBtns[8].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[32] * incremento[7] + 1) - incremento[7]) * costosBase[7]);
                skillTreePanelBtns[10].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[33] * incremento[4] + 1) - incremento[4]) * costosBase[4]);
            } else if (indice == 6) {
                //Torreta AoEBuff
                textosInformacion[0].text = torretaComp.nombreTorreta;

                textosInformacion[1].gameObject.SetActive(false);
                textosInformacion[2].gameObject.SetActive(true);
                textosInformacion[3].gameObject.SetActive(false);
                textosInformacion[4].gameObject.SetActive(false);
                textosInformacion[5].gameObject.SetActive(false);
                textosInformacion[9].gameObject.SetActive(true);
                textosInformacion[10].gameObject.SetActive(false);

                textosInformacion[2].text = "Rango de la torreta: " + (torretaComp.startRango + vTorretaAoEBuff[4]);
                textosInformacion[6].text = "Nivel actual: " + vTorretaAoEBuff[0];
                textosInformacion[7].text = "Experiencia extra: " + vTorretaAoEBuff[1] + "%";
                textosInformacion[8].text = "Reducir precio de mejoras: " + vTorretaAoEBuff[2] + "%";
                textosInformacion[9].text = "Mas daño para torretas: " + (torretaComp.startExtraDmgForBulletsWithBuff + vTorretaAoEBuff[3]) + "%";

                skillTreePanelBtns[5].gameObject.SetActive(false);
                skillTreePanelBtns[6].gameObject.SetActive(false);
                skillTreePanelBtns[7].gameObject.SetActive(false);
                skillTreePanelBtns[8].gameObject.SetActive(false);
                skillTreePanelBtns[9].gameObject.SetActive(true);
                skillTreePanelBtns[10].gameObject.SetActive(true);
                skillTreePanelBtns[11].gameObject.SetActive(false);

                skillTreePanelBtns[2].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[34] * incremento[0] + 1) - incremento[0]) * costosBase[0]);
                skillTreePanelBtns[3].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[35] * incremento[1] + 1) - incremento[1]) * costosBase[1]);
                skillTreePanelBtns[4].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[36] * incremento[2] + 1) - incremento[2]) * costosBase[2]);
                skillTreePanelBtns[9].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[37] * incremento[8] + 1) - incremento[8]) * costosBase[8]);
                skillTreePanelBtns[10].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[38] * incremento[4] + 1) - incremento[4]) * costosBase[4]);
            } else if (indice == 7) {
                //Torreta AoEDmg
                textosInformacion[0].text = torretaComp.nombreTorreta;

                textosInformacion[1].gameObject.SetActive(false);
                textosInformacion[2].gameObject.SetActive(true);
                textosInformacion[3].gameObject.SetActive(false);
                textosInformacion[4].gameObject.SetActive(true);
                textosInformacion[5].gameObject.SetActive(false);
                textosInformacion[9].gameObject.SetActive(false);
                textosInformacion[10].gameObject.SetActive(false);

                textosInformacion[2].text = "Rango de la torreta: " + (torretaComp.startRango + vTorretaAoEDmg[4]);
                textosInformacion[4].text = "Daño por segundo: " + (torretaComp.startDanioPorSegundo + vTorretaAoEDmg[3]) + "/Seg";
                textosInformacion[6].text = "Nivel actual: " + vTorretaAoEDmg[0];
                textosInformacion[7].text = "Experiencia extra: " + vTorretaAoEDmg[1] + "%";
                textosInformacion[8].text = "Reducir precio de mejoras: " + vTorretaAoEDmg[2] + "%";

                skillTreePanelBtns[5].gameObject.SetActive(false);
                skillTreePanelBtns[6].gameObject.SetActive(false);
                skillTreePanelBtns[7].gameObject.SetActive(true);
                skillTreePanelBtns[8].gameObject.SetActive(false);
                skillTreePanelBtns[9].gameObject.SetActive(false);
                skillTreePanelBtns[10].gameObject.SetActive(true);
                skillTreePanelBtns[11].gameObject.SetActive(false);

                skillTreePanelBtns[2].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[39] * incremento[0] + 1) - incremento[0]) * costosBase[0]);
                skillTreePanelBtns[3].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[40] * incremento[1] + 1) - incremento[1]) * costosBase[1]);
                skillTreePanelBtns[4].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[41] * incremento[2] + 1) - incremento[2]) * costosBase[2]);
                skillTreePanelBtns[9].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[42] * incremento[8] + 1) - incremento[8]) * costosBase[8]);
                skillTreePanelBtns[10].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[43] * incremento[4] + 1) - incremento[4]) * costosBase[4]);
            } else if (indice == 8) {
                //torreta aoeSlow
                textosInformacion[0].text = torretaComp.nombreTorreta;

                textosInformacion[1].gameObject.SetActive(false);
                textosInformacion[2].gameObject.SetActive(true);
                textosInformacion[3].gameObject.SetActive(false);
                textosInformacion[4].gameObject.SetActive(false);
                textosInformacion[5].gameObject.SetActive(true);
                textosInformacion[9].gameObject.SetActive(false);
                textosInformacion[10].gameObject.SetActive(false);

                textosInformacion[2].text = "Rango de la torreta: " + (torretaComp.startRango + vTorretaAoESlow[4]);
                textosInformacion[5].text = "Porcentaje de ralentizacion: " + (torretaComp.StartPorcentajeDeRalentizacion + vTorretaAoESlow[3]) + "/%";
                textosInformacion[6].text = "Nivel actual: " + vTorretaAoESlow[0];
                textosInformacion[7].text = "Experiencia extra: " + vTorretaAoESlow[1] + "%";
                textosInformacion[8].text = "Reducir precio de mejoras: " + vTorretaAoESlow[2] + "%";

                skillTreePanelBtns[5].gameObject.SetActive(false);
                skillTreePanelBtns[6].gameObject.SetActive(false);
                skillTreePanelBtns[7].gameObject.SetActive(false);
                skillTreePanelBtns[8].gameObject.SetActive(true);
                skillTreePanelBtns[9].gameObject.SetActive(false);
                skillTreePanelBtns[10].gameObject.SetActive(true);
                skillTreePanelBtns[11].gameObject.SetActive(false);

                skillTreePanelBtns[2].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[44] * incremento[0] + 1) - incremento[0]) * costosBase[0]);
                skillTreePanelBtns[3].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[45] * incremento[1] + 1) - incremento[1]) * costosBase[1]);
                skillTreePanelBtns[4].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[46] * incremento[2] + 1) - incremento[2]) * costosBase[2]);
                skillTreePanelBtns[8].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[47] * incremento[7] + 1) - incremento[7]) * costosBase[7]);
                skillTreePanelBtns[10].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[48] * incremento[4] + 1) - incremento[4]) * costosBase[4]);
            } else if (indice == 9) {
                //torreta pesada
                textosInformacion[0].text = torretaComp.nombreTorreta;

                textosInformacion[1].gameObject.SetActive(true);
                textosInformacion[2].gameObject.SetActive(true);
                textosInformacion[3].gameObject.SetActive(true);
                textosInformacion[4].gameObject.SetActive(false);
                textosInformacion[5].gameObject.SetActive(false);
                textosInformacion[6].gameObject.SetActive(true);
                textosInformacion[7].gameObject.SetActive(true);
                textosInformacion[8].gameObject.SetActive(true);
                textosInformacion[9].gameObject.SetActive(false);
                textosInformacion[10].gameObject.SetActive(false);

                textosInformacion[1].text = "Daño de la torreta: " + (torretaComp.startBulletDamage + vTorretaPesadaB[3]);
                textosInformacion[2].text = "Rango de la torreta: " + (torretaComp.startRango + vTorretaPesadaB[5]);
                textosInformacion[3].text = "Cadencia de tiro: " + (torretaComp.startFireRate + vTorretaPesadaB[4]) + "/Seg";
                textosInformacion[6].text = "Nivel actual: " + vTorretaPesadaB[0];
                textosInformacion[7].text = "Experiencia extra: " + vTorretaPesadaB[1] + "%";
                textosInformacion[8].text = "Reducir precio de mejoras: " + vTorretaPesadaB[2] + "%";

                skillTreePanelBtns[2].gameObject.SetActive(true);
                skillTreePanelBtns[3].gameObject.SetActive(true);
                skillTreePanelBtns[4].gameObject.SetActive(true);
                skillTreePanelBtns[5].gameObject.SetActive(true);
                skillTreePanelBtns[6].gameObject.SetActive(true);
                skillTreePanelBtns[7].gameObject.SetActive(false);
                skillTreePanelBtns[8].gameObject.SetActive(false);
                skillTreePanelBtns[9].gameObject.SetActive(false);
                skillTreePanelBtns[10].gameObject.SetActive(true);
                skillTreePanelBtns[11].gameObject.SetActive(false);

                skillTreePanelBtns[2].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[49] * incremento[0] + 1) - incremento[0]) * costosBase[0]);
                skillTreePanelBtns[3].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[50] * incremento[1] + 1) - incremento[1]) * costosBase[1]);
                skillTreePanelBtns[4].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[51] * incremento[2] + 1) - incremento[2]) * costosBase[2]);
                skillTreePanelBtns[5].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[52] * incremento[5] + 1) - incremento[5]) * costosBase[5]);
                skillTreePanelBtns[6].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[53] * incremento[3] + 1) - incremento[3]) * costosBase[3]);
                skillTreePanelBtns[10].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[54] * incremento[4] + 1) - incremento[4]) * costosBase[4]);
            } else if (indice == 10) {
                //torreta Cañon
                textosInformacion[0].text = torretaComp.nombreTorreta;

                textosInformacion[1].text = "Daño de la torreta: " + (torretaComp.startBulletDamage + vTorretaCanon[3]);
                textosInformacion[2].text = "Rango de la torreta: " + (torretaComp.startRango + vTorretaCanon[5]);
                textosInformacion[3].text = "Cadencia de tiro: " + (torretaComp.startFireRate + vTorretaCanon[4]) + "/Seg";
                textosInformacion[6].text = "Nivel actual: " + vTorretaCanon[0];
                textosInformacion[7].text = "Experiencia extra: " + vTorretaCanon[1] + "%";
                textosInformacion[8].text = "Reducir precio de mejoras: " + vTorretaCanon[2] + "%";

                skillTreePanelBtns[2].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[55] * incremento[0] + 1) - incremento[0]) * costosBase[0]);
                skillTreePanelBtns[3].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[56] * incremento[1] + 1) - incremento[1]) * costosBase[1]);
                skillTreePanelBtns[4].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[57] * incremento[2] + 1) - incremento[2]) * costosBase[2]);
                skillTreePanelBtns[5].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[58] * incremento[5] + 1) - incremento[5]) * costosBase[5]);
                skillTreePanelBtns[6].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[59] * incremento[3] + 1) - incremento[3]) * costosBase[3]);
                skillTreePanelBtns[10].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[60] * incremento[4] + 1) - incremento[4]) * costosBase[4]);
            } else if (indice == 11) {
                //torreta mortero
                textosInformacion[0].text = torretaComp.nombreTorreta;

                textosInformacion[2].gameObject.SetActive(false);
                textosInformacion[4].gameObject.SetActive(false);
                textosInformacion[5].gameObject.SetActive(false);
                textosInformacion[9].gameObject.SetActive(false);
                textosInformacion[10].gameObject.SetActive(true);

                textosInformacion[1].fontSize = 18;
                textosInformacion[3].fontSize = 18;

                textosInformacion[1].text = "Daño de la torreta: " + (torretaComp.startBulletDamage + vTorretaMortero[3]);
                textosInformacion[3].text = "Cadencia de tiro: " + (torretaComp.startFireRate + vTorretaMortero[4]) + "/Seg";
                textosInformacion[6].text = "Nivel actual: " + vTorretaMortero[0];
                textosInformacion[7].text = "Experiencia extra: " + vTorretaMortero[1] + "%";
                textosInformacion[8].text = "Reducir precio de mejoras: " + vTorretaMortero[2] + "%";
                textosInformacion[10].text = "Rango de explosion: " + (torretaComp.startExplosionRadiusBullet + vTorretaMortero[5]);

                skillTreePanelBtns[10].gameObject.SetActive(false);
                skillTreePanelBtns[11].gameObject.SetActive(true);

                skillTreePanelBtns[2].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[61] * incremento[0] + 1) - incremento[0]) * costosBase[0]);
                skillTreePanelBtns[3].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[62] * incremento[1] + 1) - incremento[1]) * costosBase[1]);
                skillTreePanelBtns[4].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[63] * incremento[2] + 1) - incremento[2]) * costosBase[2]);
                skillTreePanelBtns[5].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[64] * incremento[5] + 1) - incremento[5]) * costosBase[5]);
                skillTreePanelBtns[6].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[65] * incremento[3] + 1) - incremento[3]) * costosBase[3]);
                skillTreePanelBtns[11].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[66] * incremento[9] + 1) - incremento[9]) * costosBase[9]);
            } else if (indice == 12) {
                //torreta missile
                textosInformacion[0].text = torretaComp.nombreTorreta;

                textosInformacion[1].gameObject.SetActive(true);
                textosInformacion[2].gameObject.SetActive(true);
                textosInformacion[3].gameObject.SetActive(true);
                textosInformacion[4].gameObject.SetActive(false);
                textosInformacion[5].gameObject.SetActive(false);
                textosInformacion[9].gameObject.SetActive(false);
                textosInformacion[10].gameObject.SetActive(true);

                textosInformacion[1].fontSize = 18;
                textosInformacion[3].fontSize = 18;

                textosInformacion[1].text = "Daño de la torreta: " + (torretaComp.startBulletDamage + vTorretaMissileLauncher[3]);
                textosInformacion[2].text = "Rango de la torreta: " + (torretaComp.startRango + vTorretaMissileLauncher[6]);
                textosInformacion[3].text = "Cadencia de tiro: " + (torretaComp.startFireRate + vTorretaMissileLauncher[4]) + "/Seg";
                textosInformacion[6].text = "Nivel actual: " + vTorretaMissileLauncher[0];
                textosInformacion[7].text = "Experiencia extra: " + vTorretaMissileLauncher[1] + "%";
                textosInformacion[8].text = "Reducir precio de mejoras: " + vTorretaMissileLauncher[2] + "%";
                textosInformacion[10].text = "Rango de explosion: " + (torretaComp.startExplosionRadiusBullet + vTorretaMissileLauncher[5]);

                skillTreePanelBtns[5].gameObject.SetActive(true);
                skillTreePanelBtns[6].gameObject.SetActive(true);
                skillTreePanelBtns[7].gameObject.SetActive(false);
                skillTreePanelBtns[8].gameObject.SetActive(false);
                skillTreePanelBtns[9].gameObject.SetActive(false);
                skillTreePanelBtns[10].gameObject.SetActive(true);
                skillTreePanelBtns[11].gameObject.SetActive(true);

                skillTreePanelBtns[2].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[67] * incremento[0] + 1) - incremento[0]) * costosBase[0]);
                skillTreePanelBtns[3].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[68] * incremento[1] + 1) - incremento[1]) * costosBase[1]);
                skillTreePanelBtns[4].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[69] * incremento[2] + 1) - incremento[2]) * costosBase[2]);
                skillTreePanelBtns[5].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[70] * incremento[5] + 1) - incremento[5]) * costosBase[5]);
                skillTreePanelBtns[6].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[71] * incremento[3] + 1) - incremento[3]) * costosBase[3]);
                skillTreePanelBtns[10].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[72] * incremento[4] + 1) - incremento[4]) * costosBase[4]);
                skillTreePanelBtns[11].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((valoresUnitarios[73] * incremento[9] + 1) - incremento[9]) * costosBase[9]);
            }
        }
    }

    public void comprarMejora() {
        string btnName = EventSystem.current.currentSelectedGameObject.name;
        if (indice == 0 && btnName == "ActualLevelBtn") {
            //dinero en la partida
            float costo = Mathf.Floor(((valoresUnitarios[0] * incremento[10] + 1) - incremento[10]) * costosBase[10]);
            if (playerStatsComp.silverAmount >= costo) {
                playerStatsComp.silverAmount -= costo;
                valoresUsuario[0] += 65;
                valoresUnitarios[0] += 1;
                skillTreePanelBtns[2].GetComponentInChildren<Text>().text = "Comprar mejora\nCosto: " + Mathf.Floor(((valoresUnitarios[0] * incremento[10] + 1) - incremento[10]) * costosBase[10]);
                textosInformacion[6].text = "Dinero en la partida: $" + (playerStatsComp.startMoney + valoresUsuario[0]);
            } else {
                skillTreePanelBtns[2].GetComponentInChildren<Text>().text = "No hay suficiente varo";
                StartCoroutine(esperaCambiartextoBotton(2, "Comprar mejora\nCosto: ", 0, 10));
            }
        } else if (indice == 0 && btnName == "MoreExperienceBtn") {
            //porcentaje de dinero ganado de enemigos
            float costo = Mathf.Floor(((valoresUsuario[1] * incremento[12] + 1) - incremento[12]) * costosBase[12]);
            if (playerStatsComp.silverAmount >= costo) {
                playerStatsComp.silverAmount -= costo;
                valoresUsuario[1] += 5;
                valoresUnitarios[1] += 1;
                skillTreePanelBtns[3].GetComponentInChildren<Text>().text = "Comprar mejora\nCosto: " + Mathf.Floor(((valoresUsuario[1] * incremento[12] + 1) - incremento[12]) * costosBase[12]);
                textosInformacion[7].text = "Dinero de enemigos: +" + valoresUsuario[1] + "%";
            } else {
                skillTreePanelBtns[3].GetComponentInChildren<Text>().text = "No hay suficiente dinero";
                StartCoroutine(esperaCambiartextoBotton(3, "Comprar mejora\nCosto: ", 1, 12));
            }
        } else if (indice == 0 && btnName == "UpgradeCostBtn") {
            //plata ganada por horda
            float costo = Mathf.Floor(((valoresUsuario[2] * incremento[14] + 1) - incremento[14]) * costosBase[14]);
            if (playerStatsComp.silverAmount >= costo) {
                playerStatsComp.silverAmount -= costo;
                valoresUsuario[2] += 1;
                valoresUnitarios[2] += 1;
                skillTreePanelBtns[4].GetComponentInChildren<Text>().text = "Comprar mejora\nCosto: " + Mathf.Floor(((valoresUsuario[2] * incremento[14] + 1) - incremento[14]) * costosBase[14]);
                textosInformacion[8].text = "Plata ganada por horda: $" + (playerStatsComp.plataPorHorda + valoresUsuario[2]);
            } else {
                skillTreePanelBtns[4].GetComponentInChildren<Text>().text = "No hay suficiente dinero";
                StartCoroutine(esperaCambiartextoBotton(4, "Comprar mejora\nCosto:", 2, 14));
            }
        } else if (indice == 0 && btnName == "TurretDmgBtn") {
            //Incremento de vidas en partida
            float costo = Mathf.Floor(((valoresUsuario[3] * incremento[15] + 1) - incremento[15]) * costosBase[15]);
            if (playerStatsComp.silverAmount >= costo) {
                playerStatsComp.silverAmount -= costo;
                valoresUsuario[3] += 5;
                valoresUnitarios[3] += 1;
                skillTreePanelBtns[5].GetComponentInChildren<Text>().text = "Comprar mejora\nCosto: " + Mathf.Floor(((valoresUnitarios[3] * incremento[15] + 1) - incremento[15]) * costosBase[15]);
                textosInformacion[1].text = "Mas vidas: " + (playerStatsComp.startLives + valoresUsuario[3]);
            } else {
                skillTreePanelBtns[5].GetComponentInChildren<Text>().text = "No hay suficiente dinero";
                StartCoroutine(esperaCambiartextoBotton(5, "Comprar mejora\nCosto: ", 3, 15));
            }
        } else if (indice == 1) {
            if (btnName == "ActualLevelBtn") {
                _vTorretaCubo(0, 3, 0, 4, 2, 6, "Nivel actual: ", null);
            } else if (btnName == "MoreExperienceBtn") {
                _vTorretaCubo(1, 5, 1, 5, 3, 7, "Experiencia extra: ", "%");
            } else if (btnName == "UpgradeCostBtn") {
                _vTorretaCubo(2, 2, 2, 6, 4, 8, "Reducir precio de mejoras: ", "%");
            } else if (btnName == "TurretDmgBtn") {
                _vTorretaCubo(3, 3.5f, 5, 7, 5, 1, "Daño de la torreta: ", null);
            } else if (btnName == "FireRateBtn") {
                _vTorretaCubo(4, 0.35f, 3, 8, 6, 3, "Cadencia de tiro: ", "/Seg");
            } else if (btnName == "RangeBtn") {
                _vTorretaCubo(5, 0.25f, 4, 9, 10, 2, "Rango de la torreta: ", null);
            }
        } else if (indice == 2) {
            if (btnName == "ActualLevelBtn") {
                _vTorretaMG(0, 3, 0, 10, 2, 6, "Nivel actual: ", null);
            } else if (btnName == "MoreExperienceBtn") {
                _vTorretaMG(1, 5, 1, 11, 3, 7, "Experiencia extra: ", "%");
            } else if (btnName == "UpgradeCostBtn") {
                _vTorretaMG(2, 2, 2, 12, 4, 8, "Reducir precio de mejoras: ", "%");
            } else if (btnName == "TurretDmgBtn") {
                _vTorretaMG(3, 5, 5, 13, 5, 1, "Daño de la torreta: ", null);
            } else if (btnName == "FireRateBtn") {
                _vTorretaMG(4, 0.5f, 3, 14, 6, 3, "Cadencia de tiro: ", "/Seg");
            } else if (btnName == "RangeBtn") {
                _vTorretaMG(5, 0.4f, 4, 15, 10, 2, "Rango del a torreta: ", null);
            }
        } else if (indice == 3) {
            if (btnName == "ActualLevelBtn") {
                _vTorretaSniper(0, 3, 0, 16, 2, 6, "Nivel actual: ", null);
            } else if (btnName == "MoreExperienceBtn") {
                _vTorretaSniper(1, 5, 1, 17, 3, 7, "Experiencia extra: ", "%");
            } else if (btnName == "UpgradeCostBtn") {
                _vTorretaSniper(2, 2, 2, 18, 4, 8, "Reducir precio de mejoras: ", "%");
            } else if (btnName == "TurretDmgBtn") {
                _vTorretaSniper(3, 5, 5, 19, 5, 1, "Daño de la torreta: ", null);
            } else if (btnName == "FireRateBtn") {
                _vTorretaSniper(4, 0.2f, 3, 20, 6, 3, "Cadencia de tiro: ", "/Seg");
            } else if (btnName == "RangeBtn") {
                _vTorretaSniper(5, 0.7f, 4, 21, 10, 2, "Rango de la torreta: ", null);
            }
        } else if (indice == 4) {
            if (btnName == "ActualLevelBtn") {
                _vTorretaTrapecio(0, 3, 0, 22, 2, 6, "Nivel actual: ", null);
            } else if (btnName == "MoreExperienceBtn") {
                _vTorretaTrapecio(1, 5, 1, 23, 3, 7, "Experiencia extra: ", "%");
            } else if (btnName == "UpgradeCostBtn") {
                _vTorretaTrapecio(2, 2, 2, 24, 4, 8, "Reducir precio de mejoras: ", "%");
            }else if (btnName == "TurretDmgBtn") {
                _vTorretaTrapecio(3, 7, 5, 25, 5, 1, "Daño de la torreta: ", null);
            } else if (btnName == "FireRateBtn") {
                _vTorretaTrapecio(4, 0.4f, 3, 26, 6, 3, "Cadencia de tiro: ", "/Seg");
            } else if (btnName == "RangeBtn") {
                _vTorretaTrapecio(5, 0.3f, 4, 27, 10, 2, "Rango de la torreta: ", null);
            }
        } else if (indice == 5) {
            if (btnName == "ActualLevelBtn") {
                _vTorretaAoEBase(0, 3, 0, 28, 2, 6, "Nivel actual: ", null);
            } else if (btnName == "MoreExperienceBtn") {
                _vTorretaAoEBase(1, 5, 1, 29, 3, 7, "Experiencia extra: ", "%");
            } else if (btnName == "UpgradeCostBtn") {
                _vTorretaAoEBase(2, 2, 2, 30, 4, 8, "Reducir precio de mejoras: ", "%");
            } else if (btnName == "DmgPerSecondBtn") {
                _vTorretaAoEBase(3, 3, 5, 31, 7, 4, "Daño por segundo: ", "/Seg");
            } else if (btnName == "RalentizacionBtn") {
                _vTorretaAoEBase(4, 0.1f, 7, 32, 8, 5, "Porcentaje de ralentizacion: ", "%");
            } else if (btnName == "RangeBtn") {
                _vTorretaAoEBase(5, 0.15f, 4, 33, 10, 2, "Rango de la torreta: ", null);
            }
        } else if (indice == 6) {
            if (btnName == "ActualLevelBtn") {
                _vTorretaAoEBuff(0, 3, 0, 34, 2, 6, "Nivel actual: ", null);
            } else if (btnName == "MoreExperienceBtn") {
                _vTorretaAoEBuff(1, 5, 1, 35, 3, 7, "Experiencia extra: ", "%");
            } else if (btnName == "UpgradeCostBtn") {
                _vTorretaAoEBuff(2, 2, 2, 36, 4, 8, "Reducir precio de mejoras: ", "%");
            } else if (btnName == "extraBuffDmgBtn") {
                _vTorretaAoEBuff(3, 2.5f, 8, 37, 9, 9, "Porcentaje de mejora: ", "%" );
            } else if (btnName == "RangeBtn") {
                _vTorretaAoEBuff(4, 0.35f, 4, 38, 10, 2, "Rango de la torreta: ", null);
            }
        } else if (indice == 7) {
            if (btnName == "ActualLevelBtn") {
                _vTorretaAoEDmg(0, 3, 0, 39, 2, 6, "Nivel actual: ", null);
            } else if (btnName == "MoreExperienceBtn") {
                _vTorretaAoEDmg(1, 5, 1, 40, 3, 7, "Experiencia extra: ", "%");
            } else if (btnName == "UpgradeCostBtn") {
                _vTorretaAoEDmg(2, 2, 2, 41, 4, 8, "Reducir precio de mejoras: ", "%");
            } else if (btnName == "DmgPerSecondBtn") {
                _vTorretaAoEDmg(3, 4, 5, 42, 7, 4, "Daño por segundo: ", "/Seg");
            } else if (btnName == "RangeBtn") {
                _vTorretaAoEDmg(4, 0.35f, 4, 43, 10, 2, "Rango de la torreta: ", null);
            }
        } else if (indice == 8) {
            if (btnName == "ActualLevelBtn") {
                _vTorretaAoESlow(0, 3, 0, 44, 2, 6, "Nivel actual: ", null);
            } else if (btnName == "MoreExperienceBtn") {
                _vTorretaAoESlow(1, 2, 1, 45, 3, 7, "Experiencia extra: ", "%");
            } else if (btnName == "UpgradeCostBtn") {
                _vTorretaAoESlow(2, 2, 2, 46, 4, 8, "Reducir precio de mejoras: ", "%");
            } else if (btnName == "RalentizacionBtn") {
                _vTorretaAoESlow(3, 0.25f, 7, 47, 8, 5, "Porcentaje de ralentizacion: ", "%");
            } else if (btnName == "RangeBtn") {
                _vTorretaAoESlow(4, 0.35f, 4, 48, 10, 2, "Rango de la torreta: ", null);
            }
        } else if (indice == 9) {
            if (btnName == "ActualLevelBtn") {
                _vTorretaPBase(0, 3, 0, 49, 2, 6, "Nivel actual: ", null);
            } else if (btnName == "MoreExperienceBtn") {
                _vTorretaPBase(1, 2, 1, 50, 3, 7, "Experiencia extra: ", "%");
            } else if (btnName == "UpgradeCostBtn") {
                _vTorretaPBase(2, 2, 2, 51, 4, 8, "Reducir precio de mejoras: ", "%");
            } else if (btnName == "TurretDmgBtn") {
                _vTorretaPBase(3, 8, 5, 52, 5, 1, "Daño de la torreta: ", null);
            } else if (btnName == "FireRateBtn") {
                _vTorretaPBase(4, 0.25f, 3, 53, 6, 3, "Cadencia de tiro: ", "/Seg");
            } else if (btnName == "RangeBtn") {
                _vTorretaPBase(5, 0.4f, 4, 54, 10, 2, "Rango de la torreta: ", null);
            }
        } else if (indice == 10) {
            if (btnName == "ActualLevelBtn") {
                _vTorretaPCanon(0, 3, 0, 55, 2, 6, "Nivel actual: ", null);
            } else if (btnName == "MoreExperienceBtn") {
                _vTorretaPCanon(1, 2, 1, 56, 3, 7, "Experiencia extra: ", "%");
            } else if (btnName == "UpgradeCostBtn") {
                _vTorretaPCanon(2, 2, 2, 57, 4, 8, "Reducir precio de mejoras: ", "%");
            } else if (btnName == "TurretDmgBtn") {
                _vTorretaPCanon(3, 9, 5, 58, 5, 1, "Daño de la torreta: ", null);
            } else if (btnName == "FireRateBtn") {
                _vTorretaPCanon(4, 0.2f, 3, 59, 6, 3, "Cadencia de tiro: ", "/Seg");
            } else if (btnName == "RangeBtn") {
                _vTorretaPCanon(5, 0.45f, 4, 60, 10, 2, "Rango de la torreta: ", null);
            }
        } else if (indice == 11) {
            if (btnName == "ActualLevelBtn") {
                _vTorretaMortero(0, 3, 0, 61, 2, 6, "Nivel actual: ", null);
            } else if (btnName == "MoreExperienceBtn") {
                _vTorretaMortero(1, 2, 1, 62, 3, 7, "Experiencia extra: ", "%");
            } else if (btnName == "UpgradeCostBtn") {
                _vTorretaMortero(2, 2, 2, 63, 4, 8, "Reducir precio de mejoras: ", "%");
            } else if (btnName == "TurretDmgBtn") {
                _vTorretaMortero(3, 9, 5, 64, 5, 1, "Daño de la torreta: ", null);
            } else if (btnName == "FireRateBtn") {
                _vTorretaMortero(4, 0.1f, 3, 65, 6, 3, "Cadencia de tiro: ", "/Seg");
            } else if (btnName == "explosionRangeBtn") {
                _vTorretaMortero(5, 0.35f, 9, 66, 11, 10, "Rango de explosion: ", null);
            }
        } else if (indice == 12) {
            if (btnName == "ActualLevelBtn") {
                _vTorretaMissile(0, 3, 0, 67, 2, 6, "Nivel actual: ", null);
            } else if (btnName == "MoreExperienceBtn") {
                _vTorretaMissile(1, 2, 1, 68, 3, 7, "Experiencia extra: ", "%");
            } else if (btnName == "UpgradeCostBtn") {
                _vTorretaMissile(2, 2, 2, 69, 4, 8, "Reducir precio de mejoras: ", "%");
            } else if (btnName == "TurretDmgBtn") {
                _vTorretaMissile(3, 9, 5, 70, 5, 1, "Daño de la torreta: ", null);
            } else if (btnName == "FireRateBtn") {
                _vTorretaMissile(4, 0.2f, 3, 71, 6, 3, "Cadencia de tiro: ", "/Seg");
            } else if (btnName == "explosionRangeBtn") {
                _vTorretaMissile(5, 0.35f, 9, 72, 11, 10, "Rango de explosion: ", null);
            } else if (btnName == "RangeBtn") {
                _vTorretaMissile(6, 0.45f, 4, 73, 10, 2, "Rango de la torreta: ", null);
            }
        }
    }

    public void _vTorretaCubo(int _vTorreta, float _valorPTorreta, int _vIncremento, int _vUnitario, int _skillPanelBtn, int _vTextosInf, string _msgUno, string _msgDos ) {
        Turret torretaComp = torretas[indice - 1].prefab.GetComponent<Turret>();
        float costo = Mathf.Floor(((vTorretaCubo[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
        if (playerStatsComp.silverAmount >= costo) {
            playerStatsComp.silverAmount -= costo;
            vTorretaCubo[_vTorreta] += _valorPTorreta;
            valoresUnitarios[_vUnitario] += 1;
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((vTorretaCubo[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
            if (_skillPanelBtn == 2) {
                //ActualLevelBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaCubo[0];
            } else if (_skillPanelBtn == 3) {
                //MoreExperienceBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaCubo[1] + _msgDos;
            } else if (_skillPanelBtn == 4) {
                //UpgradeCostBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaCubo[2] + _msgDos;
            } else if (_skillPanelBtn == 5) {
                //TurretDmgBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startBulletDamage + vTorretaCubo[3]);
            } else if (_skillPanelBtn == 6) {
                //FireRateBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startFireRate + vTorretaCubo[4]) + _msgDos;
            } else if (_skillPanelBtn == 10) {
                //RangeBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startRango + vTorretaCubo[5]);
            }
        } else {
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "No hay suficiente dinero";
            StartCoroutine(esperaCambiartextoBotton(_skillPanelBtn, "Comprar mejora\nCosto: ", _vUnitario, _vIncremento));
        }
    }

    public void _vTorretaMG(int _vTorreta, float _valorPTorreta, int _vIncremento, int _vUnitario, int _skillPanelBtn, int _vTextosInf, string _msgUno, string _msgDos) {
        Turret torretaComp = torretas[indice - 1].prefab.GetComponent<Turret>();
        float costo = Mathf.Floor(((vTorretaMG[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
        if (playerStatsComp.silverAmount >= costo) {
            playerStatsComp.silverAmount -= costo;
            vTorretaMG[_vTorreta] += _valorPTorreta;
            valoresUnitarios[_vUnitario] += 1;
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((vTorretaMG[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
            if (_skillPanelBtn == 2) {
                //ActualLevelBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaMG[0];
            } else if (_skillPanelBtn == 3) {
                //MoreExperienceBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaMG[1] + _msgDos;
            } else if (_skillPanelBtn == 4) {
                //UpgradeCostBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaMG[2] + _msgDos;
            } else if (_skillPanelBtn == 5) {
                //TurretDmgBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startBulletDamage + vTorretaMG[3]);
            } else if (_skillPanelBtn == 6) {
                //FireRateBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startFireRate + vTorretaMG[4]) + _msgDos;
            } else if (_skillPanelBtn == 10) {
                //RangeBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startRango + vTorretaMG[5]);
            }
        } else {
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "No hay suficiente dinero";
            StartCoroutine(esperaCambiartextoBotton(_skillPanelBtn, "Comprar mejora\nCosto: ", _vUnitario, _vIncremento));
        }
    }

    public void _vTorretaSniper(int _vTorreta, float _valorPTorreta, int _vIncremento, int _vUnitario, int _skillPanelBtn, int _vTextosInf, string _msgUno, string _msgDos) {
        Turret torretaComp = torretas[indice - 1].prefab.GetComponent<Turret>();
        float costo = Mathf.Floor(((vTorretaSniper[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
        if (playerStatsComp.silverAmount >= costo) {
            playerStatsComp.silverAmount -= costo;
            vTorretaSniper[_vTorreta] += _valorPTorreta;
            valoresUnitarios[_vUnitario] += 1;
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((vTorretaSniper[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
            if (_skillPanelBtn == 2) {
                //ActualLevelBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaSniper[0];
            } else if (_skillPanelBtn == 3) {
                //MoreExperienceBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaSniper[1] + _msgDos;
            } else if (_skillPanelBtn == 4) {
                //UpgradeCostBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaSniper[2] + _msgDos;
            } else if (_skillPanelBtn == 5) {
                //TurretDmgBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startBulletDamage + vTorretaSniper[3]);
            } else if (_skillPanelBtn == 6) {
                //FireRateBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startFireRate + vTorretaSniper[4]) + _msgDos;
            } else if (_skillPanelBtn == 10) {
                //RangeBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startRango + vTorretaSniper[5]);
            }
        } else {
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "No hay suficiente dinero";
            StartCoroutine(esperaCambiartextoBotton(_skillPanelBtn, "Comprar mejora\nCosto: ", _vUnitario, _vIncremento));
        }
    }

    public void _vTorretaTrapecio(int _vTorreta, float _valorPTorreta, int _vIncremento, int _vUnitario, int _skillPanelBtn, int _vTextosInf, string _msgUno, string _msgDos) {
        Turret torretaComp = torretas[indice - 1].prefab.GetComponent<Turret>();
        float costo = Mathf.Floor(((vTorretaTrapecio[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
        if (playerStatsComp.silverAmount >= costo) {
            playerStatsComp.silverAmount -= costo;
            vTorretaTrapecio[_vTorreta] += _valorPTorreta;
            valoresUnitarios[_vUnitario] += 1;
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((vTorretaTrapecio[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
            if (_skillPanelBtn == 2) {
                //ActualLevelBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaTrapecio[0];
            } else if (_skillPanelBtn == 3) {
                //MoreExperienceBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaTrapecio[1] + _msgDos;
            } else if (_skillPanelBtn == 4) {
                //UpgradeCostBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaTrapecio[2] + _msgDos;
            } else if (_skillPanelBtn == 5) {
                //TurretDmgBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startBulletDamage + vTorretaTrapecio[3]);
            } else if (_skillPanelBtn == 6) {
                //FireRateBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startFireRate + vTorretaTrapecio[4]) + _msgDos;
            } else if (_skillPanelBtn == 10) {
                //RangeBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startRango + vTorretaTrapecio[5]);
            }
        } else {
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "No hay suficiente dinero";
            StartCoroutine(esperaCambiartextoBotton(_skillPanelBtn, "Comprar mejora\nCosto: ", _vUnitario, _vIncremento));
        }
    }

    public void _vTorretaAoEBase(int _vTorreta, float _valorPTorreta, int _vIncremento, int _vUnitario, int _skillPanelBtn, int _vTextosInf, string _msgUno, string _msgDos) {
        Turret torretaComp = torretas[indice - 1].prefab.GetComponent<Turret>();
        float costo = Mathf.Floor(((vTorretaAoEBase[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
        if (playerStatsComp.silverAmount >= costo) {
            playerStatsComp.silverAmount -= costo;
            vTorretaAoEBase[_vTorreta] += _valorPTorreta;
            valoresUnitarios[_vUnitario] += 1;
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((vTorretaAoEBase[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
            if (_skillPanelBtn == 2) {
                //ActualLevelBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaAoEBase[0];
            } else if (_skillPanelBtn == 3) {
                //MoreExperienceBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaAoEBase[1] + _msgDos;
            } else if (_skillPanelBtn == 4) {
                //UpgradeCostBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaAoEBase[2] + _msgDos;
            } else if (_skillPanelBtn == 7) {
                //DmgPerSecond
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startDanioPorSegundo + vTorretaAoEBase[3]) + _msgDos;
            } else if (_skillPanelBtn == 8) {
                //RalentizacionBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.StartPorcentajeDeRalentizacion + vTorretaAoEBase[4]) + _msgDos;
            } else if (_skillPanelBtn == 10) {
                //RangeBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startRango + vTorretaAoEBase[5]);
            }
        } else {
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "No hay suficiente dinero";
            StartCoroutine(esperaCambiartextoBotton(_skillPanelBtn, "Comprar mejora\nCosto: ", _vUnitario, _vIncremento));
        }
    }

    public void _vTorretaAoEBuff(int _vTorreta, float _valorPTorreta, int _vIncremento, int _vUnitario, int _skillPanelBtn, int _vTextosInf, string _msgUno, string _msgDos) {
        Turret torretaComp = torretas[indice - 1].prefab.GetComponent<Turret>();
        float costo = Mathf.Floor(((vTorretaAoEBuff[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
        if (playerStatsComp.silverAmount >= costo) {
            playerStatsComp.silverAmount -= costo;
            vTorretaAoEBuff[_vTorreta] += _valorPTorreta;
            valoresUnitarios[_vUnitario] += 1;
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((vTorretaAoEBuff[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
            if (_skillPanelBtn == 2) {
                //ActualLevelBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaAoEBuff[0];
            } else if (_skillPanelBtn == 3) {
                //MoreExperienceBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaAoEBuff[1] + _msgDos;
            } else if (_skillPanelBtn == 4) {
                //UpgradeCostBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaAoEBuff[2] + _msgDos;
            } else if (_skillPanelBtn == 9) {
                //extraBuffDmg
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startExtraDmgForBulletsWithBuff + vTorretaAoEBuff[3]) + _msgDos;
            } else if (_skillPanelBtn == 10) {
                //RangeBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startRango + vTorretaAoEBuff[4]);
            }
        } else {
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "No hay suficiente dinero";
            StartCoroutine(esperaCambiartextoBotton(_skillPanelBtn, "Comprar mejora\nCosto: ", _vUnitario, _vIncremento));
        }
    }

    public void _vTorretaAoEDmg(int _vTorreta, float _valorPTorreta, int _vIncremento, int _vUnitario, int _skillPanelBtn, int _vTextosInf, string _msgUno, string _msgDos) {
        Turret torretaComp = torretas[indice - 1].prefab.GetComponent<Turret>();
        float costo = Mathf.Floor(((vTorretaAoEDmg[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
        if (playerStatsComp.silverAmount >= costo) {
            playerStatsComp.silverAmount -= costo;
            vTorretaAoEDmg[_vTorreta] += _valorPTorreta;
            valoresUnitarios[_vUnitario] += 1;
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((vTorretaAoEDmg[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
            if (_skillPanelBtn == 2) {
                //ActualLevelBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaAoEDmg[0];
            } else if (_skillPanelBtn == 3) {
                //MoreExperienceBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaAoEDmg[1] + _msgDos;
            } else if (_skillPanelBtn == 4) {
                //UpgradeCostBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaAoEDmg[2] + _msgDos;
            } else if (_skillPanelBtn == 7) {
                //DmgPerSecond
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startDanioPorSegundo + vTorretaAoEDmg[3]) + _msgDos;
            } else if (_skillPanelBtn == 10) {
                //RangeBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startRango + vTorretaAoEDmg[4]);
            }
        } else {
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "No hay suficiente dinero";
            StartCoroutine(esperaCambiartextoBotton(_skillPanelBtn, "Comprar mejora\nCosto: ", _vUnitario, _vIncremento));
        }
    }

    public void _vTorretaAoESlow(int _vTorreta, float _valorPTorreta, int _vIncremento, int _vUnitario, int _skillPanelBtn, int _vTextosInf, string _msgUno, string _msgDos) {
        Turret torretaComp = torretas[indice - 1].prefab.GetComponent<Turret>();
        float costo = Mathf.Floor(((vTorretaAoESlow[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
        if (playerStatsComp.silverAmount >= costo) {
            playerStatsComp.silverAmount -= costo;
            vTorretaAoESlow[_vTorreta] += _valorPTorreta;
            valoresUnitarios[_vUnitario] += 1;
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((vTorretaAoESlow[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
            if (_skillPanelBtn == 2) {
                //ActualLevelBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaAoESlow[0];
            } else if (_skillPanelBtn == 3) {
                //MoreExperienceBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaAoESlow[1] + _msgDos;
            } else if (_skillPanelBtn == 4) {
                //UpgradeCostBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaAoESlow[2] + _msgDos;
            } else if (_skillPanelBtn == 8) {
                //RalentizacionBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.StartPorcentajeDeRalentizacion + vTorretaAoESlow[3]) + _msgDos;
            } else if (_skillPanelBtn == 10) {
                //RangeBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startRango + vTorretaAoESlow[4]);
            }
        } else {
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "No hay suficiente dinero";
            StartCoroutine(esperaCambiartextoBotton(_skillPanelBtn, "Comprar mejora\nCosto: ", _vUnitario, _vIncremento));
        }
    }

    public void _vTorretaPBase(int _vTorreta, float _valorPTorreta, int _vIncremento, int _vUnitario, int _skillPanelBtn, int _vTextosInf, string _msgUno, string _msgDos) {
        Turret torretaComp = torretas[indice - 1].prefab.GetComponent<Turret>();
        float costo = Mathf.Floor(((vTorretaPesadaB[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
        if (playerStatsComp.silverAmount >= costo) {
            playerStatsComp.silverAmount -= costo;
            vTorretaPesadaB[_vTorreta] += _valorPTorreta;
            valoresUnitarios[_vUnitario] += 1;
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((vTorretaPesadaB[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
            if (_skillPanelBtn == 2) {
                //ActualLevelBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaPesadaB[0];
            } else if (_skillPanelBtn == 3) {
                //MoreExperienceBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaPesadaB[1] + _msgDos;
            } else if (_skillPanelBtn == 4) {
                //UpgradeCostBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaPesadaB[2] + _msgDos;
            } else if (_skillPanelBtn == 5) {
                //TurretDmgBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startBulletDamage + vTorretaPesadaB[3]);
            } else if (_skillPanelBtn == 6) {
                //FireRateBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startFireRate + vTorretaPesadaB[4]) + _msgDos;
            } else if (_skillPanelBtn == 10) {
                //RangeBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startRango + vTorretaPesadaB[5]);
            }
        } else {
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "No hay suficiente dinero";
            StartCoroutine(esperaCambiartextoBotton(_skillPanelBtn, "Comprar mejora\nCosto: ", _vUnitario, _vIncremento));
        }
    }

    public void _vTorretaPCanon(int _vTorreta, float _valorPTorreta, int _vIncremento, int _vUnitario, int _skillPanelBtn, int _vTextosInf, string _msgUno, string _msgDos) {
        Turret torretaComp = torretas[indice - 1].prefab.GetComponent<Turret>();
        float costo = Mathf.Floor(((vTorretaCanon[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
        if (playerStatsComp.silverAmount >= costo) {
            playerStatsComp.silverAmount -= costo;
            vTorretaCanon[_vTorreta] += _valorPTorreta;
            valoresUnitarios[_vUnitario] += 1;
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((vTorretaCanon[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
            if (_skillPanelBtn == 2) {
                //ActualLevelBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaCanon[0];
            } else if (_skillPanelBtn == 3) {
                //MoreExperienceBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaCanon[1] + _msgDos;
            } else if (_skillPanelBtn == 4) {
                //UpgradeCostBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaCanon[2] + _msgDos;
            } else if (_skillPanelBtn == 5) {
                //TurretDmgBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startBulletDamage + vTorretaCanon[3]);
            } else if (_skillPanelBtn == 6) {
                //FireRateBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startFireRate + vTorretaCanon[4]) + _msgDos;
            } else if (_skillPanelBtn == 10) {
                //RangeBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startRango + vTorretaCanon[5]);
            }
        } else {
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "No hay suficiente dinero";
            StartCoroutine(esperaCambiartextoBotton(_skillPanelBtn, "Comprar mejora\nCosto: ", _vUnitario, _vIncremento));
        }
    }

    public void _vTorretaMortero(int _vTorreta, float _valorPTorreta, int _vIncremento, int _vUnitario, int _skillPanelBtn, int _vTextosInf, string _msgUno, string _msgDos) {
        Turret torretaComp = torretas[indice - 1].prefab.GetComponent<Turret>();
        float costo = Mathf.Floor(((vTorretaMortero[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
        if (playerStatsComp.silverAmount >= costo) {
            playerStatsComp.silverAmount -= costo;
            vTorretaMortero[_vTorreta] += _valorPTorreta;
            valoresUnitarios[_vUnitario] += 1;
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((vTorretaMortero[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
            if (_skillPanelBtn == 2) {
                //ActualLevelBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaMortero[0];
            } else if (_skillPanelBtn == 3) {
                //MoreExperienceBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaMortero[1] + _msgDos;
            } else if (_skillPanelBtn == 4) {
                //UpgradeCostBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaMortero[2] + _msgDos;
            } else if (_skillPanelBtn == 5) {
                //TurretDmgBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startBulletDamage + vTorretaMortero[3]);
            } else if (_skillPanelBtn == 6) {
                //FireRateBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startFireRate + vTorretaMortero[4]) + _msgDos;
            } else if (_skillPanelBtn == 11) {
                //explosionRangeBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startExplosionRadiusBullet + vTorretaMortero[5]);
            }
        } else {
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "No hay suficiente dinero";
            StartCoroutine(esperaCambiartextoBotton(_skillPanelBtn, "Comprar mejora\nCosto: ", _vUnitario, _vIncremento));
        }
    }

    public void _vTorretaMissile(int _vTorreta, float _valorPTorreta, int _vIncremento, int _vUnitario, int _skillPanelBtn, int _vTextosInf, string _msgUno, string _msgDos) {
        Turret torretaComp = torretas[indice - 1].prefab.GetComponent<Turret>();
        float costo = Mathf.Floor(((vTorretaMissileLauncher[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
        if (playerStatsComp.silverAmount >= costo) {
            playerStatsComp.silverAmount -= costo;
            vTorretaMissileLauncher[_vTorreta] += _valorPTorreta;
            valoresUnitarios[_vUnitario] += 1;
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "Comprar Mejora\nCost: " + Mathf.Floor(((vTorretaMissileLauncher[_vTorreta] * incremento[_vIncremento] + 1) - incremento[_vIncremento]) * costosBase[_vIncremento]);
            if (_skillPanelBtn == 2) {
                //ActualLevelBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaMissileLauncher[0];
            } else if (_skillPanelBtn == 3) {
                //MoreExperienceBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaMissileLauncher[1] + _msgDos;
            } else if (_skillPanelBtn == 4) {
                //UpgradeCostBtn
                textosInformacion[_vTextosInf].text = _msgUno + vTorretaMissileLauncher[2] + _msgDos;
            } else if (_skillPanelBtn == 5) {
                //TurretDmgBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startBulletDamage + vTorretaMissileLauncher[3]);
            } else if (_skillPanelBtn == 6) {
                //FireRateBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startFireRate + vTorretaMissileLauncher[4]) + _msgDos;
            } else if (_skillPanelBtn == 10) {
                //RangeBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startRango + vTorretaMissileLauncher[5]);
            } else if (_skillPanelBtn == 11) {
                //explosionRangeBtn
                textosInformacion[_vTextosInf].text = _msgUno + (torretaComp.startExplosionRadiusBullet + vTorretaMissileLauncher[6]);
            }
        } else {
            skillTreePanelBtns[_skillPanelBtn].GetComponentInChildren<Text>().text = "No hay suficiente dinero";
            StartCoroutine(esperaCambiartextoBotton(_skillPanelBtn, "Comprar mejora\nCosto: ", _vUnitario, _vIncremento));
        }
    }

    IEnumerator esperaCambiartextoBotton(int _skillTreePanelBtn, string _mensajebtn, int _indiceValorUnitario, int formulaValue) {
        yield return new WaitForSeconds(1.5f);
        skillTreePanelBtns[_skillTreePanelBtn].GetComponentInChildren<Text>().text = _mensajebtn + Mathf.Floor(((valoresUnitarios[formulaValue] * incremento[formulaValue] + 1) - incremento[formulaValue]) * costosBase[formulaValue]);
    }
}
