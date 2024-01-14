using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipLaunch1 : UpgradeStat
{
    [SerializeField] private ParticleSystem _fire;

    private SpaceObject[] spaceObj;
    private Camera _cam;

    PlanetInSpace _planet;

    public bool _onSpaceship;
    public bool _isLaunched;

    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        spaceObj = GameObject.FindObjectsOfType<SpaceObject>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_onSpaceship && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(Launch());
        }

        if (_isLaunched)
        {
            Acceleration();
            Move();
        }
    }

    void Move()
    {
        foreach (SpaceObject spaceObject in spaceObj)
        {
            spaceObject.SetDir(-transform.up * _curSpeed);
        }
    }

    private IEnumerator Launch()
    {
        _fire.Play();
        _isLaunched = true;
        yield return new WaitForSeconds(3);
        _isLaunched = false; _onSpaceship = false;
        GetComponent<Spaceship>().enabled = true;
    }

    private void Acceleration()
    {
        _cam.orthographicSize += .5f * Time.deltaTime;
        LimitSpeed();

        _curSpeed += 0.25f * Time.deltaTime;
    }

    private void LimitSpeed()
    {
        if (_curSpeed > _maxSpeed) _curSpeed = _maxSpeed;
    }
}
