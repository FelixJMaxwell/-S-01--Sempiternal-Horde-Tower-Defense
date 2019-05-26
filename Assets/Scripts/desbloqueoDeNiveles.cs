using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class desbloqueoDeNiveles : MonoBehaviour {

    [Header("Inspector Setup")]
    public Button[] lvlBtns;

    /// <summary>
    /// 0.- Candado
    /// 1.- Default
    /// 2.- ArrowLeft
    /// 3.- ArrowRight
    /// 4.- M1
    /// 5.- M2
    /// 6.- M3
    /// 7.- M4
    /// </summary>
    public Sprite[] imgsNeeded;

    /// <summary>
    /// textos:
    /// 0.- Costo de nivel (LevelCost)
    /// 1.- Costo en plata (SilverCost)
    /// 2.- Cantidad de Oro (GoldAmount)
    /// 3.- Cantidad de plata (SilverAmount)
    /// </summary>
    public Text[] textos;

    /// <summary>
    /// 0.- GoldAmount
    /// 1.- SilverAmount
    /// </summary>
    public InputField[] iField;

    public GameObject[] Niveles;
    public bool changeCurrencyConfirmation;
    public Button changeCurrencyBtn;
    public Image arrowImg;
    public Button playButton;
    public Button unlockButton;

    [Header("")]
    public int costoParaDesbloquear1;
    public int costoParaDesbloquear2;

    [Header("Configuraciones especificas de cada boton")]
    public bool btnState;
    public enum btnStatus{Locked, Unlocked};
    public btnStatus Status;
    public GameObject playerStatsGO;
    public PlayerStats playerstatsComponent;
    public string primerBtnPresionado;
    public fadeInScene fadeScenePanel;
    public GameObject mainMenuGO;
    public desbloqueoDeNiveles mainmenuComp;
    public bool exitFor = false;

    private void Awake() {
        playerStatsGO = GameObject.Find("PlayerStats");
        playerstatsComponent = playerStatsGO.GetComponent<PlayerStats>();

        if (mainMenuGO != null) {
            mainmenuComp = mainMenuGO.GetComponent<desbloqueoDeNiveles>();
        }
    }

    private void Update() {

        if (transform.name == "MainMenu") {
            //mainmenuComp.textos[2].text = "Oro: " + playerstatsComponent.goldAmount;
            mainmenuComp.textos[3].text = "Plata: " + playerstatsComponent.silverAmount;
        }

        if (exitFor == false) {
            for (int i = 0; i < lvlBtns.Length; i++) {

                if (lvlBtns[i].transform.gameObject.activeInHierarchy) {
                    if (lvlBtns[i].name == primerBtnPresionado) {
                        lvlBtns[i].GetComponent<Animator>().SetBool("nivelSeleccionado", true);
                    } else {
                        lvlBtns[i].GetComponent<Animator>().SetBool("nivelSeleccionado", false);
                    }
                }

                if (lvlBtns[i].name == playerstatsComponent.nivelesDesbloqueados[i]) {
                    lvlBtns[i].GetComponent<desbloqueoDeNiveles>().Status = btnStatus.Unlocked;
                }

                if (lvlBtns[i].GetComponent<desbloqueoDeNiveles>().Status == btnStatus.Locked) {
                    lvlBtns[i].image.sprite = imgsNeeded[0];
                } else {

                    if (lvlBtns[i].name == "Level01") {
                        lvlBtns[i].image.sprite = imgsNeeded[4];
                    }else if(lvlBtns[i].name == "Level02") {
                        lvlBtns[i].image.sprite = imgsNeeded[5];
                    }else if(lvlBtns[i].name == "Level03") {
                        lvlBtns[i].image.sprite = imgsNeeded[6];
                    } else if (lvlBtns[i].name == "Level04") {
                        lvlBtns[i].image.sprite = imgsNeeded[7];
                    } else if(lvlBtns[i].name == "Level05") {
                        lvlBtns[i].image.sprite = imgsNeeded[8];
                    }
                    playerstatsComponent.nivelesDesbloqueados[i] = lvlBtns[i].name;
                }

                if (i > lvlBtns.Length) {
                    exitFor = true;
                }
            }
        }
    }

    public void revisarPropiedades() {
        desbloqueoDeNiveles btnComponent = EventSystem.current.currentSelectedGameObject.GetComponent<desbloqueoDeNiveles>();
        primerBtnPresionado = btnComponent.name;

        if (btnComponent.Status == btnStatus.Locked) {
            playButton.gameObject.SetActive(false);
            unlockButton.gameObject.SetActive(true);
            textos[0].text = "Desbloquea por:";
            textos[1].text = "Plata: $" + btnComponent.costoParaDesbloquear1.ToString();
            costoParaDesbloquear2 = btnComponent.costoParaDesbloquear1;
        } else {
            playButton.gameObject.SetActive(true);
            unlockButton.gameObject.SetActive(false);

            if (primerBtnPresionado == "Level01") {
                textos[0].text = "Level 01";
                textos[1].text = "Seleccionado";
            } else if (primerBtnPresionado == "Level02") {
                textos[0].text = "Level 02";
                textos[1].text = "Seleccionado";
            } else if (primerBtnPresionado == "Level03") {
                textos[0].text = "Level 03";
                textos[1].text = "Seleccionado";
            } else if (primerBtnPresionado == "Level04") {
                textos[0].text = "Level 04";
                textos[1].text = "Seleccionado";
            } else if (primerBtnPresionado == "Level05") {
                textos[0].text = "Level 05";
                textos[1].text = "Seleccionado";
            }
        }
    }

    public void desbloquearNivel() {
        desbloqueoDeNiveles btnComponent = EventSystem.current.currentSelectedGameObject.GetComponent<desbloqueoDeNiveles>();

        if (costoParaDesbloquear2 <= playerstatsComponent.silverAmount) {
            for (int i = 0; i < lvlBtns.Length; i++) {
                if (primerBtnPresionado == lvlBtns[i].name) {
                    lvlBtns[i].GetComponent<desbloqueoDeNiveles>().Status = btnStatus.Unlocked;
                    playerstatsComponent.silverAmount = playerstatsComponent.silverAmount - costoParaDesbloquear2;
                    textos[3].text = "Plata: " + playerstatsComponent.silverAmount.ToString();
                    textos[0].text = "Nivel";
                    textos[1].text = "Desbloqueado";
                }
            }
            unlockButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
        } else {
            textos[0].text = "No hay";
            textos[1].text = "Suficiente Plata";
            StartCoroutine(esperaMensajes(costoParaDesbloquear2));
        }
    }

    IEnumerator esperaMensajes(int _costoParaDesbloquear) {
        yield return new WaitForSeconds(1);
        textos[0].text = "Desbloquea por: ";
        textos[1].text = "Plata: $" + _costoParaDesbloquear;
    }

    public void jugarNivel() {
        for (int i = 0; i < lvlBtns.Length; i++) {
            if (primerBtnPresionado == lvlBtns[i].name) {
                if (lvlBtns[i].GetComponent<desbloqueoDeNiveles>().Status == btnStatus.Unlocked) {
                    fadeScenePanel.fadeTo(primerBtnPresionado);
                }
            }
        }
    }

    public void multiplicadorDeDificultad(string _multiplicador) {
        playerstatsComponent.multiplicadorDeDificultad = int.Parse(_multiplicador);
    }

    /*
    public void conversionDeMonedas(string _cantidad) {
        InputField inputfield = EventSystem.current.currentSelectedGameObject.GetComponent<InputField>();
        if (inputfield.name.Contains("Gold")) {
            if (float.Parse(_cantidad) <= playerstatsComponent.goldAmount) {
                changeCurrencyConfirmation = false;
                changeCurrencyBtn.interactable = true;
                arrowImg.sprite = imgsNeeded[3];
                iField[1].text = (float.Parse(_cantidad) * 1000).ToString();
            } else {
                changeCurrencyBtn.interactable = false;
            }
        } else {
            if (float.Parse(_cantidad) <= playerstatsComponent.silverAmount) {
                changeCurrencyConfirmation = true;
                changeCurrencyBtn.interactable = true;
                arrowImg.sprite = imgsNeeded[2];
                iField[0].text = (float.Parse(_cantidad) / 1000).ToString();
            } else {
                changeCurrencyBtn.interactable = false;
            }
        }
    }
    */

    public void changeCurrency() {

        if (changeCurrencyConfirmation) {
            float newGold = float.Parse(iField[0].text);
            //Debug.Log("Se convirtieron: " + iField[1].text + " de Platas a: " + iField[0].text + " Oro");
            //playerstatsComponent.goldAmount += newGold;
            playerstatsComponent.silverAmount -= float.Parse(iField[1].text);
            //textos[2].text = "Oro: " + playerstatsComponent.goldAmount.ToString();
        } else {
            float newSilver = float.Parse(iField[1].text);
            //Debug.Log("Se convirtieron: " + iField[0].text + " de Oro a: " + iField[1].text + " Platas");
            playerstatsComponent.silverAmount += newSilver;
            //playerstatsComponent.goldAmount -= float.Parse(iField[0].text);
            textos[3].text = "Plata: " + playerstatsComponent.silverAmount.ToString();
        }

        for (int i = 0; i < iField.Length; i++) {
            iField[i].text = "";
        }

    }
}