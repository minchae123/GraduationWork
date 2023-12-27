using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectedPlanets : MonoBehaviour
{
	[SerializeField] private List<PlanetInfo> planets;
	
	[SerializeField] private Image infoImage;
	[SerializeField] private TextMeshProUGUI planetName;
	[SerializeField] private TextMeshProUGUI planetExplain;

	[SerializeField] private PlanetContent collected;

	private void Awake()
	{
		
	}

	public void AddCollected(PlanetInfo info)
	{

	}

	public void ShowChange(PlanetInfo info)
	{
		infoImage.sprite = info.planetSprite;
		planetName.text = info.planetName;
		planetExplain.text = info.planetExplain;
	}

	public void ShowReset()
	{
		infoImage.sprite = null;
		planetName.text = "???";
		planetExplain.text = "??????????";  
	}
}
