using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoSingleton<Coin>
{
    [SerializeField] private TextMeshProUGUI coinText;

    public int currentCoin = 0;

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
        coinText.text = $"{currentCoin}¿ø";
    }
}
