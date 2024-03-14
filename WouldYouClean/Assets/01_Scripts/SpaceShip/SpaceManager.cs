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

    private float _targetSize = 5;
    private float _curSize;

    [Header("Background")]
    [SerializeField] private SpriteRenderer _sr;
    private float _targetStarSize = 5;
    private float _curStarSize;
    //셰이더 설정하기

    [Header("Player")]
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _spaceshipPos;
    //[SerializeField] private Transform _inPlanetPos;
    public bool nearSpaceship;

    [Header("SpaceShip")]
    public InputReader input;
    public SpaceShip spaceship;
    public bool canInteraction;
    public bool canLanding;
    public bool isLanding;
    public bool isFlight;

    [SerializeField] private ParticleSystem _fire;

    private float _distance;
    private float _shortDis;

    private void Awake()
    {
        canInteraction = true;

        _curSize = SpaceshipCam.orthographicSize;
    }

    private void Start()
    {
        Planets = GameObject.FindObjectsOfType<PlanetInSpace>();
    }

    private void Update()
    {
        Interaction();

        VisualRange();

        //콜라이더로 하시래요
        if (Vector2.Distance(_spaceshipPos.position, _player.transform.position) < 2)
            nearSpaceship = true;
        else
            nearSpaceship = false;
    }

    private void VisualRange()
    {
        float smoothCamSize = Mathf.SmoothDamp(SpaceshipCam.orthographicSize, _targetSize, ref _curSize, 1.5f);
        SpaceshipCam.orthographicSize = smoothCamSize;

        float starScale = Mathf.SmoothDamp(_sr.material.GetFloat("_OverallScale"), _targetStarSize, ref _curStarSize, 1.5f);
        _sr.material.SetFloat("_OverallScale", starScale);
    }

    #region 우주선발사및착륙
    private void Interaction()
    {
        //거리 측정
        _shortDis = Vector2.Distance(_spaceshipPos.position, Planets[0].transform.position);

        foreach (PlanetInSpace planet in Planets)
        {
            //if (Vector2.Distance(spaceship.transform.position, planet.transform.position) < 7)

            _distance = Vector2.Distance(spaceship.transform.position, planet.transform.position);

            if (_distance < _shortDis)
            {
                curPlanet = planet;
                _shortDis = _distance;
            }
        }

        if (curPlanet != null)
        {
            if (Vector2.Distance(spaceship.transform.position, curPlanet.transform.position) < 7)
            {
                //행성주변 사각형으로 선택된거같은 표시  
                curPlanet.interacted = true;
                canLanding = true;
                curPlanet.inPlanet.SetActive(true);
            }
            else
            {
                curPlanet.interacted = false;
                canLanding = false;
                curPlanet.inPlanet.SetActive(false);
            }
        }

        //상호작용 가능상태
        if (canInteraction)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isLanding && !isFlight && nearSpaceship)
                {
                    StartCoroutine(SpaceshipLaunch());
                }
                else if (isFlight && canLanding && !curPlanet.clean)
                {
                    StartCoroutine(SpaceshipLanding());
                }
            }
        }

        //착륙 시
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

        //임시 테스트용 /입구에서 상호작용누르면 행성으로 행성내 우주선에서 누르면 우주선안으로
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    StartCoroutine(PlayerPosChange());
        //}
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

        _player.transform.position = _spaceshipPos.position;

        isFlight = false;
        isLanding = true;

        input.enabled = false;
        spaceship.enabled = false;

        //우주선 안으로
        yield return new WaitForSeconds(1f);
        StartCoroutine(CameraChange());
        _fire.Stop();
        Finish();
    }

    private IEnumerator SpaceshipLaunch()
    {
        Interacting();
        StartCoroutine(CameraChange());
        yield return new WaitForSeconds(1f);
        _targetSize = 10;
        _targetStarSize = 3;
        spaceship.transform.rotation = Quaternion.Euler(0, 0, 0);

        isLanding = false;
        isFlight = true;

        spaceship.enabled = true;
        _fire.Play();
        yield return new WaitForSeconds(1f);
        input.enabled = true;
        //curPlanet.clean = true; //진행도가 완료되면 클리어로 바꾸기
        Finish();
    }
    #endregion

    public IEnumerator CameraChange()
    {
        FadePanel.DOFade(1, .75f);
        yield return new WaitForSeconds(1f);
        MainCam.gameObject.SetActive(!MainCam.gameObject.activeSelf);
        SpaceshipCam.gameObject.SetActive(!SpaceshipCam.gameObject.activeSelf);
        PlayerInputReader.enabled = !PlayerInputReader.enabled;
        //SpaceshipInputReader.enabled = !SpaceshipInputReader.enabled;
        //yield return new WaitForSeconds(1f);
        FadePanel.DOFade(0, .75f);
    }

    //혹시 몰라 남겨놓기
    //private IEnumerator PlayerPosChange() 
    //{
    //    Interacting();
    //    nearSpaceship = !nearSpaceship;

    //    FadePanel.DOFade(1, .75f);
    //    yield return new WaitForSeconds(1f);
    //    //우주선 내부인지
    //    if (nearSpaceship)
    //    {
    //        player.transform.position = inSpaceshipPos.position;
    //    }
    //    else
    //    {
    //        player.transform.position = inPlanetPos.position;
    //    }
    //    yield return new WaitForSeconds(1f);
    //    FadePanel.DOFade(0, .75f).OnComplete(Finish);
    //}
}
