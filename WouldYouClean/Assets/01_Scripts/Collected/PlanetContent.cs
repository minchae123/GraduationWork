using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PlanetContent : MonoBehaviour, IPointerClickHandler
{
	private CollectedPlanets collectedPlanets;
	[SerializeField] private PlanetInfo info;

	private void Awake()
	{
		collectedPlanets = FindObjectOfType<CollectedPlanets>();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		collectedPlanets.ShowChange(info);
	}
}
