using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SpaceManager : MonoBehaviour
{
    public static SpaceManager Instance;

    private Camera _cam;
    private float _targetSize = 5;
    private float _curSize;

    [Header("Spaceship")]
    public InputReader input;
    public Spaceship spaceship;
    public bool canInteraction;
    public bool execution;
    public bool canLanding;
    public bool isLanding;
    public bool isFlight;

    private void Awake()
    {
        canInteraction = true;
        _cam = Camera.main;
        _curSize = _cam.orthographicSize;
    }

    private void Update()
    {
        Interaction();



        VisualRange();
    }

    private void VisualRange()
    {
        float smoothCamSize = Mathf.SmoothDamp(_cam.orthographicSize, _targetSize, ref _curSize, 1.5f);

        _cam.orthographicSize = smoothCamSize;
    }

    #region 우주선발사및착륙
    private void Interaction()
    {
        if (Vector2.Distance(spaceship.transform.position,/*행성 제일 가까운거 받아오기*/transform.position) < 5)
        {
            //행성주변 사각형으로 선택된거같은 표시

            if (Input.GetKeyDown(KeyCode.F))
                canLanding = true;
        }

        if (canInteraction)
        {
            if (execution)
            {
                if (isLanding && !isFlight)
                {
                    StartCoroutine(SpaceshipLaunch());
                }
                else if (isFlight && canLanding)
                {
                    StartCoroutine(SpaceshipLanding());
                }
            }
        }
        else
            execution = false;
    }

    void Interacting()
    {
        execution = false;
        canInteraction = false;
    }


    void Finish()
    {
        canInteraction = true;
    }

    private IEnumerator SpaceshipLanding()
    {
        Interacting();
        _targetSize = 5;

        isFlight = false;
        canLanding = false;
        isLanding = true;

        input.enabled = false;
        StartCoroutine(Landing());
        yield return new WaitForSeconds(1f);
        spaceship.enabled = false;
        Finish();
    }

    private IEnumerator Landing()
    {

        yield return new WaitForSeconds(1f);
    }

    private IEnumerator SpaceshipLaunch()
    {
        Interacting();
        _targetSize = 10;

        isLanding = false;
        isFlight = true;

        spaceship.enabled = true;
        yield return new WaitForSeconds(1f);
        input.enabled = true;
        Finish();
    }
    #endregion
}
