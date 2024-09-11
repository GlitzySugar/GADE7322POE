using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public TurretBP standardTurret;
    BuildManager buildManager;
    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    //selecting the turret from the store panel in the canvas
    public void SelectStandardTurret()
    {
        Debug.Log("Standard Turret Purchased");
        buildManager.SelectTurretToBuild(standardTurret);
    }
}
