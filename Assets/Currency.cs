using UnityEngine;

public class Currency : MonoBehaviour
{
    public static int money;
    public int startMoney = 400;

    private void Start()
    {
        money = startMoney;
    }
}
