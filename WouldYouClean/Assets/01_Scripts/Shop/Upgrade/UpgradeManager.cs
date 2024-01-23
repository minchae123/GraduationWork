using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoSingleton<UpgradeManager>
{
    public Transform shop;
    public UpgradeTable itemTable;

    private void Start()
    {
        shop.gameObject.SetActive(false);
    }

    public void EnterUpgradeShop()
    {
        shop.gameObject.SetActive(true);
    }
    public void ExitUpgradeShop()
    {
        shop.gameObject.SetActive(false);
    }
    public void SetItem(ShopItemSO item)
    {
        itemTable.SetItem(item);
    }
    public void UpgradeInventory(int price)
    {
        Inventory.Instance.UpgradeInventory();
        Coin.Instance.RemoveCoin(price);
    }

    public void UpgradeCleaner(int price)
    {
        print("청소기 업그레이드");
        Coin.Instance.RemoveCoin(price);
    }
}
