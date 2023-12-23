using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerAnim : PlayerMain
{
    [SerializeField] private Image _breathBar;
    [SerializeField] private Image _hpBar;
    [SerializeField] private float _breath = 150f;

    public bool _istest = false;

    private float _hp;
    private bool _isch = false;

    private Sequence _hpSeq;

    private void Awake()
    {
        _hp = _breath;
        _hpSeq = DOTween.Sequence();
    }

    public override void Update()
    {
        _isPlain = _istest;
        Cleaning();
        divideHp();
    }

    private void divideHp()
    {
        if (_isPlain && _breath < 150)
            _breath += 30f * Time.deltaTime;
        else if (!_isPlain && _breath > 0)
            _breath -= 30f * Time.deltaTime;

        _breath = Mathf.Clamp(_breath, 0f, 150f);

        if (!_isch) DotBreathSlider();
        else DotHphSlider();
    }

    private void DotBreathSlider()
    {
        RectTransform rect = _breathBar.GetComponent<RectTransform>();
        _hpSeq.Append(
        rect.DOSizeDelta(new Vector2(rect.sizeDelta.x, _breath), 0.1f).OnComplete(() =>
        {
            if (_breath <= 0)
            {
                _breath = _hp;
                DotHphSlider();
            }
        }));
    }

    private void DotHphSlider()
    {
        _isch = true;

        RectTransform rect = _hpBar.GetComponent<RectTransform>();
        _hpSeq.Append(rect.DOSizeDelta(new Vector2(rect.sizeDelta.x, _breath), 0.1f).SetLoops(100, LoopType.Incremental).OnComplete(() =>
        {
            if (_breath <= 0)
            {
                _isch = false;
                _breath = _hp;
            }
        }));
    }

    private void Cleaning()
    {
        _animator.SetBool("clean", !_isPlain);
    }
}
