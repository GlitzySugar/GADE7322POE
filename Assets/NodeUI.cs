using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    public TMP_Text upgradeCost;
    public TMP_Text sellCost;
    public Button upgradeButton;
    private Node target;
    
    public void SetTarget(Node _target)
    {
        target = _target; 
        

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "Maxed";
            upgradeButton.interactable = false;
        }

        sellCost.text = "$" + target.turretBlueprint.GetSellAmount();

        ui.SetActive(true);
    }
    public void Hide ()
    {
        ui.SetActive(false);
    }
    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }
    
    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
