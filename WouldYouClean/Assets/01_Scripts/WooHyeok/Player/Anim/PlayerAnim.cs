using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerAnim : PlayerMain
{
    [SerializeField] private Image _hpBar;
    private float hp = 150f;
    public override void Update()
    {
        print(hp);
        if (_isPlain && hp < 150)
            hp += 1f * Time.deltaTime;
        else if(!_isPlain && hp > 0)
            hp -= 1f * Time.deltaTime;

        RectTransform rect = _hpBar.GetComponent<RectTransform>();
        rect.DOSizeDelta(new Vector2(rect.sizeDelta.x, hp), 1);

        Cleaning();
    }

    private void Cleaning()
    {
            _animator.SetBool("clean", _isPlain);
    }
}
