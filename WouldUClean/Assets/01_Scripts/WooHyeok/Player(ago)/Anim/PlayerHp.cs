using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private Slider _breathBar;
    [SerializeField] private Slider _hpBar;

    [SerializeField] private float _limitHp;
    [SerializeField] private float _limitBreath;
    [SerializeField] private float _value = 5f;

    private bool _isPlain = false;

    private float _breath;
    private float _hp;

    private void Awake()
    {
        _hp = _limitHp;
        _breath = _limitBreath;

        _breathBar.maxValue = _limitBreath;
        _hpBar.maxValue = _limitHp;
    }

    public void Update()
    {
        //¿©±â 
        divideHp();
        UpdateSlider();
    }

    private void UpdateSlider()
    {
        if (_isPlain || SpaceManager.Instance.isSpace)
        {
            _breath = IncValue(_breath, _limitBreath);
            _hp = IncValue(_hp, _limitHp);
        }
        else
        {
            _breath = DecValue(_breath);

            if(_breath <= 0)
            _hp = DecValue(_hp);
        }

        _hpBar.value = _hp;
        _breathBar.value = _breath;
    }

    private void divideHp()
    {
        _breath = Mathf.Clamp(_breath, 0f, _limitBreath);
        _hp = Mathf.Clamp(_hp, 0f, _limitBreath);

    }

    private float IncValue(float value, float limit)
    {
        if (_isPlain && value < limit)
            value += _value * Time.deltaTime;

        return value;
    }

    private float DecValue(float value)
    {
        if (!_isPlain && value > 0)
            value -= _value * Time.deltaTime;

        return value;
    }

    private void Die()
    {
        //if(_hp <= 0)
            //Á×À½
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
