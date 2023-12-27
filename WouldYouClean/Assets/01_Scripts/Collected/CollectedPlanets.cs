using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectedPlanets : MonoBehaviour
{
	[SerializeField] private List<PlanetInfo> planets;

	[SerializeField] private PlanetContent collected;
	[SerializeField] private Transform context;

	[Header("Ό³Έν UI")]
	[SerializeField] private Image infoImage;
	[SerializeField] private TextMeshProUGUI planetName;
	[SerializeField] private TextMeshProUGUI planetExplain;


	private void Awake()
	{
		
	}

    public void AddPlanetsCollected(PlanetInfo info)
	{
		planets.Add(info);
		collected.info = info;
		var obj = Instantiate(collected, context);
	}

	public void AddTrashCollected(PlanetInfo info)
	{
		planets.Add(info);
		collected.info = info;
		var obj = Instantiate(collected, context);
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
