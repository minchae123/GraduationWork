using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
	public class PlayerColorClass
	{
		public Color RED;
		public Color GREEN;
		public Color BLUE;
		public Color YELLOW;
		public Color MAGENTA;
		public Color MINT;
		public Color WHITE;

		public Dictionary<ColorEnum, Color> ColorDictionary;

		public void SetColors()
        {
			RED = new Color(255f / 255f, 185f / 255f, 170f / 255f);
			GREEN = new Color(151f / 255f, 220f / 255f, 169f / 255f);
			BLUE = new Color(119f / 255f, 158f / 255f, 220f / 255f);

			YELLOW = new Color(246f / 255f, 234f / 255f, 182f / 255f);
			MAGENTA = new Color(203f / 255f, 170f / 255f, 203f / 255f);
			MINT = new Color(140f / 255f, 222f / 255f, 230f / 255f);

			WHITE = Color.white;

			SetDictionary();
		}
		private void SetDictionary()
		{
			ColorDictionary = new Dictionary<ColorEnum, Color>();

			ColorDictionary[ColorEnum.RED]	   =  			RED;
			ColorDictionary[ColorEnum.GREEN]   =			GREEN;
			ColorDictionary[ColorEnum.BLUE]    =			BLUE;
			ColorDictionary[ColorEnum.YELLOW]  =			YELLOW;
			ColorDictionary[ColorEnum.MAGENTA] =			MAGENTA;
			ColorDictionary[ColorEnum.MINT]	   =			MINT;
			ColorDictionary[ColorEnum.WHITE]   =			WHITE;
		}
	}

	public PlayerColorClass playerColors;

	private void Awake()
	{
		playerColors = new PlayerColorClass();
		playerColors.SetColors();
	}

	public List<Transform> FindAllItems() //FindAllItems<T>() where T : class 나중에 interface를 많이 쓸거라면 이걸로 바꿔서
	{
		List<Transform> items = new List<Transform>();

		foreach (Transform trm in FindObjectsOfType<Transform>())
		{
			if (trm.TryGetComponent(out Item item))
			{
				items.Add(trm);
			}
		}

		return items;
	}

	public Color FindColor(ColorEnum c)
    {
		if (playerColors.ColorDictionary.TryGetValue(c, out Color color)) 
        {
			return color;
        }
		return Color.black;
    }
}
