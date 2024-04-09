using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class Store : MonoSingleton<Store>
{
    public bool IsInShop = false;

    [Header("ShopUI")]
    [SerializeField] private GameObject shop;
    
    // ===================================

    [Header("===================================")] 
    /*[Header("Stat")]
    // 여긴 나중에 다른 스크립트로 변경
    private int O2Lv = 1;
    private int HPLv = 1;
    private int UFOLv = 1;
    //

    [Header("Fill UI")]
    [SerializeField] private Transform O2stat;
    [SerializeField] private Transform HPstat;
    [SerializeField] private Transform UFOstat;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI coinText;*/

    // ===================================

    [Header("===================================")]
    [Header("Purchase")]
    [SerializeField] private SellingItemList SellingItemList;
    [SerializeField] private Transform purchaseScrollViewParent;
    
    [SerializeField] private PurchaseSlotUI purchaseSlotUI;
    [SerializeField] private PurchaseSlotUI[] purchaseSlots;

    private ScrollRect scrollRect;
    private UpgradeSys upgrade;

    [Header("===================================")]
    [Header("CheckPanel")]
    [SerializeField] private ShopItemSO currentItem;
    [SerializeField] private Transform checkPanel;

    [Header("UI")]
    private TextMeshProUGUI itemPrice;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI itemInfo;
    private Image itemImage;

    private void Awake()
    {
        itemName = checkPanel.Find("InfoContainer/NameText").GetComponentInChildren<TextMeshProUGUI>();
        itemInfo = checkPanel.Find("InfoContainer/InfoText").GetComponentInChildren<TextMeshProUGUI>();
        itemPrice = checkPanel.Find("ButtonContainer/YES/Text (TMP)").GetComponentInChildren<TextMeshProUGUI>();
        itemImage = checkPanel.Find("InfoContainer/ItemImage").GetComponentInChildren<Image>();

        scrollRect = purchaseScrollViewParent.GetComponentInParent<ScrollRect>();
        upgrade = GetComponent<UpgradeSys>();
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

        upgrade.EnterShop();

        shop.transform.DOScaleY(1, 0.6f).SetEase(Ease.OutCubic).SetUpdate(true).OnComplete(() => { SetAnimPurchaseList(); upgrade.SetPlayerStatLevel(); });

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

    #region checkPanel
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
            Debug.LogWarning("아이템 정보 체크 바람");
        }

        if(upgrade.CheckCanBuy(type)) // 최대 업그레이드 아직X
        {        
            // currentItem 돈 만큼 제외
            Coin.Instance.RemoveCoin(currentItem.itemPrice);
            upgrade.BuyItem(type);
        }
        else // 최대 업그레이드일 경우
        {
            print("업그레이드 끝남"); // 추후 최대 업그레이드 된 건 구매 불가로 만들 예정
        }
        OnClickBtn();
    }
    public void OnClickBtn()
    {
        checkPanel.DOScaleY(0f, 0.15f).SetUpdate(true).OnComplete(() => checkPanel.gameObject.SetActive(false));
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

    private void ResetShop()
    {
        checkPanel.DOScaleY(0, 0.15f).OnComplete(() => checkPanel.gameObject.SetActive(false));
        upgrade.ResetShop();

        for (int i = 0; i < SellingItemList.itemList.Count; i++)
        {
            purchaseSlots[i].transform.DOScaleX(0, 0.1f);
        }
        OnClickBtn();
    }
}
