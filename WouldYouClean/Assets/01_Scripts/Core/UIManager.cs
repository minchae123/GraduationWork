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
    private bool isSetting = false;

    [Header("Setting UI")]
    [SerializeField] private GameObject settingPanel;

    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;

    private string bgmKey = "BGMVolume";
    private string effectKey = "EffectVolume";


    private void Start()
    {
        OnExitSettingPanel();

        bgmSlider.value = PlayerPrefs.GetFloat(bgmKey);
        effectSlider.value = PlayerPrefs.GetFloat(effectKey);
    }

    #region ����â
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
        isSetting = false;
        settingPanel.SetActive(false);

        Time.timeScale = 1;
    }
    public void OnExitAndSave()
    {
        SaveManager.Instance.SaveGameData();
        SceneManager.LoadScene("Title");
        // ���� ���̺��ϰ�
        // Ÿ��Ʋ ȭ������ �̵��ϴ� ����
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isSetting) // ????? ?? ???? ???? ??
            {
                OnEntrySettingPanel();
            }
            else // ????? ???? ??????
            {
                OnExitSettingPanel();
            }
        }

    }

    public void OnEntrySettingPanel()
    {
        isSetting = true;
        settingPanel.SetActive(true);

        Time.timeScale = 0;
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
        panel.transform.DOScale(Vector2.one * 1f, 1.5f).SetEase(Ease.InOutQuint); // ũ�⸦ 1.5��� 1�� ���� ���� Ű��
        panel.DOAnchorPos(Vector2.zero, 1.5f).SetEase(Ease.InOutQuint);
    }

    public void ClosePanel(RectTransform rect)
    {
        rect.transform.DOScale(Vector2.zero, 1f).SetEase(Ease.InOutQuint);
        rect.DOAnchorPos(Vector2.zero, 1f);
    }
}
