using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class SettingUI : MonoBehaviour
{
    [Header("Transform")]
    [SerializeField] private Transform settingTrm;
    [SerializeField] private Transform buttomTrm;

    [Header("BGM Button")]
    [SerializeField] private List<Sprite> bgmSprite;

    private bool isClickBGM = false;
    private Image bgmImage;

    [Header("SFX Button")]
    [SerializeField] private List<Sprite> sfxSprite;

    private bool isClickSFX = false;
    private Image sfxImage;

    private void Start()
    {
        bgmImage = buttomTrm.Find("BGM").GetComponent<Image>();
        sfxImage = buttomTrm.Find("SFX").GetComponent<Image>();

        settingTrm.localScale = Vector3.one;
        buttomTrm.localScale = new Vector3(1, 0, 1);
    }

    public void OnClickSettingButton() // 세팅 들어가기
    {
        settingTrm.DOScaleY(0, 0.08f).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            buttomTrm.DOScaleY(1, 0.3f).SetEase(Ease.OutExpo);
        });
    }
    public void OnClickBGMButton() // BGM On & Off
    {
        isClickBGM = !isClickBGM;
        bgmImage.sprite = bgmSprite[isClickBGM.GetHashCode()];

        if (isClickBGM) // 음소거 되었을 때
        {

        }
        else
        {

        }
    }
    public void OnClickSFXButton() // SFX On & Off
    {
        isClickSFX = !isClickSFX;
        sfxImage.sprite = sfxSprite[isClickSFX.GetHashCode()];

        if (isClickSFX) // 음소거 되었을 때
        {

        }
        else
        {

        }
    }
    public void OnClickRestartButton() // 게임 재시작
    {

    }
    public void OnClickHomeButton() // 처음 화면으로
    {

    }
    public void OnClickExitButton() // 세팅 나오기
    {
        buttomTrm.DOScaleY(0, 0.08f).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            settingTrm.DOScaleY(1, 0.3f).SetEase(Ease.OutExpo);
        });
    }
}
