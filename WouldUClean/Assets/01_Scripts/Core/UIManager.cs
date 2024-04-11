using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("Setting UI")]
    [HideInInspector] public bool IsInSetting = false;

    [Header("Setting UI")]
    [SerializeField] private GameObject settingPanel;

    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;

    private string bgmKey = "BGMVolume";
    private string effectKey = "EffectVolume";

    public float Sensitivity { get; private set; }


    private void Start()
    {
        OnExitSettingPanel();
        settingPanel.transform.localScale = new Vector3(1, 0, 1);

        Sensitivity = 1f;

        bgmSlider.value = PlayerPrefs.GetFloat(bgmKey);
        effectSlider.value = PlayerPrefs.GetFloat(effectKey);
    }

    #region 설정창
    public void BgmSliderValueChanged()
    {
        float bgmVolume = bgmSlider.value;
        SoundManager.Instance.SetBGMVolume(bgmVolume);
    }
    public void EffectSliderValueChanged()
    {
        float effectVolume = effectSlider.value;
        SoundManager.Instance.SetEffectVolume(effectVolume);
    }
    public void OnExitSettingPanel()
    {
        Cursor.lockState = CursorLockMode.Locked;

        IsInSetting = false;
        settingPanel.transform.DOScaleY(0, 0.5f).SetEase(Ease.InOutExpo).SetUpdate(true).OnComplete(() => settingPanel.SetActive(false));

        Time.timeScale = 1;
    }
    public void OnEntrySettingPanel()
    {
        Cursor.lockState = CursorLockMode.None;

        IsInSetting = true;
        settingPanel.transform.DOScaleY(1, 0.3f).SetEase(Ease.InOutQuart).SetUpdate(true);
        settingPanel.SetActive(true);

        Time.timeScale = 0;
    }
    public void OnExitAndSave()
    {
        SaveManager.Instance.SaveGameData();
        SceneManager.LoadScene("Title");
        // 게임 세이브하고
        // 타이틀 화면으로 이동하는 로직
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MapInfoUI.Instance.IsInMap)
            {
                MapInfoUI.Instance.OffMapInfo(); return;
            }
            else if (Shop.Instance.IsInShop)
            {
                Shop.Instance.ExitShop(); return;
            }
            if (!IsInSetting) // ????? ?? ???? ???? ??
            {
                OnEntrySettingPanel();
            }
            else // ????? ???? ??????
            {
                OnExitSettingPanel();
            }
        }

    }

    public void ScaleRectTransform(RectTransform obj, Vector3 endValue, float duraion, Ease ease = Ease.Linear, params Action[] action)
    {
        obj.DOScale(endValue, duraion).SetEase(ease).OnComplete(() =>
        {
            for (int i = 0; i < action.Length; i++)
            {
                action[i]?.Invoke();
            }
        });
    }

    public void ShowPanel(RectTransform panel)
    {
        panel.transform.DOScale(Vector2.one * 1f, 1.5f).SetEase(Ease.Linear); // 크기를 1.5배로 1초 동안 점차 키움
        panel.DOAnchorPos(Vector2.zero, 1.5f).SetEase(Ease.InOutQuint);
    }

    public void ClosePanel(RectTransform rect)
    {
        rect.transform.DOScale(Vector2.zero, 1f).SetEase(Ease.InOutQuint);
        rect.DOAnchorPos(Vector2.zero, 1f);
    }

    public void SliderValue(float value)
    {
        Sensitivity = value;
    }
}
