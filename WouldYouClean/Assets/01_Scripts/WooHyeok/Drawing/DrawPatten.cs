using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawPatten : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private RectTransform[] spots;
    [SerializeField] private string passWord;

    private LineRenderer line;
    private HashSet<RectTransform> completeSpot = new HashSet<RectTransform>();

    private Vector3 worldMousePosition;

    private string enterPassWord = null;
    private int UiLength;
    private int UiIdx;

    private void Start()
    {
        line = GetComponent<LineRenderer>();

        UiLength = spots.Length;
    }

    private void Update()
    {
        StartLine();
    }

    private void StartLine()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            worldMousePosition = cam.ScreenToWorldPoint(mousePosition);

            MouseOverUI(mousePosition);
        }
    }

    private bool ExistSet(int idx) => completeSpot.Contains(spots[idx]);

    private void MouseOverUI(Vector3 mousePosition)
    {
        for (int i = 0; i < UiLength; i++)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(spots[i], mousePosition, cam))
            {
                if (!ExistSet(i))
                {
                    VisitSpot(i);

                    break;
                }
            }
        }
    }

    private void VisitSpot(int idx)
    {
        completeSpot.Add(spots[idx]);
        UiIdx = idx;

        DrawLine(worldMousePosition);
    }

    private void DrawLine(Vector3 position)
    {
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(cam, spots[UiIdx].position);
        Vector3 result = Vector3.zero;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(spots[UiIdx], screenPos, cam, out result);

        line.positionCount++;
        line.SetPosition(line.positionCount - 1, result);

        SuccessPatten();
    }

    private void SuccessPatten()
    {
        enterPassWord += UiIdx.ToString();

        if (passWord == enterPassWord)
            print("Success!!!");
    }
}
