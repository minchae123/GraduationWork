using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivideObj : MonoBehaviour
{
    public ObjectType type;


    public void PickUpItem() // Ãß°¡
    {
        Inventory.Instance.AddItem(type);
        Destroy(gameObject);
    }
}
