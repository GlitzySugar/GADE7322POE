using System.Drawing;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using Color = UnityEngine.Color;

public class Node : MonoBehaviour
{
    [SerializeField] private Color hoverColor;

    private Renderer rend;
    private Color startColor;

    [HideInInspector]
    public GameObject turret; 
    [HideInInspector]
    public TurretBP turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private TurretBP turretToBuild;
    BuildManager buildManager;

    //Gets the rendere component and stores the the original color of the node
     void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    //is called when the mouse is clicked
    void OnMouseDown()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //if there is a turret on the preexisting node it shows that a new turret can be build there and returs
        if(turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        //if the can build parameter is empty is returns
        if (!buildManager.CanBuild)
        {
            return;
        }

        //builds a turret on the node
        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBP bPrint)
    {
        if (Currency.money < bPrint.cost)
        {
            Debug.Log("No money dog. Get ur bread up");
            return;
        }
        Currency.money -= bPrint .cost;
        GameObject _turret = (GameObject)Instantiate(bPrint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = bPrint;

        EnemySpawn.spawnScore += 1;
        Debug.Log("Built! Money Left" + Currency.money);
    }

    public void SellTurret()
    {
        Currency.money += turretBlueprint.GetSellAmount();

        //spawn effext

        Destroy(turret);
        turretBlueprint = null;
    }
    // is called when  the mouse hovers over a node
     void OnMouseEnter()
    {
        if (!buildManager.CanBuild)
        {
            return;
        }
      
        //if the player has enough money to build on node is grey
        if (buildManager.hasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            //if the player has no money the node is red when hovered 
            rend.material.color = Color.red;
        }
       
    }

    // called when the players mouse stops hovering over the node 
     void OnMouseExit()
    {
        //sets the node coler back to the original
        rend.material.color = startColor;
    }

    public void UpgradeTurret()
    {
        if (Currency.money < turretBlueprint.upgradeCost)
        {
            Debug.Log("No money to upgrade dog. Get ur bread up");
            return;
        }
        Currency.money -= turretBlueprint.upgradeCost;

        //destroys old turrret
        Destroy(turret);
        //build new one
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        EnemySpawn.spawnScore += 1;

        isUpgraded = true;
        Debug.Log("Upgraded! Money Left" + Currency.money);
    }
    public void UpgradeTurret2()
    {
        if (Currency.money < turretBlueprint.upgradeCost2)
        {
            Debug.Log("No money to upgrade dog. Get ur bread up");
            return;
        }
        Currency.money -= turretBlueprint.upgradeCost2;

        //destroys old turrret
        Destroy(turret);
        //build new one
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab2, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        EnemySpawn.spawnScore += 1;

        isUpgraded = true;
        Debug.Log("Upgraded! Money Left" + Currency.money);
    }

    //gets the position of the node 
    public Vector3 GetBuildPosition()
    {
        Vector3 temp;
        float x,y,z;
        temp = transform.position;
        x = transform.position.x;
        y = transform.position.y + 0.5f;
        z = transform.position.z;
        temp = new Vector3(x,y,z);
        return temp;
    }
}
