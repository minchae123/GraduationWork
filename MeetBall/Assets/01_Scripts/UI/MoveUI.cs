using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class MoveUI : MonoBehaviour
{
	[SerializeField] private Image colorImage;
	[SerializeField] private TextMeshProUGUI moveCnt;

	private Vector3 originPos;
	private Outline outline;

    public void SetUI(ColorEnum colorEnum, int cnt)
	{
		Color color = GameManager.Instance.FindColor(colorEnum);
		originPos = moveCnt.transform.localPosition;
		outline = colorImage.GetComponent<Outline>();

		colorImage.color = color;
		moveCnt.text = $"{cnt}";
		//print(cnt);
    }

	public void Select()
	{
		transform.DOScale(1.1f, 0.8f).SetUpdate(true);
		outline.enabled = true;
    }
	public void UnSelect()
    {
		transform.DOScale(1.0f, 0.4f).SetUpdate(true);
		outline.enabled = false;
    }

	public void UpdateMove(int cnt)
    {
		DOTween.Clear();

		moveCnt.rectTransform.localPosition = originPos;
		moveCnt.transform.DOShakePosition(0.5f, strength: new Vector3(5, 5, 0), vibrato: 10, randomness: 90, fadeOut: false).SetUpdate(true);
		
		moveCnt.text = $"{cnt}";
		//print(cnt);
	}
}
