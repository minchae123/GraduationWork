using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
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

			ColorDictionary[ColorEnum.RED] =				RED;
			ColorDictionary[ColorEnum.GREEN] =			GREEN;
			ColorDictionary[ColorEnum.BLUE] =				BLUE;
			ColorDictionary[ColorEnum.YELLOW] =		YELLOW;
			ColorDictionary[ColorEnum.MAGENTA] =		MAGENTA;
			ColorDictionary[ColorEnum.MINT] =				MINT;
			ColorDictionary[ColorEnum.WHITE] =			WHITE;
		}
	}

	public static GameManager Instance;
	public PlayerColorClass playerColors;

	public int curStage;

	private void Awake()
	{
		if (Instance != null) Debug.LogError("CHECKGAMEMANAGER");

		Instance = this;

		playerColors = new PlayerColorClass();
		playerColors.SetColors();
	}

	public Color FindColor(ColorEnum c)
    {
		if (playerColors.ColorDictionary.TryGetValue(c, out Color color)) 
        {
			return color;
        }
		return Color.black;
    }

    public void StageUp()
	{
		curStage++;
	}
}
