using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transparencia : MonoBehaviour {

    private Renderer rend;
    public Color ColorInicial;
    public upgradeTowerPanel UpPanelComp;

    // Use this for initialization
    void Start() {

        rend = GetComponent<Renderer>();
        rend.material.color = ColorInicial;
    }

    private void Update() {
        transform.localScale = new Vector3(UpPanelComp.selectedTurretComponent.rango * 2, 0.001f, UpPanelComp.selectedTurretComponent.rango * 2);
    }

}
