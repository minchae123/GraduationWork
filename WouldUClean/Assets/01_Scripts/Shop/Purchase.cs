using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Purchase : MonoSingleton<Purchase>
{
    private PlayerHp upgradePlayer;

    [Header("Stat")]
    private int O2Lv = 1;
    private int HPLv = 1;
    private int UFOLv = 1;
    //

    [Header("Fill UI")]
    [SerializeField] private Transform O2stat;
    [SerializeField] private Transform HPstat;
    [SerializeField] private Transform UFOstat;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI coinText;

    private void Awake()
    {
        upgradePlayer = GameManager.Instance._playerTrm.GetComponent<PlayerHp>();
    }
    private void Start()
    {
        coinText.text = "Loading. . .";
    }

    public void EnterShop()
    {
        coinText.text = "Loading. . .";
        CoinTextTyping();
    }
    public void ResetShop()
    {
        O2stat.DOScaleX(0, 0.2f);
        HPstat.DOScaleX(0, 0.2f);
        UFOstat.DOScaleX(0, 0.2f);
    }
    private void CoinTextTyping()
    {
        coinText.maxVisibleCharacters = 0;
        DOTween.To(x => coinText.maxVisibleCharacters = (int)x, 0f, coinText.text.Length, 0.3f).SetUpdate(true);
    }
    public void SetPlayerStatLevel()
    {
        // 혹시 모르니까
        O2Lv = Mathf.Clamp(O2Lv, 0, 5);
        HPLv = Mathf.Clamp(HPLv, 0, 5);
        UFOLv = Mathf.Clamp(UFOLv, 0, 5);

        coinText.text = $"{Coin.Instance.currentCoin}원";
        coinText.transform.DOScale(1.02f, 0.1f).
            OnComplete(() => coinText.transform.DOScale(1f, 0.05f).SetUpdate(true))
            .SetUpdate(true);

        O2stat.DOScaleX(1f / 5f * O2Lv, 0.2f).SetUpdate(true);
        HPstat.DOScaleX(1f / 5f * HPLv, 0.2f).SetUpdate(true);
        UFOstat.DOScaleX(1f / 5f * UFOLv, 0.2f).SetUpdate(true);
    }

    public bool CheckCanBuy(PurchaseItem type)
    {
        switch (type)
        {
            case PurchaseItem.O2Tank:
                if (O2Lv >= 5) return false;
                break;
            case PurchaseItem.HP:
                if (HPLv >= 5) return false;
                break;
            case PurchaseItem.UFO:
                if (UFOLv >= 5) return false;
                break;
            default:
                break;
        }
        return true;
    }

    // purchase 
    public void BuyItem(ShopItemSO current)
    {
        PurchaseItem type = current.Item;

        switch (type)
        {
            case PurchaseItem.Debt:
                {
                    //current.DebtPayBack();
                    break;
                }
            
            case PurchaseItem.Inventory:
                {
                    Inventory.Instance.UpgradeInventory(); 
                
                    break;
                }

            case PurchaseItem.O2Tank:
                {
                    upgradePlayer.OnlimitBreath(20);
                    ++O2Lv;
                    break;
                }

            case PurchaseItem.HP:
                {
                    upgradePlayer.OnlimitHp(20);
                    ++HPLv;
                    break;
                }

            case PurchaseItem.UFO:
                {
                    ++UFOLv; // 기름 추가
                    break;
                }
            
            case PurchaseItem.Item:
                break;
            
            default:
                break;
        }

        SetPlayerStatLevel();
    }
}
