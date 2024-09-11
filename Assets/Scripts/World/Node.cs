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
        //if the can build parameter is empty is returns
        if(!buildManager.CanBuild)
        {
            return;
        }

        //if there is a turret on the preexisting node it shows that a new turret can be build there and returs
        if(turret != null)
        {
            Debug.Log("Can't build there");
            return;
        }

        //builds a turret on the node
        buildManager.BuildTurretOn(this);
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

    //gets the position of the node 
    public Vector3 GetBuildPosition()
    {
        return transform.position;
    }
}
