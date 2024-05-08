using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class MoveButtonConnect : MonoBehaviour
{
	[SerializeField] private Button[] moveBtns = new Button[5];

	private List<Movement> movements = new List<Movement>();

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			SetMovements();

			ButtonConnect();
		}
	}


	public void SetMovements()
	{
		movements = new List<Movement>();

		movements.AddRange(FindObjectsOfType<Movement>());
	}

	public void ButtonConnect()
	{
		foreach (var b in moveBtns)
		{
			b.onClick.RemoveAllListeners();
			MethodInfo method = typeof(Movement).GetMethod(b.name);
			print(method);
			if (method == null)
			{
				Debug.LogError("Method not found: " + b.name);
				continue;
			}
			foreach (var m in movements)
			{
				b.onClick.AddListener(() => method.Invoke(m, null));
			}
		}
	}
}
