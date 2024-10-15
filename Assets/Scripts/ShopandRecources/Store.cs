using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public TurretBP standardTurret;
    public TurretBP aoeDamageTurret;
    public TurretBP aoeSlowTurret;
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
    public void SelectAOEDamageTurret()
    {
        Debug.Log("Aoe Damage Turret Purchased");
        buildManager.SelectTurretToBuild(aoeDamageTurret);
    }
    public void AOESlowTurret()
    {
        Debug.Log("Aoe Slow Turret Purchased");
        buildManager.SelectTurretToBuild(aoeSlowTurret);
    }
}
