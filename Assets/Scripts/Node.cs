using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;

public class Node : MonoBehaviour {
    //Color al pasar mouse por encima de un nodo
    public Color hoverColor;
    //Color para la condicion en caso de que no haya sufiente dinero
    public Color notEnoughMoneyColor;
    public Color nodoObstaculizadoColor;

    public Vector3 positionOffset;
    //Torreta construida en el nodo

    //[HideInInspector]
    public GameObject turret = null, 
        previewTurret = null;
    //[HideInInspector]
    public TorretBlueprint turretBlueprint = null;

    //Optimizacion, evita buscar el render cada que el mouse pasa por encima de un nodo
    //buscamos el componente renderer del objeto actual: Nodo
    private Renderer rend;
    //Color inicial del nodo
    private Color startColor;
    //asignacion de BuildManager
    BuildManager buildManager;
    public GameObject upgradePanel;
    public bool newTarget = false;
    public Turret turretComponent;
    public bool nodoObstaculizado = false;
    public float costoExtraPorObstaculo = 45f;
    public GameObject Obstaculo;
    public PlayerStats playerStatsComp;
    //Esta variable evita una excepcion extra�a, a la hora de iniciar una partida
    //si no se selecciona una torreta de la tienda, lanza un error, segun leo es un bug de Unity pero por si las moscas :V
    public static bool buildTurretException = false;

    private void Start() {
        playerStatsComp = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        //Obtenemos el renderer y lo almacenamos en Rend
        rend = GetComponent<Renderer>();
        //asignamos el color inicial al color del rend
        startColor = rend.material.color;
        //instancia de BuildManager
        buildManager = BuildManager.instance;
        upgradePanel = GameObject.Find("UpgradePanel");
    }

    //metodo para posicionar la torreta, llamar deesde BuildManager
    public Vector3 getBuildPosition() {
        return transform.position + positionOffset;
    }

    //Click del mouse
    void OnMouseDown() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        //si turret no es null, significa que ya hay algo construido en el nodo
        if (turret != null) {
            buildManager.selectNode(this);
            return;
        }

        if (!buildManager.canBuild) {
            return;
        }

        buildPreview(buildManager.getTurretToBuild());

