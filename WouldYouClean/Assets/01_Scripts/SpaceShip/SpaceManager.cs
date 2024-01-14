using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SpaceManager : MonoBehaviour
{
    [Header("Planet")]
    public PlanetInSpace[] Planets;
    public PlanetInSpace curPlanet = null;
    private float _shortDis;

    private Camera _cam;
    private float _targetSize = 5;
    private float _curSize;

    [Header("Background")]
    [SerializeField] private SpriteRenderer _sr;
    //셰이더 설정하기

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
    }

    #region 우주선발사및착륙
    private void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.F))
            execution = true;

        foreach (PlanetInSpace planet in Planets)
        {
            if (Vector2.Distance(spaceship.transform.position, planet.transform.position) < 5)
                curPlanet = planet;
        }

        if (curPlanet != null)
            if (Vector2.Distance(spaceship.transform.position, curPlanet.transform.position) < 5)
            {
                //행성주변 사각형으로 선택된거같은 표시  

                canLanding = true;
            }
            else
                canLanding = false;

        if (canInteraction)
        {
            if (execution)
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
        else
            execution = false;


        if (isLanding && curPlanet != null)
        {
            Vector3 dir = spaceship.transform.position - curPlanet.transform.position;

            foreach (PlanetInSpace planet in Planets)
                planet.SetDir(dir);

            //착륙할때 행성 바라보게
        }
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
        spaceship.enabled = false;
        yield return new WaitForSeconds(1f);
        Finish();
    }

    private IEnumerator SpaceshipLaunch()
    {
        Interacting();
        _targetSize = 10;
        spaceship.transform.rotation = Quaternion.Euler(0, 0, 0);

        isLanding = false;
        isFlight = true;

        spaceship.enabled = true;
        yield return new WaitForSeconds(1f);
        input.enabled = true;
        curPlanet.clean = true;
        Finish();
    }
    #endregion
}
