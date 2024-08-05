using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public GameData gameData;

    public class PlayerColorClass
    {
        public Color RED;
        public Color GREEN;
        public Color BLUE;
        public Color YELLOW;
        public Color MAGENTA;
        public Color MINT;
        public Color WHITE;

        public Dictionary<OriginColorEnum, Color> OriginColorDictionary;
        public Dictionary<TargetColorEnum, Color> ToMakeColorDictionary;

        public void SetColors()
        {
            RED = new Color(255f / 255f, 40f / 255f, 42f / 255f);
            GREEN = new Color(19f / 255f, 202f / 255f, 0f / 255f);
            BLUE = new Color(11f / 255f, 63f / 255f, 255f / 255f);

            YELLOW = new Color(246f / 255f, 234f / 255f, 182f / 255f);
            MAGENTA = new Color(203f / 255f, 170f / 255f, 203f / 255f);
            MINT = new Color(140f / 255f, 222f / 255f, 230f / 255f);

            WHITE = Color.white;

            SetDictionary();
        }
        private void SetDictionary()
        {
            OriginColorDictionary = new Dictionary<OriginColorEnum, Color>();

            OriginColorDictionary[OriginColorEnum.RED] = RED;
            OriginColorDictionary[OriginColorEnum.GREEN] = GREEN;
            OriginColorDictionary[OriginColorEnum.BLUE] = BLUE;


            ToMakeColorDictionary = new Dictionary<TargetColorEnum, Color>();

            ToMakeColorDictionary[TargetColorEnum.YELLOW] = YELLOW;
            ToMakeColorDictionary[TargetColorEnum.MAGENTA] = MAGENTA;
            ToMakeColorDictionary[TargetColorEnum.MINT] = MINT;
            ToMakeColorDictionary[TargetColorEnum.WHITE] = WHITE;
        }
    }

    public string stage { get; set; }
    public PlayerColorClass playerColors;

    public GameObject Game, Book;
    private void Awake()
    {
        playerColors = new PlayerColorClass();
        playerColors.SetColors();
        Game = GameObject.Find("Game");
        Book = GameObject.Find("Book");
    }

    private void Start()
    {
        OpenBook();
        gameData = SaveManager.Instance.Load();

        if (gameData == null)
        {
            gameData = new GameData();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            PrintData();

        if (Input.GetKeyDown(KeyCode.V))
            SaveData();
    }

    public List<Item> FindAllItems() //FindAllItems<T>() where T : class 나중에 interface를 많이 쓸거라면 이걸로 바꿔서
    {
        List<Item> items = new List<Item>();

        foreach (Transform trm in FindObjectsOfType<Transform>())
        {
            if (trm.TryGetComponent(out Item item))
            {
                items.Add(item);
            }
        }

        return items;
    }

    public Color FindColor(OriginColorEnum c)
    {
        if (playerColors.OriginColorDictionary.TryGetValue(c, out Color color))
        {
            return color;
        }
        return Color.black;
    }
    public Color FindColor(TargetColorEnum c)
    {
        if (playerColors.ToMakeColorDictionary.TryGetValue(c, out Color color))
        {
            return color;
        }
        return Color.black;
    }

    public bool MergeColor(OriginColorEnum c1, OriginColorEnum c2)
    {
        TargetColorEnum final;
        final = StageManager.Instance.CurrentStageSO.targetColor;

        if ((int)c1 + (int)c2 == (int)final)
        {
            return true;
        }
        return false;
    }

    public void OpenBook()
    {
        Game.SetActive(false);
        Book.SetActive(true);
    }

    public void StartGame(int stageNum)
    {
        print("asd");
        Game.SetActive(true);
        //StageManager.Instance.Init();
        StageManager.Instance.SetStageNumber(stageNum);
        Book.SetActive(false);
    }

    public void SaveData()
    {
        SaveManager.Instance.Save(gameData);
    }

    public void PrintData()
    {
        print(gameData.bigStage["Snow"]);
    }
}
