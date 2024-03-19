using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("Setting UI")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;

    private string bgmKey = "BGMVolume";
    private string effectKey = "EffectVolume";

    [Header("")]
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private RectTransform doorLock;
    [SerializeField] private string password;

    private string[] answer = { "Success!", "Failed" };
    private string passStr = null;
    private bool isPass = false;

    private void Start()
    {
        bgmSlider.value = PlayerPrefs.GetFloat(bgmKey);
        effectSlider.value = PlayerPrefs.GetFloat(effectKey);
    }

    // 음악 소리 변경
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

    private void Update()
    {
        if (isPass && passStr.Length >= password.Length)
        {
            if (password == passStr)
                StartCoroutine(GuessPassword(answer[0]));
            else
                StartCoroutine(GuessPassword(answer[1]));

            passStr = null;
            isPass = false;
        }
    }

    private IEnumerator GuessPassword(string result)
    {
        yield return new WaitForSeconds(0.5f);
        textBox.text = result;

        yield return new WaitForSeconds(1f);
        UndoLock(result);
    }
    
    private void UndoLock(string result)
    {
        textBox.text = null;

        if (result == answer[0])
            ClosePanel(doorLock);
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


    public void NumBtn(int num)
    {
        isPass = true;

        passStr += num.ToString();

        textBox.text = passStr;
    }
}
