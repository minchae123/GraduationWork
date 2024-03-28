using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MapInfoUI : MonoSingleton<MapInfoUI>
{
    public bool IsInMap;

    [SerializeField] private GameObject map;
    private CanvasGroup mapGrounp;

    [Header("======Selected======")]
    [SerializeField] private MapInfoSO selectedMap;

    [Header("Selected UI")]
    [SerializeField] private Transform selectedUIParent;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image planetImage;
    [SerializeField] private Image mapDifficulty;
    [SerializeField] private Image trashValue;

    private Vector3 resetVec = new Vector3(0, 1, 1);

    private void Awake()
    {
        mapGrounp = map.GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        map.SetActive(false);
        mapGrounp.alpha = 0;
        IsInMap = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) // 디버깅
        {
            if (IsInMap) OffMapInfo();
            else OnMapInfo();
        }
    }

    public void OnMapInfo()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        map.SetActive(true);

        // 리셋
        nameText.text = "행성을 선택하세요";
        mapGrounp.DOFade(1, 0.5f).SetUpdate(true).OnComplete(() => IsInMap = true);
    }
    public void OffMapInfo()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        planetImage.DOFade(0, 0.3f);

        mapGrounp.DOFade(0, 0.5f).SetUpdate(true).OnComplete(() => { map.SetActive(false); IsInMap = false; });
    }

    public void SetSelectedMap(MapInfoSO mapInfo)
    {
        selectedMap = mapInfo;

        // 초기화
        mapDifficulty.transform.DOScale(resetVec,0.3f);
        trashValue.transform.DOScale(resetVec,0.3f);
        planetImage.DOFade(0, 0.1f).OnComplete(()=>planetImage.DOFade(1,0.2f).SetUpdate(true)).SetUpdate(true);

        selectedUIParent.transform.DOLocalRotate(new Vector3(0, 360, 0), 0.3f, RotateMode.LocalAxisAdd).OnComplete(()=>
        {
            mapDifficulty.transform.DOScaleX(0.2f * selectedMap.mapDifficulty, 0.3f)
            .OnComplete(() => trashValue.transform.DOScaleX(0.2f * selectedMap.trashValue, 0.4f).SetUpdate(true)).SetUpdate(true);
        }).SetUpdate(true);

        nameText.text = selectedMap.name;
        planetImage.sprite = selectedMap.mapSprite;
    }
}
