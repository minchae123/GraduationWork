using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceBackground : MonoBehaviour
{
    private SpriteRenderer _sr;
    private float _brightness;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    //private void Start()
    //{
    //    StartCoroutine(Twinkle());
    //}

    private void Update()
    {
        _brightness = Mathf.Clamp(_brightness, 0.965f, 0.975f);
        _brightness = Mathf.PingPong(Time.time, 1f);
        _sr.material.SetFloat("_Threshold", _brightness);
    }

    //private IEnumerator Twinkle()
    //{
    //    _sr.material.SetFloat("_Threshold", .965f);
    //    yield return new WaitForSeconds(1);
    //    _sr.material.SetFloat("_Threshold", .975f);
    //    yield return new WaitForSeconds(1);
    //    StartCoroutine(Twinkle());
    //}
}
