using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    [Header("Configuracion generales")]
    public TorretBlueprint standarTurret;
    public TorretBlueprint pesadaBase;
    public TorretBlueprint aoeBase;
    [Header("Configuracion extra")]
    public Animator hideShowShop;
    public Sprite[] imgShop;
    public Toggle toggleShop;
    public Button[] shopBtns;
    BuildManager buildManager;
    public static bool noTurret = false;
    public static bool previewTurretOn = false;

    void Start() {
        buildManager = BuildManager.instance;
        hideShowShop = GetComponent<Animator>();
        hideShowShop.SetBool("HideShop", false);
        hideShowShop.SetBool("ShowShop", false);
    }

    private void Update() {
        if (noTurret) {
            noTurret = false;
            shopBtns[0].image.sprite = imgShop[6];
            shopBtns[1].image.sprite = imgShop[7];
            shopBtns[2].image.sprite = imgShop[5];
        }
    }

    public void hideShowShopPanel() {
        if (toggleShop.isOn == true) {
            hideShowShop.SetBool("HideShop", false);
            hideShowShop.SetBool("ShowShop", true);
            toggleShop.image.sprite = imgShop[0];
        } else {
            hideShowShop.SetBool("ShowShop", false);
            hideShowShop.SetBool("HideShop", true);
            toggleShop.image.sprite = imgShop[1];
        }
    }

    //metodo para asignar a un boton en la UI como evento "On click ()"
    //en la UI y el boton especifico asignamos Runtime Only
    //agregamos el padre de este boton, en este caso shop en el espacio debajo del runtime
    //y como funcion buscamos el nombre del padre (shop) y entre sus metodos buscamos el siguiente:
    /*public void PurchaseStandardTurret() {
        Debug.Log("Standard Turret purchased");
        buildManager.setTorretToBuild(buildManager.standartTurretPrefab);
    }*/
    //||||||||||
    //|||||||||| Cambios: para agregar la moneda del juego, junto a torretblueprint
    //|||||||||| se declaran los nombres de las torretas, arriba y se asignan en base a su coste abajo          
    //VVVVVVVVVV
    //

    public void selectStandardTurret() {
        if (previewTurretOn) {
            return;
        }
        Node.buildTurretException = true;
        buildManager.selectedTorretToBuild(standarTurret);
        shopBtns[0].image.sprite = imgShop[3];  //Marco
        shopBtns[1].image.sprite = imgShop[7];
        shopBtns[2].image.sprite = imgShop[5];
    }
    
    public void selectPesadaBase() {
        if (previewTurretOn) {
            return;
        }
        Node.buildTurretException = true;
        buildManager.selectedTorretToBuild(pesadaBase);
        shopBtns[0].image.sprite = imgShop[6];
        shopBtns[1].image.sprite = imgShop[4];  //Marco
        shopBtns[2].image.sprite = imgShop[5];
    }

    public void selectAoEBase() {
        if (previewTurretOn) {
            return;
        }
        Node.buildTurretException = true;
        buildManager.selectedTorretToBuild(aoeBase);
        shopBtns[0].image.sprite = imgShop[6];
        shopBtns[1].image.sprite = imgShop[7];
        shopBtns[2].image.sprite = imgShop[2];  //Marco
    }

    //Para agregar una nuevea torreta, copiar alguno de los anteriores
    //y declarar un nuevo torretBluePrint con el nombre de la torreta y modificar el boton en el shop UI
}