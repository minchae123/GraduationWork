using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Interaction : MonoBehaviour
{
    [SerializeField] private RectTransform _sellTable;
    StarterAssetsInputs _input;

    private DivideObj _cleanItem;
    private RaycastHit hit;

    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 100, Color.red);

        if (_input.interaction)
        {
            if (TrashRay())
            {
                switch (hit.collider.tag)
                {
                    case "Trash":
                        _cleanItem = hit.transform.GetComponent<DivideObj>();
                        CleanItem();
                        break;
                    case "Sell":
                        UIManager.Instance.ShowPanel(_sellTable);
                        break;
                }
            }
            else
                _input.interaction = false;

        }
    }

    private bool TrashRay() =>
        Physics.Raycast(transform.position, transform.forward, out hit, 10f);

    private void CleanItem()
    {
        CollectedPlanets.Instance.AddTrashCollected(_cleanItem);//도감에 추가
        _cleanItem.PickUpItem();
    }
}
