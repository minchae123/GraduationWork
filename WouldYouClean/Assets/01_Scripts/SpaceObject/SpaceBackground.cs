using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class SpaceBackground : MonoBehaviour
{
    private SpriteRenderer _sr;
    private float _brightness = .97f;
    private float _bright = .001f;

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
    }

    public void SetDir(Vector2 dir)
    {
        curDir += dir;
        _sr.material.SetVector("_Offset", curDir);
    }
}
