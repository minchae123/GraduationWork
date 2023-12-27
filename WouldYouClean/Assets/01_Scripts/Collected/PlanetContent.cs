using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PlanetContent : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private Image spriteImage;
	[SerializeField] private TextMeshProUGUI planetName;
	[SerializeField] private TextMeshProUGUI planetExplain;

	private PlanetInfo info;

	private void Awake()
	{
		
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		spriteImage.sprite = info.planetSprite;
		planetName.text = info.planetName;
		planetExplain.text = info.planetExplain;
	}
}
