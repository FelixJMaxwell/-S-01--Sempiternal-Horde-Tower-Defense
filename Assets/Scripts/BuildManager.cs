using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BuildManager : MonoBehaviour {

    //Existe solo un BuildManager que puede ser accedido desde cualquier lugar
    public static BuildManager instance;

    private void Awake() {
        if (instance != null) {
            Debug.LogError("More than one BuildManager in scene");
            return;
        }
        instance = this;
    }

    //end buildmanager singleton

    public PlayerStats playerStatsComp;
    private void Start() {
        playerStatsComp = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
    }

    //particulas para el momento en que se construye y vende una torreta
    public GameObject buildEffect;
    public GameObject sellEffect;
    //public GameObject effectsContainer;

    //torre a construir
    public TorretBlueprint torretToBuild;

    public Node selectedNode;
    public GameObject upgradesTowerPanelGO;
    public upgradeTowerPanel upgradesTowerPanel;
    public bool previewTurretLanded = false;

    public bool canBuild { get { return torretToBuild != null; } }
    //En caso de tener suficiente dinero devolver falso y si tenemos suficiente o mas true
    public bool hasMoney { get { return playerStatsComp.Money >= torretToBuild.cost; } }

    public void selectNode(Node node) {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        } else {
            if (previewTurretLanded) {
                return;
            }

            if (selectedNode == node) {
                deselectNode();
                return;
            } else if (selectedNode != node) {
                if (upgradesTowerPanel.ParticulasDelNodo != null) {
                    upgradesTowerPanel.ParticulasDelNodo.SetActive(false);
                }
                selectedNode = node;
                torretToBuild = null;
                Node.buildTurretException = false;
                upgradesTowerPanel.setTarget(node);
            }
        }
    }

    public void deselectNode() {
        selectedNode = null;
        upgradesTowerPanel.hideUpgradePanel();
    }

    public void selectedTorretToBuild(TorretBlueprint turret) {
        torretToBuild = turret;
        deselectNode();
    }

    public TorretBlueprint getTurretToBuild() {
        return torretToBuild;
    }
}