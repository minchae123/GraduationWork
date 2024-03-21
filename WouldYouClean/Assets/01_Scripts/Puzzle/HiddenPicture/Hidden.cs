using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidden : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private HiddenSO hiddenSO;
    [SerializeField] private RectTransform spawnParent;

    private Dictionary<int, List<RectTransform>> differentPos = new Dictionary<int, List<RectTransform>>();

    private Vector3 worldMousePosition;

    private int level = 0;

    void Start()
    {
        //RectTransform obj = Instantiate(hiddenSO._left[0]);
        //obj.parent = spawnParent;
        //obj.localScale = Vector3.one;

        for (int i = 0; i < hiddenSO._left.Length; i++)
        {
            differentPos.Add(i, new List<RectTransform>());
            GetChildren(i, hiddenSO._left[i]);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            worldMousePosition = cam.ScreenToWorldPoint(mousePosition);

            MouseOverUI(mousePosition);
        }
    }

    private void MouseOverUI(Vector3 mousePosition)
    {
        for (int i = 0; i < differentPos[level].Count; i++)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(differentPos[level][i], mousePosition, cam))
            {
                DrawLine(worldMousePosition, i);
                break;
            }
        }
    }

    private void DrawLine(Vector3 position, int UiIdx)
    {
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(cam, differentPos[level][UiIdx].position);
        Vector3 result = Vector3.zero;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(differentPos[level][UiIdx], screenPos, cam, out result);
        //Debug.Log(result);
    }

    private void Assignment(Sprite sprite, int idx)
    {

    }

    private void GetChildren(int keyNum, RectTransform parent)
    {
        foreach (Transform item in parent)
        {
            differentPos[keyNum].Add(item.GetComponent<RectTransform>());
        }
    }
}
