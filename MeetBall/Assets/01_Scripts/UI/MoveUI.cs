using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveUI : MonoBehaviour
{
	[SerializeField] private Image colorImage;
	[SerializeField] private TextMeshProUGUI moveCnt;

	public void SetUI(Color color, int cnt)
    {
		colorImage.color = color;
		moveCnt.text = $"{cnt}";
    }
}
