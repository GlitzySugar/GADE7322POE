using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [SerializeField] private Color hoverColor;

    private Renderer rend;
    private Color startColor;

    private GameObject turret;

     void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void OnMouseDown()
    {
        if(turret != null)
        {
            Debug.Log("Can't build there");
            return;
        }

        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, new Vector3 (transform.position.x, transform.position.y + 1.2f, transform.position.z), transform.rotation);
    }
     void OnMouseEnter()
    {
            rend.material.color = hoverColor ;
    }
     void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
