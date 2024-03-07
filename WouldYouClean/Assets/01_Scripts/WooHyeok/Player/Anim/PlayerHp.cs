using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHp : PlayerMain
{
    [SerializeField] private Image _breathBar;
    [SerializeField] private Image _hpBar;
    [SerializeField] private float _breath = 150f;

    public bool _istest = false;

    private float _hp;
    private float _limitBreath;
    private bool _isch = false;

    private Sequence _hpSeq;

    private void Awake()
    {
        _hp = _breath;
        _limitBreath = _hp;
        _hpSeq = DOTween.Sequence();
    }

    public void Update()
    {
        _isPlain = _istest;

        divideHp();
    }

    private void divideHp()
    {
        _breath = Mathf.Clamp(_breath, 0f, _limitBreath);

        DotBreathSlider();
        DotHphSlider();
    }

    private float DecValue(float value)
    {
        if (!_isPlain && value > 0)
            value -= 20f * Time.deltaTime;

        return value;
    }

    private float IncValue(float value, float limit)
    {
        if (_isPlain && value < limit)
            value += 20f * Time.deltaTime;

        return value;
    }

    private void DotBreathSlider()
    {
        RectTransform rect = _breathBar.GetComponent<RectTransform>();
        _hpSeq.Append(
        rect.DOSizeDelta(new Vector2(rect.sizeDelta.x, _breath), 0.1f).OnComplete(() =>
        {
            _breath = DecValue(_breath);
            _breath = IncValue(_breath, _limitBreath);

            if (_breath <= 0)
            {
                DotHphSlider();
            }
        }));
    }

    private void DotHphSlider()
    {
        _isch = true;

        RectTransform rect = _hpBar.GetComponent<RectTransform>();
        _hpSeq.Append(rect.DOSizeDelta(new Vector2(rect.sizeDelta.x, _hp), 0.1f).SetLoops(1, LoopType.Incremental).OnComplete(() =>
        {
            if (_breath <= 0)
                _hp = DecValue(_hp);

            _hp = IncValue(_hp, 150);

            if (_hp <= 0)
            {
                //print("죽음");//죽음 모션이나 이것 저것 하는 곳
            }
        }));
    }

    public void OnDamage(float damage)
    {
        _hp -= damage;
    }

    public void OnlimitBreath(float value)
    {
        _limitBreath += value;
    }
}
