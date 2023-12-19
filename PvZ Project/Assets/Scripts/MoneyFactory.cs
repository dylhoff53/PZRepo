using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyFactory : MonoBehaviour
{
    public float maxTime;
    public float timer;
    public int moneyValue;


    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= maxTime)
        {
            timer = 0f;
            Money();
        }
    }

    public void Money()
    {
        PlayerStats.Money += moneyValue;
    }
}
