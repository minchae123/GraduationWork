using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/PlanetInfo")]
public class PlanetInfo : ScriptableObject
{
	public string planetName;
	public Sprite planetSprite;

	[TextArea]
	public string planetExplain;
}
