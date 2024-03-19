using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("Setting UI")]
    [SerializeField] private GameObject settingPanel;

    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;

    private string bgmKey = "BGMVolume";
    private string effectKey = "EffectVolume";


    private void Start()
    {
        settingPanel.SetActive(false);

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
        settingPanel.SetActive(false);
    }
    public void OnExitAndSave()
    {
        // 게임 세이브하고
        // 타이틀 화면으로 이동하는 로직
    }
    #endregion

    private void Update()
    {

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
        panel.transform.DOScale(Vector2.one * 1f, 1.5f).SetEase(Ease.InOutQuint); // 크기를 1.5배로 1초 동안 점차 키움
        panel.DOAnchorPos(Vector2.zero, 1.5f).SetEase(Ease.InOutQuint);
    }

    public void ClosePanel(RectTransform rect)
    {
        rect.transform.DOScale(Vector2.zero, 1f).SetEase(Ease.InOutQuint);
        rect.DOAnchorPos(Vector2.zero, 1f);
    }
}
