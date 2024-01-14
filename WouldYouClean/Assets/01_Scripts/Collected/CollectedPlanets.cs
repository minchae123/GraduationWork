using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectedPlanets : MonoSingleton<CollectedPlanets>
{
	[SerializeField] private List<PlanetInfo> planets;
	[SerializeField] private List<DivideObj> trashs;

	[SerializeField] private DivideObj a;
	[SerializeField] private PlanetContent collected;
	[SerializeField] private Transform planetContext;
	[SerializeField] private Transform trashContext;

	[Header("Ό³Έν UI")]
	[SerializeField] private Image infoImage;
	[SerializeField] private TextMeshProUGUI planetName;
	[SerializeField] private TextMeshProUGUI planetExplain;


	private void Awake()
	{
	}

    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Q))
			AddTrashCollected(a);
        
    }

    public void AddPlanetsCollected(PlanetInfo info)
	{
		planets.Add(info);
		collected.info = info;
		var obj = Instantiate(collected, planetContext);
	}

	public void AddTrashCollected(DivideObj info)
	{
		trashs.Add(info);

		collected.info.planetName = info.type._ObjectName;
		collected.info.planetExplain = info.type._ObjectExplain;
		collected.info.planetSprite = info.type._ItemIcon;

		var obj = Instantiate(collected, trashContext);
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
