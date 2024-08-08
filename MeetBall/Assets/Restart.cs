using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
	private void OnMouseDown()
	{
		StageManager.Instance.ReStartBtn();
	}
}
