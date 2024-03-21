using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoSingleton<Coin>, ISaveManager
{
    [SerializeField] private TextMeshProUGUI coinText;

    public int currentCoin = 0;

    private void Start()
    {
        coinText.text = $"{currentCoin}��";
    }

    public void AddCoin(int amount)
    {
        currentCoin += amount;
        UpdateCoinText();
    }
    public void RemoveCoin(int amount)
    {
        currentCoin -= amount;
        UpdateCoinText();
    }

    public void UpdateCoinText()
    {
        coinText.text = $"{currentCoin}��";
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