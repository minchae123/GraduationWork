using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoSingleton<Coin>
{
    public int currentCoin = 0;
    public void AddCoin(int amount)
    {
        currentCoin += amount;
    }
    public void RemoveCoin(int amount)
    {
        currentCoin -= amount;
    }
}
