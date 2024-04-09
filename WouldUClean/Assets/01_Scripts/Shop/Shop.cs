using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoSingleton<Shop>
{
    public bool IsInShop = false;

    [Header("ShopUI")]
    [SerializeField] private GameObject shop;
    
    [Header("===================================")]
    [Header("Purchase")]
    [SerializeField] private SellingItemList SellingItemList;
    [SerializeField] private Transform purchaseScrollViewParent;
    
    [SerializeField] private PurchaseSlotUI purchaseSlotUI;
    [SerializeField] private PurchaseSlotUI[] purchaseSlots;

    private ScrollRect scrollRect;
    private Purchase purchaseSys;

    [Header("===================================")]

    [Header("===================================")]

    [Header("CheckPanel")]
    [SerializeField] private ShopItemSO currentItem;
    [SerializeField] private Transform checkPanel;
    [SerializeField] private Transform debtPanel;

    [Header("UI")]
    private TextMeshProUGUI itemPrice;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI itemInfo;
    private Image itemImage;

    [Header("TEST")]
    [SerializeField] private TextMeshProUGUI testText;

    private void Awake()
    {
        itemName = checkPanel.Find("InfoContainer/NameText").GetComponentInChildren<TextMeshProUGUI>();
        itemInfo = checkPanel.Find("InfoContainer/InfoText").GetComponentInChildren<TextMeshProUGUI>();
        itemPrice = checkPanel.Find("ButtonContainer/YES/Text (TMP)").GetComponentInChildren<TextMeshProUGUI>();
        itemImage = checkPanel.Find("InfoContainer/ItemImage").GetComponentInChildren<Image>();

        scrollRect = purchaseScrollViewParent.GetComponentInParent<ScrollRect>();
        purchaseSys = GetComponent<Purchase>();
    }
    private void Start()
    {
        shop.SetActive(false);
        shop.transform.localScale = new Vector3(1, 0, 1);

        purchaseSlots = new PurchaseSlotUI[SellingItemList.itemList.Count];
        SetPurchaseItem();

        ResetShop();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            if (UIManager.Instance.IsInSetting || MapInfoUI.Instance.IsInMap) return;

            if (!IsInShop) EnterShop();
            else ExitShop();
        }
    }

    // enter & exit
    public void EnterShop()
    {
        Cursor.lockState = CursorLockMode.None;

        shop.SetActive(true);

        purchaseSys.EnterShop();

        shop.transform.DOScaleY(1, 0.6f).SetEase(Ease.OutCubic).SetUpdate(true).OnComplete(() => { SetAnimPurchaseList(); purchaseSys.SetPlayerStatLevel(); });

        IsInShop = true;

        Time.timeScale = 0;
    }
    public void ExitShop()
    {
        Cursor.lockState = CursorLockMode.Locked;

        shop.transform.DOScaleY(0, 0.4f).SetEase(Ease.InCubic).SetUpdate(true).OnComplete(() => shop.SetActive(false));

        IsInShop = false;

        Time.timeScale = 1;
        ResetShop();
    }
    private void ResetShop()
    {
        checkPanel.DOScaleY(0, 0.15f).OnComplete(() => checkPanel.gameObject.SetActive(false));
        debtPanel.DOScaleY(0, 0.15f).OnComplete(() => debtPanel.gameObject.SetActive(false));
        purchaseSys.ResetShop();

        for (int i = 0; i < SellingItemList.itemList.Count; i++)
        {
            purchaseSlots[i].transform.DOScaleX(0, 0.1f);
        }
        OnClickBtn();
    }

    #region PurchaseCheckPanel
    public void SetCheckPanel(ShopItemSO item)
    {
        checkPanel.gameObject.SetActive(true);
        checkPanel.DOScaleY(1f, 0.2f).SetUpdate(true);

        currentItem = item;
        itemName.text = currentItem.itemName;
        itemInfo.text = currentItem.itemInfo;
        itemPrice.text = $"{currentItem.itemPrice}원";
        itemImage.sprite = currentItem.itemIcon;

        OnMoveWhileCheckPanel(false);
    }
    public void OnClickBuyButton()
    {
        PurchaseItem type = currentItem.Item;
        if (type == PurchaseItem.NONE)
        {
            Debug.LogWarning("아이템 정보 체크 바람"); return;
        }

        Coin.Instance.RemoveCoin(currentItem.itemPrice);
        purchaseSys.BuyItem(currentItem);
        OnClickBtn();
    }
    public void OnClickBtn()
    {
        checkPanel.DOScaleY(0f, 0.15f).SetUpdate(true).OnComplete(() => checkPanel.gameObject.SetActive(false));
        debtPanel.DOScaleY(0f, 0.15f).SetUpdate(true).OnComplete(() => debtPanel.gameObject.SetActive(false));
        currentItem = null;

        OnMoveWhileCheckPanel(true);
    }
    public void OnMoveWhileCheckPanel(bool b)
    {
        for (int i = 0; i < purchaseSlots.Length; i++)
        {
            purchaseSlots[i].ButtonInteractive(b);
        }

        scrollRect.vertical = b;
    }
    #endregion

    public void SetDebtPanel(ShopItemSO item)
    {
        debtPanel.gameObject.SetActive(true);
        debtPanel.DOScaleY(1f, 0.2f).SetUpdate(true);

        currentItem = item;
    }

    public void OnClickDebt()
    {
        int value = 0;
        string txt = testText.text.Replace("\u200B", "");

        if (int.TryParse(txt, out value))
        {
            Coin.Instance.RemoveCoin(value);
            purchaseSys.BuyItem(currentItem);
            OnClickBtn();
        }
        else
        {
            print("숫자가 아님니다");
        }

        testText.text = string.Empty;
    }

    private void SetAnimPurchaseList()
    {
        for (int i = 0; i < SellingItemList.itemList.Count; i++)
        {
            purchaseSlots[i].transform.DOScaleX(1, 0.1f).SetUpdate(true);
        }
    }
    public void SetPurchaseItem()
    {
        for(int i=0;i< SellingItemList.itemList.Count; i++)
        {
            PurchaseSlotUI slot = Instantiate(purchaseSlotUI, purchaseScrollViewParent);
            slot.SetItem(SellingItemList.itemList[i]);

            purchaseSlots[i] = slot;
            purchaseSlots[i].transform.localScale = new Vector3(0, 1, 1);
        }
    }
    

}