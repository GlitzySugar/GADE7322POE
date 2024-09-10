using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;

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

    public bool CanBuild { get { return turretToBuild != null; } }

    public void BuildTurretOn(Node node)
    {

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;
    }
    public void SelectTurretToBuild(TurretBP turret)
    {
        turretToBuild = turret;
    }
}
