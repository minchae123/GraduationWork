using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawPatten : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private RectTransform[] spots;

    private LineRenderer line;
    private HashSet<RectTransform> completeSpot = new HashSet<RectTransform>();

    private int UiLength;
    private int UiIdx;

    private void Start()
    {
        line = GetComponent<LineRenderer>();

        UiLength = spots.Length;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            Vector3 worldMousePosition = cam.ScreenToWorldPoint(mousePosition);

            bool isOverUI = IsMouseOverUI(mousePosition);

            if (isOverUI)
                AddPositionToLineRenderer(worldMousePosition);
        }
    }

    private bool IsMouseOverUI(Vector3 mousePosition)
    {
        for (int i = 0; i < UiLength; i++)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(spots[i], mousePosition, cam))
            {
                if (!completeSpot.Contains(spots[i]))
                {
                    completeSpot.Add(spots[i]);
                    UiIdx = i;

                    return true;

                }
            }
        }

        return false;
    }

    private void AddPositionToLineRenderer(Vector3 position)
    {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(spots[UiIdx].position);
        //https://blog.naver.com/bysmk14/221313438577
        //유니티 UI의 월드 좌표
        line.positionCount++;
        line.SetPosition(line.positionCount - 1, screenPoint);
    }
}
