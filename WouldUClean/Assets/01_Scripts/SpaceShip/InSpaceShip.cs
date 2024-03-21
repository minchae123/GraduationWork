using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Machine
{
	Oxygen,
	Shop,
	Research
}

public class InSpaceShip : MonoBehaviour
{
	[SerializeField] private Machine machine;
	[SerializeField] private GameObject interfaceKey;

	private bool isinterfaceKey;

	private ResearchTable researchTable;
	private Shop shop;
	private Oxygen oxygen;

	private void Awake()
	{
		researchTable = FindObjectOfType<ResearchTable>();
		shop = FindObjectOfType<Shop>();
		oxygen = FindObjectOfType<Oxygen>();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.F))
			isinterfaceKey = true;
		if(Input.GetKeyUp(KeyCode.F))
			isinterfaceKey = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{

	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if(collision.name == "Player")
		{
			interfaceKey.SetActive(true);
			if (isinterfaceKey)
			{
				switch (machine)
				{
					case Machine.Oxygen:
						OxygenSystem();
						break;
					case Machine.Shop:
						ShopSystem();
						break;
					case Machine.Research:
						ReserchSystem();
						break;
				}
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.name == "Player")
			interfaceKey.SetActive(false);
	}

	private void OxygenSystem()
	{
		oxygen.SetActive(true);
	}

	private void ShopSystem()
	{
		shop.EnterShop();
	}

	private void ReserchSystem()
	{
		researchTable.ActivateMainPanel();
	}
}