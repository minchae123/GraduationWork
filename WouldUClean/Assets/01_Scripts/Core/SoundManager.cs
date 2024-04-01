using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    private AudioSource audioSource;
	[SerializeField] private AudioSource bgmAudioSource;

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
        bgmAudioSource.volume = PlayerPrefs.GetFloat(bgmKey); // 설정된 bgm 음악 소리
        bgmAudioSource.loop = true;
        audioSource.volume = PlayerPrefs.GetFloat(effectKey); // 설정된 effect 음악 소리
    }

    public void SetBGMVolume(float value)
    {
        bgmAudioSource.volume = value;
        PlayerPrefs.SetFloat(bgmKey, value);
    }
    public void SetEffectVolume(float value)
    {
        audioSource.volume = value;
        PlayerPrefs.SetFloat(effectKey, value);
    }
}
