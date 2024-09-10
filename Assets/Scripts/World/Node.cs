using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [SerializeField] private Color hoverColor;

    private Renderer rend;
    private Color startColor;

    [Header("Optional")]
    public GameObject turret;


    BuildManager buildManager;

     void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    void OnMouseDown()
    {
        if(!buildManager.CanBuild)
        {
            return;
        }
        if(turret != null)
        {
            Debug.Log("Can't build there");
            return;
        }

        buildManager.BuildTurretOn(this);
    }
     void OnMouseEnter()
    {
        if (!buildManager.CanBuild)
        {
            return;
        }
        rend.material.color = hoverColor ;
    }
     void OnMouseExit()
    {
        rend.material.color = startColor;
    }
    public Vector3 GetBuildPosition()
    {
        return transform.position;
    }
}
