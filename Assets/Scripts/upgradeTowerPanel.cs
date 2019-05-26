using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class upgradeTowerPanel : MonoBehaviour {

    public GameObject upgradePanel;
    public Text upgradeCost;
    public Button upgradeButton;
    public Node selectedNode;
    public Text sellAmount;
    public float sellAmountFinal;
    public float firstSell = 50;
    public Animator controladorAnimaciones;
    public Text nombreTorretaText;
    public Text nivelTorretaText;
    public Text experienciaTorretaTxT;
    public Text cantidadUpgradesTxt;
    public Text danioTxt;
    public Text RangoTxt;
    public Text fireRateTxt;
    public Text rotacionTxt;
    public Text killCount;
    public Text nameTier1;
    public Text nameTier2;
    public Text nameTier3;
    public Text costoTier1;
    public Text costoTier2;
    public Text costoTier3;
    public bool estadoSeleccion = false;
    public Turret selectedTurretComponent;
    public bool estadoNode = false;
    public bool torretaVendida = false;

    [Header("Tier1")]
    public TorretBlueprint sniperTower;
    public TorretBlueprint machineGun;
    public TorretBlueprint heavyTurret;
    [Header("Tier2")]
    public TorretBlueprint missileLauncher;
    public TorretBlueprint canonTower;
    public TorretBlueprint morteroTower;
    [Header("Tier3")]
    public TorretBlueprint ralentizadora;
    public TorretBlueprint dmgPerSecond;
    public TorretBlueprint bufftower;
    [Header("Imagenes para las upgrades")]
    public Button[] upgradeBtns;
    public Sprite[] imgTower;

    public GameObject upgradeTowersPanel;
    public GameObject specialUpgradesForLevelPanel;
    public Slider experienciaTowerSlider;
    public Slider sliderComponent;
    public Toggle toggleBtn;
    public float extraMoneyForUpgrades;
    //este valor sera acomodado por las upgrades que compre el usuario en el arbol de habilidades
    public float porcentajeMejorable = 0.5f;
    //public GameObject particulasNodoSeleccionado;
    public GameObject ParticulasDelNodo;
    BuildManager buildManager;
    public GameObject rangoVisible;
    public PlayerStats playerStatsComp;

    public GameObject panelBotonosGO;
    public GameObject upgradeTowersGO;
    public GameObject previeewTurretGO;
    public Text confirmarTorreta;
    public Text cancelarTorreta;
    public bool previewTurretOn = false;

    private void Start() {
        playerStatsComp = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        controladorAnimaciones = GetComponent<Animator>();
        buildManager = BuildManager.instance;
        sliderComponent = experienciaTowerSlider.GetComponent<Slider>();

        if (selectedTurretComponent == null) {
            toggleBtn.interactable = false;
        }
    }

    IEnumerator cambiarMensajeConfirmarTorretaBtn() {
        yield return new WaitForSeconds(1);
        if (selectedNode.nodoObstaculizado) {
            confirmarTorreta.text = "Confirmar compra: \n$" + selectedNode.turretBlueprint.cost + selectedNode.costoExtraPorObstaculo;
        } else {
            confirmarTorreta.text = "Confirmar compra: \n$" + selectedNode.turretBlueprint.cost.ToString();
        }
    }

    public void confirmarCompraTorreta() {
        if (selectedNode != null) {
            if (playerStatsComp.Money < selectedNode.turretBlueprint.cost) {
                confirmarTorreta.text = "No hay suficiente dinero";
                StartCoroutine(cambiarMensajeConfirmarTorretaBtn());
                return;
            }

            if (selectedNode.nodoObstaculizado) {
                playerStatsComp.Money -= selectedNode.turretBlueprint.cost + selectedNode.costoExtraPorObstaculo;
                selectedNode.Obstaculo.SetActive(false);
                selectedNode.nodoObstaculizado = false;
                selectedTurretComponent.previewTurret = false;
                buildManager.previewTurretLanded = false;
                Shop.previewTurretOn = false;
                previewTurretOn = false;
                Shop.noTurret = true;
            } else {
                playerStatsComp.Money -= selectedNode.turretBlueprint.cost;
                selectedTurretComponent.previewTurret = false;
                buildManager.previewTurretLanded = false;
                Shop.previewTurretOn = false;
                previewTurretOn = false;
                Shop.noTurret = true;
            }
        }
    }

    public void cancelarComprarTorreta() {
        estadoSeleccion = false;
        estadoNode = false;
        selectedTurretComponent = null;
        torretaVendida = true;
        rangoVisible.gameObject.SetActive(false);
        Destroy(selectedNode.turret);
        selectedNode.turretBlueprint = null;
        hideUpgradePanel();
        buildManager.previewTurretLanded = false;
        Shop.previewTurretOn = false;
        previewTurretOn = false;
        Shop.noTurret = true;
    }

    public void setTarget(Node targetSettedOnanotherFunction) {
        estadoSeleccion = true;
        estadoNode = true;
        selectedNode = targetSettedOnanotherFunction;
        selectedTurretComponent = selectedNode.turret.GetComponent<Turret>();

        if (selectedNode.nodoObstaculizado) {
            confirmarTorreta.text = "Confirmar compra: \n$" + selectedNode.turretBlueprint.cost + selectedNode.costoExtraPorObstaculo;
        } else {
            confirmarTorreta.text = "Confirmar compra: \n$" + selectedNode.turretBlueprint.cost.ToString();
        }
        cancelarTorreta.text = "Cancelar compra";

        rangoVisible.SetActive(true);
        rangoVisible.transform.position = selectedTurretComponent.transform.position;
        rangoVisible.transform.localScale = new Vector3(selectedTurretComponent.rango * 2, 0.001f, selectedTurretComponent.rango * 2);

        if (selectedTurretComponent.upgradesNivelActual == 1) {
            sellAmountFinal = firstSell;
        }else {
            sellAmountFinal = selectedTurretComponent.costoUpgradeSigNivel * porcentajeMejorable;
        }

        torretaVendida = false;
        upgradeButton.interactable = true;
        toggleBtn.isOn = false;
        toggleBtn.interactable = true;
        upgradePanel.SetActive(true);
        toggleBtn.image.sprite = imgTower[10];
        controladorAnimaciones.SetBool("DerAIzqSalir", false);
        controladorAnimaciones.SetBool("DerAIzqMostrar", true);

        for (int i = 0; i < selectedNode.transform.childCount; i++) {
            if (selectedNode.transform.GetChild(i).name == "ParticulasNodo") {
                ParticulasDelNodo = selectedNode.transform.GetChild(i).gameObject;
            }
        }

        if (ParticulasDelNodo.activeInHierarchy) {
            return;
        } else {
            ParticulasDelNodo.SetActive(true);
        }

        nombreTorretaText.text = selectedTurretComponent.nombreTorreta;
        nivelTorretaText.text = "Lv: " + selectedTurretComponent.nivelActualPorExperiencia.ToString();
        experienciaTorretaTxT.text = "Experiencia: " + selectedTurretComponent.experienciaActual.ToString() + " / " + selectedTurretComponent.experienciaSigNivel.ToString();
        cantidadUpgradesTxt.text = "Mejoras compradas: " + selectedTurretComponent.upgradesNivelActual.ToString();
        if (selectedTurretComponent.name.Contains("Base") && selectedTurretComponent.upgradesNivelActual == 10) {
            upgradeButton.interactable = false;
            upgradeCost.text = "Completado";
        } else if (selectedTurretComponent.upgradesNivelActual == 11) {
            upgradeButton.interactable = false;
            upgradeCost.text = "Completado";
        } else {
            upgradeCost.text = "Mejora: $" + selectedTurretComponent.costoUpgradeSigNivel;
        }
        sellAmount.text = "Vender: $" + Mathf.FloorToInt(sellAmountFinal).ToString();
        if (selectedTurretComponent.aoeDamage) {
            danioTxt.text = "Daño de torreta: " + selectedTurretComponent.dañoPorSegundo.ToString() + " / Seg";
            RangoTxt.text = "Rango de la torreta: " + selectedTurretComponent.rango.ToString();
            fireRateTxt.enabled = false;
            rotacionTxt.enabled = false;
            killCount.enabled = true;
        } else if (selectedTurretComponent.aoeRalentizacion) {
            danioTxt.text = "Porcentaje de ralentizacion: " + (selectedTurretComponent.porcentajeDeRalentizacion * 100).ToString("f2") + "%";
            RangoTxt.text = "Rango de torreta: " + selectedTurretComponent.rango.ToString();
            fireRateTxt.enabled = false;
            killCount.enabled = false;
            rotacionTxt.enabled = false;
        } else if (selectedTurretComponent.aoeDamageAndRalent) {
            danioTxt.text = "Daño de torreta: " + selectedTurretComponent.dañoPorSegundo.ToString();
            RangoTxt.text = "porcentaje de ralentizacion: " + (selectedTurretComponent.porcentajeDeRalentizacion * 100).ToString("f2") + "%";
            fireRateTxt.text = "Rango: " + selectedTurretComponent.rango.ToString();
            rotacionTxt.enabled = false;
            killCount.enabled = true;
        } else if (selectedTurretComponent.aoeBuff) {
            danioTxt.text = "Daño extra para torretas: " + (selectedTurretComponent.extraDmgForBulletsWithBuff * 100) + "%";
            RangoTxt.text = "Rango: " + selectedTurretComponent.rango.ToString();
            fireRateTxt.enabled = false;
            rotacionTxt.enabled = false;
            killCount.enabled = false;
        } else {
            killCount.enabled = true;
            fireRateTxt.enabled = true;
            rotacionTxt.enabled = true;
            danioTxt.text = "Daño de torretas: " + selectedTurretComponent.bulletDamage.ToString("f2");
            RangoTxt.text = "Rango de torretas: " + selectedTurretComponent.rango.ToString("f2");
            fireRateTxt.text = "Cadencia de tiro: " + selectedTurretComponent.fireRate.ToString("f2");
            rotacionTxt.text = "Velocidad de rotacion: " + selectedTurretComponent.turnSpeed.ToString();
        }

        if (selectedNode.turret.name.Contains("AoEBase")) {
            upgradeBtns[0].image.sprite = imgTower[2];
            nameTier1.text = "Torreta ralentizadora";
            costoTier1.text = "$" + ralentizadora.cost.ToString();
            upgradeBtns[1].image.sprite = imgTower[1];
            nameTier2.text = "Torreta de daño de area";
            costoTier2.text = "$" + dmgPerSecond.cost.ToString();
            upgradeBtns[2].image.sprite = imgTower[0];
            nameTier3.text = "Torreta Buff";
            costoTier3.text = "$" + bufftower.cost.ToString();
        } else if (selectedNode.turret.name.Contains("CuboBase")) {
            upgradeBtns[0].image.sprite = imgTower[4];
            nameTier1.text = "Torreta Francotiradora";
            costoTier1.text = "$" + sniperTower.cost.ToString();
            upgradeBtns[1].image.sprite = imgTower[3];
            nameTier2.text = "Torreta Repetidora";
            costoTier2.text = "$" + machineGun.cost.ToString();
            upgradeBtns[2].image.sprite = imgTower[5];
            nameTier3.text = "Torreta pesada";
            costoTier3.text = "$" + heavyTurret.cost.ToString();
        } else if (selectedNode.turret.name.Contains("PesadaBase")) {
            upgradeBtns[0].image.sprite = imgTower[6];
            nameTier1.text = "Torreta de misiles";
            costoTier1.text = "$" + missileLauncher.cost.ToString();
            upgradeBtns[1].image.sprite = imgTower[7];
            nameTier2.text = "Torreta Cañon";
            costoTier2.text = "$" + canonTower.cost.ToString();
            upgradeBtns[2].image.sprite = imgTower[8];
            nameTier3.text = "Torreta Mortero";
            costoTier3.text = "$" + morteroTower.cost.ToString();
        }
    }

    private void Update() {

        //al iniciar el juego no hay ningun nodo seleccionado, entonces si quito lo siguiente botara un error
        //con el upgradePanel porq no tenemos un nodo seleccionado
        if (selectedNode == null ||  torretaVendida || selectedTurretComponent == null) {
            return;
        }

        if (selectedTurretComponent.previewTurret) {
            panelBotonosGO.gameObject.SetActive(false);
            upgradeTowersGO.gameObject.SetActive(false);
            previeewTurretGO.gameObject.SetActive(true);
        } else {
            panelBotonosGO.gameObject.SetActive(true);
            upgradeTowersGO.gameObject.SetActive(true);
            previeewTurretGO.gameObject.SetActive(false);
        }

        /* FOR LATER
        if (selectedTurretComponent.upgradesNivelActual < 30) {
            upgradeTowersPanel.SetActive(true);
            specialUpgradesForLevelPanel.SetActive(false);
        }
        */

        if (estadoSeleccion) {
            nombreTorretaText.text = selectedTurretComponent.nombreTorreta;

            nivelTorretaText.text = "Lv: " + selectedTurretComponent.nivelActualPorExperiencia.ToString();
            experienciaTorretaTxT.text = "Experiencia: " + Mathf.FloorToInt(selectedTurretComponent.experienciaActual).ToString() + " / " + selectedTurretComponent.experienciaSigNivel.ToString();
            cantidadUpgradesTxt.text = "Mejoras Compradas: " + selectedNode.turret.GetComponent<Turret>().upgradesNivelActual.ToString();

            ///
            /// tener en cuenta esta parte por cualquier cosa, antes estaba en el metodo upgradeTower()
            ///
            if (selectedTurretComponent.name.Contains("Base") && selectedNode.turret.GetComponent<Turret>().upgradesNivelActual == 10) {
                upgradeButton.interactable = false;
                sellAmountFinal = selectedTurretComponent.costoUpgradeSigNivel * porcentajeMejorable;
                sellAmount.text = "Vender: $" + Mathf.FloorToInt(sellAmountFinal).ToString();
                upgradeCost.text = "Completado";
                return;
            } else if (selectedNode.turret.GetComponent<Turret>().upgradesNivelActual == 11) {
                upgradeButton.interactable = false;
                sellAmountFinal = selectedTurretComponent.costoUpgradeSigNivel * porcentajeMejorable;
                sellAmount.text = "Vender: $" + Mathf.FloorToInt(sellAmountFinal).ToString();
                upgradeCost.text = "Completado";
                return;
            } else {
                upgradeCost.text = "Mejorar: $" + selectedTurretComponent.costoUpgradeSigNivel.ToString();
                sellAmountFinal = selectedTurretComponent.costoUpgradeSigNivel * porcentajeMejorable;
                sellAmount.text = "Vender: $" + Mathf.FloorToInt(sellAmountFinal).ToString();
            }

            if (selectedTurretComponent.aoeDamage) {
                danioTxt.text = "Daño de torreta: " + selectedTurretComponent.dañoPorSegundo.ToString("f2") + " / Seg";
                RangoTxt.text = "Rango de torreta: " + selectedTurretComponent.rango.ToString();
                fireRateTxt.enabled = false;
                rotacionTxt.enabled = false;
                killCount.enabled = true;
            } else if (selectedTurretComponent.aoeRalentizacion) {
                danioTxt.text = "Porcentaje de Ralentizacion: " + (selectedTurretComponent.porcentajeDeRalentizacion * 100).ToString("f2") + "%";
                RangoTxt.text = "Rango de torreta: " + selectedTurretComponent.rango.ToString();
                fireRateTxt.enabled = false;
                rotacionTxt.enabled = false;
                killCount.enabled = false;
            } else if (selectedTurretComponent.aoeDamageAndRalent) {
                danioTxt.text = "Daño de torretas: " + selectedTurretComponent.dañoPorSegundo.ToString() + " / Seg";
                RangoTxt.text = "Porcentaje de ralentizacion: " + (selectedTurretComponent.porcentajeDeRalentizacion * 100).ToString("f2") + "%";
                fireRateTxt.text = "Rango: " + selectedTurretComponent.rango.ToString();
                rotacionTxt.enabled = false;
                killCount.enabled = true;
            } else if (selectedTurretComponent.aoeBuff) {
                danioTxt.text = "Daño extra para torretas: " + (selectedTurretComponent.extraDmgForBulletsWithBuff * 100) + "%";
                RangoTxt.text = "Rango: " + selectedTurretComponent.rango.ToString();
                fireRateTxt.enabled = false;
                rotacionTxt.enabled = false;
                killCount.enabled = false;
            } else {
                killCount.enabled = true;
                fireRateTxt.enabled = true;
                rotacionTxt.enabled = true;
                danioTxt.text = "Daño de torreta: " + selectedTurretComponent.bulletDamage.ToString("f2");
                RangoTxt.text = "Rango de torreta: " + selectedTurretComponent.rango.ToString("f2");
                fireRateTxt.text = "Cadencia de tiro: " + selectedTurretComponent.fireRate.ToString("f2");
                rotacionTxt.text = "Velocidad de rotacion: " + selectedTurretComponent.turnSpeed.ToString();
            }

            killCount.text = "Enemigos destruidos: " + selectedTurretComponent.killCount;
            sliderComponent.value = selectedTurretComponent.experienciaActual / selectedTurretComponent.experienciaSigNivel;
        }
        /* FOR LATER
        if (selectedTurretComponent.noMoreTowers || selectedTurretComponent.upgradesNivelActual >= 30) {
            upgradeTowersPanel.SetActive(false);
            specialUpgradesForLevelPanel.SetActive(true);
        }
        */

        if (playerStatsComp.Money < selectedTurretComponent.costoUpgradeSigNivel) {
            upgradePanel.GetComponent<upgradeTowerPanel>().upgradeButton.interactable = true;
            return;
        }
    }

    public void hideUpgradePanel() {
        if (ParticulasDelNodo != null) {
            ParticulasDelNodo.SetActive(false);
            controladorAnimaciones.SetBool("DerAIzqMostrar", false);
            controladorAnimaciones.SetBool("DerAIzqSalir", true);
            selectedTurretComponent = null;
            selectedNode = null;
            estadoNode = false;
            rangoVisible.SetActive(false);
        }
    }

    public void toggleUpgradePanel() {
        if (previewTurretOn) {
            return;
        }

        if (toggleBtn.isOn) {
            //Ocultar
            controladorAnimaciones.SetBool("DerAIzqMostrar", false);
            controladorAnimaciones.SetBool("DerAIzqSalir", true);
            toggleBtn.image.sprite = imgTower[9];
            if (ParticulasDelNodo != null) {
                ParticulasDelNodo.SetActive(false);
            }
            selectedTurretComponent = null;
            selectedNode = null;
            estadoNode = false;
            rangoVisible.SetActive(false);
            buildManager.selectedNode = null;
        }
    }

    public void upgradeTower() {
        selectedNode.torretUpgrade();
    }

    public void sellTower() {
        estadoSeleccion = false;
        estadoNode = false;
        selectedTurretComponent = null;
        extraMoneyForUpgrades = sellAmountFinal;
        selectedNode.sellTurret(extraMoneyForUpgrades);
        BuildManager.instance.deselectNode();
    }

    public void tier1() {

        estadoSeleccion = false;
        estadoNode = false;
        selectedTurretComponent = null;

        if (selectedNode.turret.name.Contains("CuboBase")) {
            selectedNode.newTowerUpgrade(sniperTower);
        } else if (selectedNode.turret.name.Contains("PesadaBase")) {
            selectedNode.newTowerUpgrade(missileLauncher);
        } else if (selectedNode.turret.name.Contains("AoEBase")) {
            selectedNode.newTowerUpgrade(ralentizadora);
        }
        
        BuildManager.instance.deselectNode();
    }

    public void tier2() {
        estadoSeleccion = false;
        estadoNode = false;
        selectedTurretComponent = null;
        if (selectedNode.turret.name.Contains("CuboBase")) {
            selectedNode.newTowerUpgrade(machineGun);
        } else if (selectedNode.turret.name.Contains("PesadaBase")) {
            selectedNode.newTowerUpgrade(canonTower);
        } else if (selectedNode.turret.name.Contains("AoEBase")) {
            selectedNode.newTowerUpgrade(dmgPerSecond);
        }
        BuildManager.instance.deselectNode();
    }

    public void tier3() {
        estadoSeleccion = false;
        estadoNode = false;
        selectedTurretComponent = null;
        if (selectedNode.turret.name.Contains("CuboBase")) {
            selectedNode.newTowerUpgrade(heavyTurret);
        } else if (selectedNode.turret.name.Contains("PesadaBase")) {
            selectedNode.newTowerUpgrade(morteroTower);
        } else if (selectedNode.turret.name.Contains("AoEBase")) {
            selectedNode.newTowerUpgrade(bufftower);
        }
        BuildManager.instance.deselectNode();
    }
}