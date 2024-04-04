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

    [Header("Stat")]
    // 여긴 나중에 다른 스크립트로 변경
    private int O2Lv = 1;
    private int HPLv = 1;
    private int UFOLv = 1;
    //

    [SerializeField] private Transform O2stat;
    [SerializeField] private Transform HPstat;
    [SerializeField] private Transform UFOstat;

    // ===================================

    [Header("Purchase")]
    [SerializeField] private SellingItemList SellingItemList;
    [SerializeField] private Transform purchaseScrollViewParent;
    [SerializeField] private PurchaseSlotUI purchaseSlotUI;

    [SerializeField] private PurchaseSlotUI[] purchaseSlots;

    [Header("CheckPanel")]
    [SerializeField] private Transform checkPanel;
    [SerializeField] private ShopItemSO currentItem;

    private TextMeshProUGUI itemName;
    private TextMeshProUGUI itemInfo;
    private Image itemImage;

    private void Awake()
    {
        itemName = checkPanel.Find("NameText").GetComponentInChildren<TextMeshProUGUI>();
        itemInfo = checkPanel.Find("InfoText").GetComponentInChildren<TextMeshProUGUI>();
        itemImage = checkPanel.Find("ItemImage").GetComponentInChildren<Image>();
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
        shop.transform.DOScaleY(1, 0.6f).SetEase(Ease.OutCubic).SetUpdate(true).OnComplete(() => SetPlayerStatLevel());

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
        itemImage.sprite = currentItem.itemIcon;

        for(int i =0;i< purchaseSlots.Length; i++)
        {
            purchaseSlots[i].ButtonInteractive(false);
        }
    }
    public void OnClickBuyButton()
    {
        // currentItem 돈 만큼 제외
        OnClickBtn();
    }
    public void OnClickBtn()
    {
        checkPanel.DOScaleY(0f, 0.15f).SetUpdate(true).OnComplete(() => checkPanel.gameObject.SetActive(false));
        currentItem = null;

        for (int i = 0; i < purchaseSlots.Length; i++)
        {
            purchaseSlots[i].ButtonInteractive(true);
        }
    }
    #endregion

    private void SetPlayerStatLevel()
    {
        O2stat.DOScaleX(1f / 5f * O2Lv, 0.2f).SetUpdate(true);
        HPstat.DOScaleX(1f / 5f * HPLv, 0.2f).SetUpdate(true);
        UFOstat.DOScaleX(1f / 5f * UFOLv, 0.2f).SetUpdate(true).OnComplete(() => SetAnimPurchaseList());
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

    private void ResetShop()
    {
        checkPanel.gameObject.SetActive(false);
        checkPanel.localScale = new Vector3(1, 0, 1);

        O2stat.DOScaleX(0, 0.2f);
        HPstat.DOScaleX(0, 0.2f);
        UFOstat.DOScaleX(0, 0.2f).OnComplete(()=>
        {
            for (int i = 0; i < SellingItemList.itemList.Count; i++)
            {
                purchaseSlots[i].transform.DOScaleX(0, 0.1f);
            }
        });

        OnClickBtn();
    }
}
