using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTransition : MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Transition();
        }
    }

    public void Transition()
    {
        StartCoroutine(StartTransition());
    }

    private IEnumerator StartTransition()
    {
        _image.material.SetFloat("_On", 0);
        for (float i = 0; i <= 3; i += .1f)
        {
            _image.material.SetFloat("_Scroll", i);
            yield return new WaitForSeconds(.025f);
        }

        yield return new WaitForSeconds(1);

        _image.material.SetFloat("_On", 1);
        for (float i = 3; i >= 0; i -= .1f)
        {
            _image.material.SetFloat("_Scroll", i);
            yield return new WaitForSeconds(.025f);
        }
    }
}
