using System;
using System.Collections;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public static int money;
    public int startMoney = 400;
    private bool gameRunning = true;

    //sets current money to the games start value of money 
    private void Start()
    {
        StartCoroutine(CurrencywGenerator());
    money = startMoney;
    }

    //passively gain gold as the game is running 
    IEnumerator CurrencywGenerator()
    {   
        while (gameRunning)
        {
            yield return new WaitForSeconds(1f);
            money += 5;
        }
    }
}
