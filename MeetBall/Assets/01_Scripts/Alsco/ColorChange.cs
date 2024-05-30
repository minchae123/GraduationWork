using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour, Item
{
	[SerializeField] private OriginColorEnum colorToChange;
	[Header("=======================")]

	[SerializeField] private GameObject[] visuals;

    public void Rotation(bool value)
    {
		Vector3 rot = value ? new Vector3(0f, 0f, 180f) : Vector3.zero;
		transform.rotation = Quaternion.Euler(rot);
    }

    private void Awake()
	{
		visuals[(int)colorToChange - 1].SetActive(true);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<Movement>(out Movement m))
		{
			OriginColorEnum basicColor = m.PlayerColor;
			foreach(MoveUI ui in PlayerManager.Instance.MoveUIList)
            {
				if(ui.color == basicColor)
                {
					ui.SetUI(colorToChange);
                }
            }

			m.SetPlayerColor(colorToChange);
			Destroy(gameObject);
		}
	}
}
