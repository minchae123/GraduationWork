using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Store : MonoBehaviour
{
    public bool IsInShop = false;

    [Header("Shop")]
    [SerializeField] private GameObject shop;

    private void Start()
    {
        shop.SetActive(false);
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

        IsInShop = true;
    }
    public void ExitShop()
    {
        Cursor.lockState = CursorLockMode.Locked;
        shop.SetActive(false);

        IsInShop = false;
    }
}