        //ponemos null torretToBuild despues de haberla colocado en el nodo
        //asi no pondremos una segunda torreta por error, se ira de una por una D:
        buildManager.torretToBuild = null;
    }

    void buildPreview(TorretBlueprint blueprint) {
        if (buildTurretException) {
            if (buildManager.getTurretToBuild() == null) {
                return;
            }
            
            GameObject previewTurret = Instantiate(blueprint.prefab, getBuildPosition(), Quaternion.identity);
            turret = previewTurret;
            turretBlueprint = blueprint;
            turret.GetComponent<Turret>().previewTurret = true;
            buildManager.selectNode(this);
            buildManager.previewTurretLanded = true;
            Shop.previewTurretOn = true;
            upgradePanel.GetComponent<upgradeTowerPanel>().previewTurretOn = true;
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Obstaculo") {
            nodoObstaculizado = true;
            Obstaculo = other.gameObject;
        }
    }

    //metodo para Upgrade de la torreta
    public void torretUpgrade() {
        if (playerStatsComp.Money < turret.GetComponent<Turret>().costoUpgradeSigNivel) {
            upgradePanel.GetComponent<upgradeTowerPanel>().upgradeButton.interactable = false;
            return;
        }
        
        upgradePanel.GetComponent<upgradeTowerPanel>().upgradeButton.interactable = true;
        upgradePanel.GetComponent<upgradeTowerPanel>().torretaVendida = false;
        
        //Valores generales para todas las torretas
        float _nivelActual = turret.GetComponent<Turret>().nivelActualPorExperiencia;
        int _killCount = turret.GetComponent<Turret>().killCount;
        float _experienciaActual = turret.GetComponent<Turret>().experienciaActual;
        float _upgradesNivelActual = turret.GetComponent<Turret>().upgradesNivelActual;
        _upgradesNivelActual += 1;
        float _extraRangoPorUpgrades = turret.GetComponent<Turret>().startRango * (_upgradesNivelActual / 25);
        float _extraFireRatePorUpgrades = turret.GetComponent<Turret>().startFireRate * (_upgradesNivelActual / 25);
        //se declara primero aca, por si no hay boleanos AOE activados al final se usa este
        float _extraDmgPorNivel = 0;
        //Para torretas AOE revisar los boleanos que activan dichas torretas, declarar las variables y 
        //luego asignarles valores si los boleanos son verdaderos o falsos.
        float _extraRalentizacionPorNivel = 0;
        float _extraRalentizacionPorUpgrades = 0;
        float _extraDmgPorSegundoPorNivel = 0;
        float _extraDmgPorSegundoPorUpgrades = 0;
        float _extraExperienciaPorNivelBuffTower = 0;
        float _extraExperienciaPorUpgradesBuffTower = 0;
        float _extraExperienciaPorNivelSlowTurret = 0;
        float _extraExperienciaPorUpgradeSlowTurret = 0;
        float _extraBuffPorNivel = 0;
        float _fireCountDown = turret.GetComponent<Turret>().fireCountdown;

        if (turret.GetComponent<Turret>().aoeDamageAndRalent) {
            _extraRalentizacionPorNivel = turret.GetComponent<Turret>().extraRalentizacionPorNivel;
            _extraDmgPorSegundoPorNivel = turret.GetComponent<Turret>().extraDmgPorSegundoPorNivel;
            _extraDmgPorSegundoPorUpgrades = turret.GetComponent<Turret>().startExtraDmgPorSegundoPorUpgrades * (_upgradesNivelActual / 2.5f);
            _extraRalentizacionPorUpgrades = turret.GetComponent<Turret>().startExtraRalentizacionPorUpgrades * (_upgradesNivelActual / 2.5f);
        } else if (turret.GetComponent<Turret>().aoeDamage) {
            _extraDmgPorSegundoPorNivel = turret.GetComponent<Turret>().extraDmgPorSegundoPorNivel;
            _extraDmgPorSegundoPorUpgrades = turret.GetComponent<Turret>().startExtraDmgPorSegundoPorUpgrades * (_upgradesNivelActual / 2.5f);
        } else if (turret.GetComponent<Turret>().aoeBuff) {
            _extraExperienciaPorNivelBuffTower = turret.GetComponent<Turret>().extraExperienciaPorNivelBuffTower;
            _extraExperienciaPorUpgradesBuffTower = turret.GetComponent<Turret>().startExtraExperienciaPorUpgradesBuffTower * (_upgradesNivelActual / 1.25f);
            _extraBuffPorNivel = turret.GetComponent<Turret>().extraBuffPorNivel;
        } else if (turret.GetComponent<Turret>().aoeRalentizacion) {
            _extraExperienciaPorNivelSlowTurret = turret.GetComponent<Turret>().extraExperienciaPorNivelSlowTurret;
            _extraExperienciaPorUpgradeSlowTurret = turret.GetComponent<Turret>().startExtraExperienciaPorUpgradesSlowTurret * (_upgradesNivelActual / 2.5f);
            _extraRalentizacionPorNivel = turret.GetComponent<Turret>().extraRalentizacionPorNivel;
            _extraRalentizacionPorUpgrades = turret.GetComponent<Turret>().startExtraRalentizacionPorUpgrades * (_upgradesNivelActual / 2.5f);
        } else {
            _extraDmgPorNivel = turret.GetComponent<Turret>().extraDmgPorNivel;
        }

        float _extraDmgPorUpgrades = turret.GetComponent<Turret>().extraDmgPorUpgrades;
        _extraDmgPorUpgrades = turret.GetComponent<Turret>().startBulletDamage * (_upgradesNivelActual / 10);

        turretBlueprint.upgradingCost = turret.GetComponent<Turret>().costoUpgradeSigNivel;
        playerStatsComp.Money -= turretBlueprint.upgradingCost;
        Destroy(turret);
        ///
        /// Cambiar el diseño de la torreta que aparece
        ///
        if (_upgradesNivelActual >= 0 && _upgradesNivelActual <= 4) {
            GameObject temporaryTurret = Instantiate(turretBlueprint.prefab, getBuildPosition(), Quaternion.identity);
            turret = temporaryTurret;
        } else if (_upgradesNivelActual >= 5 && _upgradesNivelActual <= 7) {
            GameObject temporaryTurret = Instantiate(turretBlueprint.upgradedPrefab1, getBuildPosition(), Quaternion.identity);
            turret = temporaryTurret;
        } else if (_upgradesNivelActual >= 8 && _upgradesNivelActual <= 9) {
            GameObject temporaryTurret = Instantiate(turretBlueprint.upgradedPrefab2, getBuildPosition(), Quaternion.identity);
            turret = temporaryTurret;
        } else if(_upgradesNivelActual >= 10) {
            GameObject temporaryTurret = Instantiate(turretBlueprint.upgradedPrefab3, getBuildPosition(), Quaternion.identity);
            turret = temporaryTurret;
        }

        upgradePanel.GetComponent<upgradeTowerPanel>().setTarget(this);

        //asignamos valores de la torreta anterior destruida por upgrade a la nueva torreta construida por upgrade
        //Para que cuando se upgradee conserve valores como nivel, killcount o experiencia
        //declarar variables arriba con guion bajo
        if (turret.GetComponent<Turret>().nombreTorreta.Contains("Mortar")) {
            turret.GetComponent<AudioSource>().enabled = false;
        }
        turretComponent = turret.GetComponent<Turret>();
        turretComponent.nivelActualPorExperiencia = _nivelActual;

        //asignacion de valores para torretas AOE si los boleanos son verdaderos en caso de ningun boleano sea verdadero se usa extraDmgPorNivel
        if (turretComponent.aoeDamageAndRalent) {
            turretComponent.extraRalentizacionPorNivel = _extraRalentizacionPorNivel;
            turretComponent.extraDmgPorSegundoPorNivel = _extraDmgPorSegundoPorNivel;
            turretComponent.extraDmgPorSegundoPorUpgrades = _extraDmgPorSegundoPorUpgrades;
            turretComponent.extraRalentizacionPorUpgrades = _extraRalentizacionPorUpgrades;
        } else if (turretComponent.aoeDamage) {
            turretComponent.extraDmgPorSegundoPorNivel = _extraDmgPorSegundoPorNivel;
            turretComponent.extraDmgPorSegundoPorUpgrades = _extraDmgPorSegundoPorUpgrades;
        } else if (turretComponent.aoeBuff) {
            turretComponent.extraExperienciaPorNivelBuffTower = _extraExperienciaPorNivelBuffTower;
            turretComponent.extraExperienciaPorUpgradesBuffTower = _extraExperienciaPorUpgradesBuffTower;
            turretComponent.extraBuffPorNivel = _extraBuffPorNivel;
        } else if (turretComponent.aoeRalentizacion) {
            turretComponent.extraExperienciaPorNivelSlowTurret = _extraExperienciaPorNivelSlowTurret;
            turretComponent.extraExperienciaPorUpgradesSlowTurret = _extraExperienciaPorUpgradeSlowTurret;
            turretComponent.extraRalentizacionPorNivel = _extraRalentizacionPorNivel;
            turretComponent.extraRalentizacionPorUpgrades = _extraRalentizacionPorUpgrades;
        } else {
            turretComponent.extraDmgPorNivel = _extraDmgPorNivel;
        }

        turretComponent.fireCountdown = _fireCountDown;
        turretComponent.extraRangoPorUpgrades = _extraRangoPorUpgrades;
        turretComponent.extraFireRatePorUpgrades = _extraFireRatePorUpgrades;
        turretComponent.extraDmgPorUpgrades = _extraDmgPorUpgrades;
        turretComponent.multiplicadorDeExperienciaPorNivel = _nivelActual;
        turretComponent.experienciaActual = _experienciaActual;
        turretComponent.killCount = _killCount;
        turretComponent.upgradesNivelActual += _upgradesNivelActual;
        turretComponent.newCapabilitiesBool = true;
        if (turretComponent.nombreTorreta.Contains("Mortar")) {
            StartCoroutine(reEnable(turret));
        }
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, getBuildPosition(), Quaternion.identity);
        Destroy(effect, 0.75f);
        Debug.Log("Turret Upgraded!");
    }

    IEnumerator reEnable(GameObject turret) {
        yield return new WaitForSeconds(1);
        turret.GetComponent<AudioSource>().enabled = true;
    }

    public void sellTurret(float _extraMoneyForUpgrades) {
        //sumando dinero de la venta al usuario
        playerStatsComp.Money += _extraMoneyForUpgrades;
        upgradePanel.GetComponent<upgradeTowerPanel>().torretaVendida = true;
        //instancia del effecto al construir una torreta
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, getBuildPosition(), Quaternion.identity);
        //effect.transform.SetParent(effectsContainer.transform);
        Destroy(effect, 1.5f);
        //destruyendo la torreta y poniendo turretblueprint en nulo
        buildManager.selectNode(this);
        Destroy(turret);
        turretBlueprint = null;
    }

    //cuando el mouse pasa encima de un nodo
    void OnMouseEnter() {
        //despues de agregar using UnityEngine.EventSystems; 
        //agregamos lo siguiente para evitar que en caso de presionar alguna torreta en la barra UI
        //al presionar dos veces la torreta se posicione en el nodo justo debajo/detras del icono de la torreta
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.canBuild)
            return;

        if (buildManager.hasMoney) {
            //cambiara de color del objeto inicial al establecido en el inspector
            if (nodoObstaculizado) {
                rend.material.color = nodoObstaculizadoColor;
            } else {
                rend.material.color = hoverColor;
            }
        } else {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    public void newTowerUpgrade(TorretBlueprint _newTowerUpgraded) {
        if (playerStatsComp.Money < _newTowerUpgraded.cost) {
            return;
        }

        playerStatsComp.Money -= _newTowerUpgraded.cost;
        //Para pasarle valores a la torreta despues de upgradear a otro tipo de torreta
        //declaramos una variable de la forma "_nombrevariable" y asignamos el valor de la torreta vieja a esa variable
        //y lo asignamos de nuevo a la nueva torreta despues de reasignar _newTowerpgraded
        int _killCount = turret.GetComponent<Turret>().killCount;
        //float _experienciaActual = turret.GetComponent<Turret>().experienciaActual;
        //float _nivelActual = turret.GetComponent<Turret>().nivelActualPorExperiencia;
        Destroy(turret);
        GameObject tempNewUpgradedTower = Instantiate(_newTowerUpgraded.prefab, getBuildPosition(), Quaternion.identity);
        turret = tempNewUpgradedTower;
        //Aca reasignamos valores a la nueva torreta compradada de la torreta anterior
        turret.GetComponent<Turret>().killCount = _killCount;
        //turret.GetComponent<Turret>().experienciaActual = _experienciaActual;
        //turret.GetComponent<Turret>().nivelActualPorExperiencia = _nivelActual;
        turretBlueprint = _newTowerUpgraded;
        buildManager.selectNode(this);
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, getBuildPosition(), Quaternion.identity);
        //effect.transform.SetParent(effectsContainer.transform);
        Destroy(effect, 0.5f);
    }

    //cuando el mouse sale del objeto actual
    void OnMouseExit() {
        //cambiara el color del objeto actual al color inicial
        rend.material.color = startColor;
    }
}
