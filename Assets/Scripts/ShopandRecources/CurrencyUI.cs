using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    public Text currencyText;

    // Update is called once per frame
    void Update()
    {
        //makes the ui show how much gold the player has
        currencyText.text = "Gold: " + Currency.money.ToString();
    }
}
