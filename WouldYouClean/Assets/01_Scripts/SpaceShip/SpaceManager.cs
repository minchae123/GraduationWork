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
    //셰이더 설정하기

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

    #region 우주선발사및착륙
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
                //행성주변 사각형으로 선택된거같은 표시  
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
            //속도초기화
            spaceship._curSpeed = 0;

            Vector3 dir = spaceship.transform.position - curPlanet.transform.position;

            foreach (PlanetInSpace planet in Planets)
                planet.SetDir(dir);

            //착륙할때 행성 바라보게
            Vector3 pos = curPlanet.transform.position;
            pos.z = 0;
            spaceship.transform.up = (pos = spaceship.transform.position) * -1;
        }
    }

    #region 함수
    void Interacting() => canInteraction = false;

    void Finish() => canInteraction = true;

    public void ChangeView()
    {
        FadePanel.DOFade(1, .75f);
        //카메라 옮기기
        FadePanel.DOFade(0, .75f);
    }
    #endregion

    private IEnumerator SpaceshipLanding()
    {
        Interacting(); //상호작용 막기

        //우주배경 설정
        _targetSize = 5;
        _targetStarSize = 5;

        //현재 상태 설정
        isFlight = false;
        isLanding = true;

        input.enabled = false;
        spaceship.enabled = false;
        yield return new WaitForSeconds(.5f);
        //행성진입

        //선택된 테두리 지우기
        curPlanet.clean = true;
        _fire.Stop();
        Finish();
    }

    private IEnumerator SpaceshipLaunch()
    {
        Interacting();
        _targetSize = 10;
        _targetStarSize = 3;
        spaceship.transform.rotation = Quaternion.Euler(0, 0, 0);

        isLanding = false;
        isFlight = true;

        spaceship.enabled = true;
        _fire.Play();
        yield return new WaitForSeconds(1f);
        input.enabled = true;
        Finish();
    }
    #endregion
}
