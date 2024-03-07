using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class DrawPatten : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private LineRenderer line;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();

        count = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            line.SetPosition(0, WorldToLocal(Input.mousePosition));
        }
        else if(Input.GetMouseButton(0))
        {
            line.SetPosition(count, WorldToLocal(Input.mousePosition));
        }
        if(Input.GetMouseButtonUp(0))
        {
            count++;
        }
    }

    private Vector2 WorldToLocal(Vector3 pos)
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(pos);
        Vector2 camRelative = cam.transform.InverseTransformPoint(mousePos);

        return camRelative;
    }
}
