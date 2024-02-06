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

    public Camera MainCam;
    public Camera SpaceshipCam;
    public InputReader PlayerInputReader;
    public InputReader SpaceshipInputReader;

    [Header("Planet")]
    public PlanetInSpace[] Planets;
    public PlanetInSpace curPlanet = null;

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


        if (isLanding)
        {
            Vector3 dir = spaceship.transform.position - curPlanet.transform.position;

            foreach (PlanetInSpace planet in Planets)
                planet.SetDir(dir);

            //착륙할때 행성 바라보게
            float angle = Mathf.Atan2(spaceship.transform.position.y - curPlanet.transform.position.y,
                spaceship.transform.position.x - curPlanet.transform.position.x) * Mathf.Rad2Deg;

            if (Vector2.Distance(spaceship.transform.position, curPlanet.transform.position) < .1f)
                spaceship.transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                spaceship.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            //천천히 돌아가게 바꿔야됨 어케함?
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
        FadePanel.DOFade(1, .75f).OnComplete(CameraChange).OnComplete(CameraChange);
        //CameraChange();
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
        curPlanet.clean = true; //진행도가 완료되면 클리어로 바꾸기
        Finish();
    }
    #endregion

    public void CameraChange()
    {
        MainCam.gameObject.SetActive(!MainCam.gameObject.activeSelf);
        SpaceshipCam.gameObject.SetActive(!SpaceshipCam.gameObject.activeSelf);
        PlayerInputReader.enabled = !PlayerInputReader.enabled;
        SpaceshipInputReader.enabled = !SpaceshipInputReader.enabled;
    }
}
