using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor.Rendering.LookDev;

public class SpaceManager : MonoBehaviour
{
    public Image FadePanel;

    [Header("Planet")]
    public PlanetInSpace[] Planets;
    public PlanetInSpace curPlanet = null;
    private float _shortDis;

    private Camera _cam;
    private float _targetSize = 5;
    private float _curSize;

    [Header("Background")]
    [SerializeField] private SpriteRenderer _sr;
    private float _targetStarSize = 5;
    private float _curStarSize;
    //���̴� �����ϱ�

    [Header("Spaceship")]
    public InputReader input;
    public Spaceship spaceship;
    public bool canInteraction;
    public bool canLanding;
    public bool isLanding;
    public bool isFlight;

    [SerializeField] private ParticleSystem _fire;

    private void Awake()
    {
        canInteraction = true;

        _cam = Camera.main;
        _curSize = _cam.orthographicSize;
    }

    private void Start()
    {
        Planets = GameObject.FindObjectsOfType<PlanetInSpace>();


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

        float starScale = Mathf.SmoothDamp(_sr.material.GetFloat("_OverallScale"), _targetStarSize, ref _curStarSize, 1.5f);
        _sr.material.SetFloat("_OverallScale", starScale);
    }

    #region ���ּ��߻������
    private void Interaction()
    {
        foreach (PlanetInSpace planet in Planets)
        {
            if (Vector2.Distance(spaceship.transform.position, planet.transform.position) < 7)
                curPlanet = planet;
        }

        if (curPlanet != null)
            if (Vector2.Distance(spaceship.transform.position, curPlanet.transform.position) < 7)
            {
                //�༺�ֺ� �簢������ ���õȰŰ��� ǥ��  
                curPlanet.interacted = true;
                canLanding = true;
            }
            else
            {
                curPlanet.interacted = false;
                canLanding = false;
            }

        if (canInteraction)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isLanding && !isFlight)
                {
                    StartCoroutine(SpaceshipLaunch());
                }
                else if (isFlight && canLanding && !curPlanet.clean)
                {
                    StartCoroutine(SpaceshipLanding());
                }
            }
        }


        if (isLanding && curPlanet != null)
        {
            Vector3 dir = spaceship.transform.position - curPlanet.transform.position;

            foreach (PlanetInSpace planet in Planets)
                planet.SetDir(dir);

            //�����Ҷ� �༺ �ٶ󺸰�
        }
    }

    void Interacting()
    {
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
        _targetStarSize = 5;

        isFlight = false;
        isLanding = true;

        input.enabled = false;
        spaceship.enabled = false;
        yield return new WaitForSeconds(.5f);
        FadePanel.DOFade(1, .75f);
        _fire.Stop();
        Finish();
    }

    private IEnumerator SpaceshipLaunch()
    {
        Interacting();
        FadePanel.DOFade(0, .75f);
        _targetSize = 10;
        _targetStarSize = 3;
        spaceship.transform.rotation = Quaternion.Euler(0, 0, 0);

        isLanding = false;
        isFlight = true;

        spaceship.enabled = true;
        _fire.Play();
        yield return new WaitForSeconds(1f);
        input.enabled = true;
        curPlanet.clean = true;
        Finish();
    }
    #endregion
}
