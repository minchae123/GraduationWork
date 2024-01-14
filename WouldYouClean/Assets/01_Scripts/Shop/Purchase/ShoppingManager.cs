using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShoppingManager : MonoBehaviour
{
    public static ShoppingManager Instance;
    
    // 나중에 어디에서 받아오고 (임의로 변수 선언해둠)
    public int currentCoin;
    [SerializeField] private TextMeshProUGUI coinText;
    
    [SerializeField] private PopUpShop popUpPrefab;
    [SerializeField] private Transform checker;
    
    private void Awake()
    {
        if (Instance != null) print("ShoppingManager오류");   
        Instance = this;
    }

    private void Start()
    {
        coinText.text = $"{currentCoin}";
    }

    public int CheckCurrentCoin()
    {
        return currentCoin;
    }

    public void RemoveCoin(int coin)
    {
        currentCoin -= coin;
        coinText.text = $"{currentCoin}";
        SetPopUpText($"구매하셨습니다.\n구매 후 잔액: {currentCoin}");
    }

    public void SetPopUpText(string text)
    {
        PopUpShop pop = Instantiate(popUpPrefab, checker);
        pop.SetText(text);
    }
}
