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
        // ���콺 ���� ��ư�� Ŭ���Ǿ��� ��
        if (Input.GetMouseButton(0))
        {
            // UI �̺�Ʈ�� �����ϰ� ���콺 ��ġ�� ������
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("ss");
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 10; // ���ϴ� Z ������ ����(ī�޶���� �Ÿ�)

                Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

                bool isOverUI = IsMouseOverUI(mousePosition);

                // UI �̹����� ������ LineRenderer ��ġ �߰�
                if (isOverUI)
                {
                    AddPositionToLineRenderer(worldMousePosition);
                }
            }
        }
    }

    // ���콺 ��ġ�� UI �̹��� ���� �ִ��� ���θ� ��ȯ
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

    // LineRenderer�� ��ġ �߰�
    void AddPositionToLineRenderer(Vector3 position)
    {
        line.positionCount++;
        line.SetPosition(line.positionCount - 1, position);
    }
}
