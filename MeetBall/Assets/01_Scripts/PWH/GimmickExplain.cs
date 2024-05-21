using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickExplain : MonoBehaviour
{
    private void Update()
    {
        CickObj();
    }

    private void CickObj()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
