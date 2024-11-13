using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBP 
{
    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public GameObject upgradedPrefab2;
    public int upgradeCost;
    public int upgradeCost2;

    public int GetSellAmount()
    {
        return cost/2;
    }
}
