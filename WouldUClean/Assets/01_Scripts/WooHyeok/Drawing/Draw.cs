using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LineType
{
	NONE = 0,
	WIDTH = 1,
	LENGTH = 2,
	V = 3,
	REVERSEV = 4,
	THUNDER = 5,
	END
}

public class Draw : MonoBehaviour
{
    private LineRenderer currentLineRenderer; // ���� ���� ������
    [SerializeField] private Camera mainCam;


	private LineType currentType;
	private float _limitValue = 1.2f;
	private Vector2 lastPos;

	private void Start()
    {
        currentLineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
		if(Input.GetMouseButtonDown(0))
        {
			AddPoint();
        }
		else if (Input.GetMouseButton(0))
		{
			Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
			Vector2 camRelative = mainCam.transform.InverseTransformPoint(mousePos);
			if (camRelative != lastPos && camRelative != null)
			{
				AddPoint(camRelative);
				lastPos = camRelative;
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			LineCheck();
		}
	}
    private void AddPoint()
    {
        Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition); // ���콺 ������ �����ͼ�
        Vector2 camRelative = mainCam.transform.InverseTransformPoint(mousePos); // inverse

        currentLineRenderer.SetPosition(0, camRelative);
        currentLineRenderer.SetPosition(1, camRelative);
    }

    private void AddPoint(Vector2 pointPos)
    {
        currentLineRenderer.SetPosition(currentLineRenderer.positionCount++, pointPos);
    }

	private void LineCheck()
	{
		Vector3[] positions = new Vector3[currentLineRenderer.positionCount];
		currentLineRenderer.GetPositions(positions);

		currentType = LineType.NONE; // �⺻ NONE ���� ����

		if (positions.Length <= 4) return; // �׳� Ŭ�� üũ

		WidthHeightCheck(positions);
		VCheck(positions); // V �Ǻ�
		ThunderCheck(positions); // �����Ǻ�

		print(currentType);
		currentLineRenderer.positionCount = 2;
	}

	private void WidthHeightCheck(Vector3[] positions)
	{
		for (int i = 0; i < positions.Length; i++)
		{
			//����
			if (positions[i].x < currentLineRenderer.GetPosition(0).x + _limitValue &&
				positions[i].x > currentLineRenderer.GetPosition(0).x - _limitValue)
			{
				currentType = LineType.LENGTH;
			}
			//����
			else if (positions[i].y < currentLineRenderer.GetPosition(0).y + _limitValue &&
				positions[i].y > currentLineRenderer.GetPosition(0).y - _limitValue)
			{
				currentType = LineType.WIDTH;
			}
		}
	}

	private float maxDisX = 1.0f; // 231102 ���� VCheck �Լ����� ���
	private void VCheck(Vector3[] positions)
	{
		Vector3 startPos = currentLineRenderer.GetPosition(0); // 0��° ������ ��������
		Vector3 endPos = currentLineRenderer.GetPosition(positions.Length - 1); // ������ ������

		// ������ ���� for�� (W, M ����ó�� �׷� �� �����ϰ� ���� ������)
		for (int i = 0; i < positions.Length; i++)
		{
			// ������ �Ǻ�
			if (i > 1 && i < positions.Length - 3 && // ���� ����
				positions[i].y < positions[i - 2].y && positions[i].y < positions[i + 2].y) // �������ٰ� ���ڱ� �ö� (�� ���� ���� ������ ���� ���� ����)
			{
				float firstY = startPos.y; // ó������ �׷��� ���� y�� (button down pos)
				float lastY = endPos.y; // ���������� �׷��� ���� y�� (button up pos)
				float value = Mathf.Abs(startPos.x - endPos.x); // ó�� ���� ������ ���� X ����

				if (firstY - positions[i].y >= 1.0f &&
					lastY - positions[i].y >= 1.0f && // (ù ��/������ ��)�� ã�Ƴ� �������� ���̰� 1�̻��� ��� (������ ������!!! firstY�� lastY �� ���� pos���� ŭ)
					value >= maxDisX) // ù ���� �� ���� x�� ����
				{
					currentType = LineType.V;
				}
			}
			if (i > 1 && i < positions.Length - 3 && // ���� ����
				positions[i].y > positions[i - 2].y && positions[i].y > positions[i + 2].y) // �ö󰡴ٰ� ���ڱ� ���� (�� ���� ���� ������ ���� ���� ŭ)
			{
				float firstY = startPos.y; // ó������ �׷��� ���� y�� (button down pos)
				float lastY = endPos.y; // ���������� �׷��� ���� y�� (button up pos)
				float value = Mathf.Abs(startPos.x - endPos.x); // ó�� ���� ������ ���� X ����

				if (positions[i].y - firstY >= 1.0f &&
					positions[i].y - lastY >= 1.0f &&
					value >= maxDisX)
				{
					currentType = LineType.REVERSEV;
				}
			}
		}
	}

	private void ThunderCheck(Vector3[] positions) // ��..������
	{
		int cnt = 0;
		for (int i = 0; i < positions.Length; i++)
		{
			if (i > 1 && i < positions.Length - 3 && // ���� ����
			  positions[i].x < positions[i - 2].x && positions[i].x < positions[i + 2].x) // < �̷� ����϶�
			{
				cnt++;
			}
			else if (i > 1 && i < positions.Length - 3 && // ���� ����
			  positions[i].x > positions[i - 2].x && positions[i].x > positions[i + 2].x) // > �̰� �̷� ���
			{
				cnt++;
			}
		}

		if (cnt >= 2 && cnt <= 4) // < > �̷� ����� �� �� �̻�, �� �� ������ ��� ���� ����̶�� ����.
		{
			currentType = LineType.THUNDER;
		}
	}
}
