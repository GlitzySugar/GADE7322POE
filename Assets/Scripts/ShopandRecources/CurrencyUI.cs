using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    public Text currencyText;
    public Text scoreTxt;

    // Update is called once per frame
    void Update()
    {
        //makes the ui show how much gold the player has
        scoreTxt.text = "Score: " + EnemyTargets.score.ToString();
        currencyText.text = "Gold: " + Currency.money.ToString();
    }
}
