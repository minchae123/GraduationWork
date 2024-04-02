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

    [SerializeField] private Material _hurtShader;
    private bool _delayDmg = false;

    public bool _isPlain { get; set; }

    private float _breath;
    private float _hp;

    private void Awake()
    {
        _isPlain = false;
        _hp = _limitHp;
        _breath = _limitBreath;

        _breathBar.maxValue = _limitBreath;
        _hpBar.maxValue = _limitHp;
    }

    public void Update()
    {
        //여기 
        divideHp();
        ResetValue();
        UpdateSlider();
    }

    private void ResetValue()
    {
        if (SpaceManager.Instance.isSpace)
            FullValue();
    }

    private void UpdateSlider()
    {
        if (_isPlain || SpaceManager.Instance.nearSpaceship)
        {
            _breath = IncValue(_breath, _limitBreath);
            _hp = IncValue(_hp, _limitHp);
        }
        else
        {
            _breath = DecValue(_breath);

            if (_breath <= 0)
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
        if (value < limit)
            value += _value * Time.deltaTime;

        return value;
    }

    private float DecValue(float value)
    {
        if (value > 0)
            value -= _value * Time.deltaTime;

        return value;
    }

    private void Die()
    {
        //if(_hp <= 0)
        //죽음
    }

    public void OnDamage(float damage)
    {
        _hp -= damage;
        //딜레이넣어야함
        if (!_delayDmg)
		{
            StartCoroutine(Hurt());
            print("아야");
		}
    }

    private IEnumerator Hurt()
    {
        _delayDmg = true;
        _hurtShader.SetFloat("_ScreenIntensity", .5f);
        yield return new WaitForSeconds(.25f);
        _hurtShader.SetFloat("_ScreenIntensity", .0f);
        yield return new WaitForSeconds(.25f);
        _delayDmg = false;
    }

    public void OnlimitHp(float value)
    {
        _limitHp += value;
    }

    public void OnlimitBreath(float value)
    {
        _limitBreath += value;
    }

    public void FullValue()
    {
        _hp = _limitHp;
        _breath = _limitBreath;
    }
}
