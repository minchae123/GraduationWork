using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResoultionFixed : MonoBehaviour
{
	public int width = 1920;
	public int height = 1080;

	private void Awake()
	{
		SetResolution(); 
	}

	public void SetResolution()
	{
		Camera camera = GetComponent<Camera>();
		Rect rect = camera.rect;
		float scaleheight = ((float)Screen.width / Screen.height) / ((float)16 / 9);
		float scalewidth = 1f / scaleheight;
		if (scaleheight < 1)
		{
			rect.height = scaleheight;
			rect.y = (1f - scaleheight) / 2f;
		}
		else
		{
			rect.width = scalewidth;
			rect.x = (1f - scalewidth) / 2f;
		}
		camera.rect = rect;
	}
}
