using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;

    //so there wont be 2 instances created if more than one build manger is in scene
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one buildManager");
            return;
        }
        instance = this;
    }

    public GameObject standardTurretPrefab;



    private TurretBP turretToBuild;
    private Node selectedNode;

    public NodeUI nodeUI;

    //parameter for checking if a node can have a turrret placed on it
    public bool CanBuild { get { return turretToBuild != null; } }

    //parameter for checking if the user has enough money to build a turret
    public bool hasMoney { get { return Currency.money >= turretToBuild.cost; } }

    //Subtracts the cost of the turret and builds the selected turret on the selected node 
    //public void BuildTurretOn(Node node)
    //{
    //    if(Currency.money < turretToBuild.cost) {
    //        Debug.Log("No money dog. Get ur bread up");
    //        return; }
    //    Currency.money -= turretToBuild.cost;
    //    GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
    //    node.turret = turret;
    //    EnemySpawn.spawnScore += 1;
    //    Debug.Log("Built! Money Left" + Currency.money);
    //}

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }
    public void SelectTurretToBuild(TurretBP turret)
    {
        turretToBuild = turret;
        selectedNode = null;

        nodeUI.Hide();
    }
    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public TurretBP GetTurretToBuild()
    {
        return turretToBuild;
    }
}
