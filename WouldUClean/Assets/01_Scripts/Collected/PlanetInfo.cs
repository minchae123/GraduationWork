using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/PlanetInfo")]
public class PlanetInfo : ScriptableObject
{
	public string planetName;
	public Sprite planetSprite;
	public Mesh planetMesh;

	[TextArea]
	public string planetExplain;
}
