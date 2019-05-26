using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class shopValues : MonoBehaviour {

    public Button[] skillTreePanelBtns;
    public Text[] textosInformacion;
    public Sprite[] turretImgs;
    public TorretBlueprint[] torretas;

    public float[] costosBase;
    public float[] incremento;

    public Image imgTorreta;
    public int indice = 0;
    public PlayerStats playerStatsComp;
    public GameObject gameManagerGO;
    public GameObject mMenuGO;

    ///
    /// Array's de valores para cada torreta
    ///

    public float[] valoresCubo;
    public float[] valoresMG;
    public float[] valoresSniper;
    public float[] valoresTrapecio;
    public float[] valoresAoEBase;
    public float[] valoresAoEBuff;
    public float[] valoresAoEDmg;
    public float[] valoresAoESlow;
    public float[] valoresPesada;
    public float[] valoresCañon;
    public float[] valoresMortero;
    public float[] valoresMissile;
    public float[] valoresJugador;

    private void Awake() {
        playerStatsComp = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        gameManagerGO = GameObject.Find("GameManager");

        valoresJugador = new float[10];
    }

    private void Start() {

        textosInformacion[0].text = "Jugador";
        textosInformacion[6].text = "Dinero en la partida: +$" + valoresJugador[0];
        textosInformacion[7].text = "Dinero ganado de enemigos (+" + valoresJugador[1] + "%)";
        textosInformacion[8].text = "Plata ganada por horda (+" + valoresJugador[2] + ")";
        textosInformacion[1].text = "Mas vidas (+" + valoresJugador[3] + ")";

        skillTreePanelBtns[2].GetComponentInChildren<Text>().text = "Comprar Mejora\nCosto: " + Mathf.Floor(((valoresJugador[0] * incremento[10] + 1) - incremento[10]) * costosBase[10]);
    }

    private void Update() {
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

    public void navegacionSkillTree() {
        string btnPresionado = EventSystem.current.currentSelectedGameObject.name;

        if (btnPresionado == "TorretaArriba") {
            
        } else {
            
        }
    }


}
