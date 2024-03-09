using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class DrawPatten : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private RectTransform[] spots;

    private Vector3 mousePosition;
    private LineRenderer line;
    private int count;

    private float minDis = 100f;
    private float dis;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();

        mousePosition = Input.mousePosition;
        count = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    line.SetPosition(0, WorldToLocal(Input.mousePosition));
        //    print(WorldToLocal(spots[0].position));
        //}
        //else if(Input.GetMouseButton(0))
        //{
        //    print("ss");
        //    line.SetPosition(count, WorldToLocal(Input.mousePosition));
        //}
        //if(Input.GetMouseButtonUp(0))
        //{
        //    count++;
        //}
        CrossSpot();
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 worldPosition = ConvertUIToWorldCoordinates(mousePosition);
        //    Debug.Log("UI Element World Position: " + worldPosition);
        //}
    }

    private void CrossSpot()
    {
        Distance();

        if (Input.GetMouseButtonDown(0))
        {
            if (minDis <= 1)
                line.SetPosition(0, WorldToLocal(Input.mousePosition));
            else
                return;
        }
        else if (Input.GetMouseButton(0))
        {
            Distance();

            if (minDis <= 1)
            {
                Debug.Log(minDis);
                line.SetPosition(count, WorldToLocal(Input.mousePosition));
                count++;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            line.positionCount = 2;
        }
    }

    private void Distance()
    {
        minDis = 100;

        for (int i = 0; i < spots.Length; i++)
        {
            dis = Vector2.Distance(WorldToLocal(mousePosition), ConvertUIToWorldCoordinates(mousePosition, spots[i]));

            if (dis < minDis)
                minDis = dis;
        }
    }

    private Vector2 WorldToLocal(Vector3 pos)
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(pos);
        Vector2 camRelative = cam.transform.InverseTransformPoint(mousePos);

        return camRelative;
    }

    private Vector3 ConvertUIToWorldCoordinates(Vector2 screenPos, RectTransform rect)
    {
        // 스크린 좌표를 UI 요소의 로컬 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPos, cam, out Vector2 localPoint);

        // UI 요소의 로컬 좌표를 월드 좌표로 변환
        return rect.TransformPoint(localPoint);
    }
}
