using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Interaction : MonoBehaviour
{
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
                _cleanItem = hit.transform.GetComponent<DivideObj>();
                CleanItem();
            }
            else
                _input.interaction = false;

        }
    }

    private bool TrashRay() =>
        Physics.Raycast(transform.position, transform.forward, out hit, 100f, LayerMask.GetMask("Trash"));

    private void CleanItem()
    {
        CollectedPlanets.Instance.AddTrashCollected(_cleanItem);//도감에 추가
        _cleanItem.PickUpItem();
    }
}
