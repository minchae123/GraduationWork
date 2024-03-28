using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Interaction : MonoBehaviour
{
    StarterAssetsInputs _input;

    private DivideObj _cleanItem;
    private RaycastHit hit;

    private string leftTag = "LMouse";
    private string rightTag = "RMouse";

    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        if (_input.LMouseClick)
            _input.LMouseClick = DivGrab(leftTag);
        else if(_input.RMouseClick)
            _input.RMouseClick = DivGrab(rightTag);
    }

    private bool DivGrab(string tag)
    {
        if (TrashRay())
        {
            switch (hit.collider.tag)
            {
                default:
                    EmptyGrab(tag);
                    break;
            }
        }
        else
            EmptyGrab(tag);

        return false;

    }

    private bool TrashRay() =>
        Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out hit, 10f);

    private void EmptyGrab(string tag)
    {
        foreach (Transform child in transform)
            if (child.TryGetComponent<Grab>(out Grab grab) && grab.CompareTag(tag))
            {
                grab.EmptyGrab();
            }
    }
}
