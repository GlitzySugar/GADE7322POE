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
    public TMP_Text upgradeCost2;
    public TMP_Text sellCost;
    public Button upgradeButton;
    public Button upgradeButton2;
    private Node target;
    
    public void SetTarget(Node _target)
    {
        target = _target; 
        

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeCost2.text = "$" + target.turretBlueprint.upgradeCost2;
            upgradeButton.interactable = true;
            upgradeButton2.interactable = true;
        }
        else
        {
            upgradeCost.text = "Maxed";
            upgradeCost2.text = "Maxed";
            upgradeButton.interactable = false;
            upgradeButton2.interactable = false;
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
    public void Upgrade2()
    {
        target.UpgradeTurret2();
        BuildManager.instance.DeselectNode();
    }
    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
