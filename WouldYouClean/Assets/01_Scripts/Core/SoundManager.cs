using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    private AudioSource audioSource;

    private string bgmKey = "BGMVolume";
    private string effectKey = "EffectVolume";

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        PlayerPrefs.SetFloat(bgmKey, 0.1f);
        PlayerPrefs.SetFloat(effectKey, 0.4f);
    }
    private void Start()
    {
        GameManager.Instance.mainCam.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat(bgmKey); // ������ bgm ���� �Ҹ�
        GameManager.Instance.mainCam.GetComponent<AudioSource>().loop = true;
        audioSource.volume = PlayerPrefs.GetFloat(effectKey); // ������ effect ���� �Ҹ�
    }

    public void SetBGMVolume(float value)
    {
        GameManager.Instance.mainCam.GetComponent<AudioSource>().volume = value;
        PlayerPrefs.SetFloat(bgmKey, value);
    }
    public void SetEffectVolume(float value)
    {
        audioSource.volume = value;
        PlayerPrefs.SetFloat(effectKey, value);
    }
}
