using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(LineRenderer))]
public class DrawPatten : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private RectTransform[] spots;

    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // 마우스 왼쪽 버튼이 클릭되었을 때
        if (Input.GetMouseButton(0))
        {
            // UI 이벤트를 무시하고 마우스 위치를 가져옴
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("ss");
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 10; // 원하는 Z 값으로 조정(카메라와의 거리)

                Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

                bool isOverUI = IsMouseOverUI(mousePosition);

                // UI 이미지를 지나면 LineRenderer 위치 추가
                if (isOverUI)
                {
                    AddPositionToLineRenderer(worldMousePosition);
                }
            }
        }
    }

    // 마우스 위치가 UI 이미지 위에 있는지 여부를 반환
    bool IsMouseOverUI(Vector3 mousePosition)
    {
        foreach (RectTransform uiElement in spots)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(uiElement, mousePosition))
            {
                return true;
            }
        }
        return false;
    }

    // LineRenderer에 위치 추가
    void AddPositionToLineRenderer(Vector3 position)
    {
        line.positionCount++;
        line.SetPosition(line.positionCount - 1, position);
    }
}
