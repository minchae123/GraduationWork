using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class SpaceBackground : MonoBehaviour
{
    private SpriteRenderer _sr;
    private float _brightness = .97f;
    private float _bright = .01f;

    private float _twinkle = .16f;
    private float _variation = .01f;

    private Vector2 curDir;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_brightness >= .975f)
            _bright = -.01f;
        else if (_brightness <= .965)
            _bright = .01f;

        _brightness += _bright * Time.deltaTime;
        _sr.material.SetFloat("_Threshold", _brightness);



        if (_twinkle >= .17f)
            _variation = -.005f;
        else if (_twinkle <= .15f)
            _variation = .005f;

        _twinkle += _variation * Time.deltaTime;
        _sr.material.SetFloat("_BrightnessVariationScale", _twinkle);
    }

    public void SetDir(Vector2 dir)
    {
        curDir += dir;
        _sr.material.SetVector("_Offset", curDir);
    }
}
