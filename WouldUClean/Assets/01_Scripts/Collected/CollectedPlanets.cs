using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectedPlanets : MonoSingleton<CollectedPlanets>
{
    [Header("저장")]
    [SerializeField] private List<PlanetInfo> planets;
    [SerializeField] private List<DivideObj> trashs;
    [SerializeField] private List<PlanetInfo> trashSO;

    [Header("스크롤 뷰")]
    [SerializeField] private DivideObj testTrash;
    [SerializeField] private ScrollRect scroll;
    [SerializeField] private PlanetContent collected;
    [SerializeField] private Transform planetContent;
    [SerializeField] private Transform trashContent;

    [Header("설명 UI")]
    [SerializeField] private Image infoImage;
    [SerializeField] private TextMeshProUGUI planetName;
    [SerializeField] private TextMeshProUGUI planetExplain;


    private void Awake()
    {
        scroll = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
    }

    private void Update()
    {
        #region test
        if (Input.GetKeyDown(KeyCode.Q))
            AddTrashCollected(testTrash);
        else if (Input.GetKeyDown(KeyCode.E))
            AddPlanetsCollected(collected.info);
        #endregion
    }

    public void ShowContext(bool isContent)
    {
        if (isContent)
            scroll.content = planetContent.GetComponent<RectTransform>();
        else
            scroll.content = trashContent.GetComponent<RectTransform>();
    }

    public void AddPlanetsCollected(PlanetInfo info)
    {
        planets.Add(info);
        collected.info = info;
        var obj = Instantiate(collected, planetContent);
    }

    public void AddTrashCollected(DivideObj info)
    {
        foreach(var i in trashs)
        {
            if (i.type == info.type)
                return;
        }

        trashs.Add(info);

        trashSO[trashs.Count - 1].planetName = info.type._ObjectName;
        trashSO[trashs.Count - 1].planetExplain = info.type._ObjectExplain;
        trashSO[trashs.Count - 1].planetSprite = info.type._ItemIcon;

        collected.info = trashSO[trashs.Count - 1];

        var obj = Instantiate(collected, trashContent);
    }

    public void ShowChange(PlanetInfo info)
    {
        infoImage.sprite = info.planetSprite;
        planetName.text = info.planetName;
        planetExplain.text = info.planetExplain;
    }

    public void ShowReset()
    {
        infoImage.sprite = null;
        planetName.text = "???";
        planetExplain.text = "??????????";
    }
}
