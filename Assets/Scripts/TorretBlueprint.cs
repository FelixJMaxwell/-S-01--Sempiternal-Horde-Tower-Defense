using System.Collections;
using UnityEngine;


//Al agregar lo siguiente, las variables publicas seran visibles en el inspector sin agregar el script
//a ningun GameObject
[System.Serializable]
public class TorretBlueprint{
    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab1;
    public GameObject upgradedPrefab2;
    public GameObject upgradedPrefab3;

    public float upgradingCost = 0;

    public int getSellAmount() {
        return cost / 2;
    }
}
