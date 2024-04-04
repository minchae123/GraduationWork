using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoSingleton<Coin>, ISaveManager
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

	public void LoadData(GameData data)
	{
        currentCoin = data.coin;
	}

	public void SaveData(ref GameData data)
	{
		data.coin = currentCoin;
	}
}
