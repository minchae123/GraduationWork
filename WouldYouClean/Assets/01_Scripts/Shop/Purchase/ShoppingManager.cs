using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShoppingManager : MonoBehaviour
{
    public static ShoppingManager Instance;
    
    // ���߿� ��𿡼� �޾ƿ��� (���Ƿ� ���� �����ص�)
    public int currentCoin;
    [SerializeField] private TextMeshProUGUI coinText;
    
    [SerializeField] private PopUpShop popUpPrefab;
    [SerializeField] private Transform checker;
    
    private void Awake()
    {
        if (Instance != null) print("ShoppingManager����");   
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
        SetPopUpText($"�����ϼ̽��ϴ�.\n���� �� �ܾ�: {currentCoin}");
    }

    public void SetPopUpText(string text)
    {
        PopUpShop pop = Instantiate(popUpPrefab, checker);
        pop.SetText(text);
    }
}
