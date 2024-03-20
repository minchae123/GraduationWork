using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShip : UpgradeStat
{
    [Header("InputSystem")]
    [SerializeField] private InputReader _input;

    private Vector2 _spaceShipDir;
    public SpaceObject[] spaceObjects;

    [SerializeField] private SpaceBackground background;

    private float _chargingTime;

    public bool isCrash;

    private Rigidbody2D _crashUFORigid;

    public Slider fuelSlider;

    private void OnMove(Vector2 value)
    {
        _spaceShipDir = value;
    }

    private void Start()
    {
        _input.OnMovement += OnMove;
        spaceObjects = GameObject.FindObjectsOfType<PlanetInSpace>();
    }

    private void Update()
    {
        //fuelSlider.value = curfuel / maxfuel;

        Move();

        //나중에 지울거
        if (Input.GetKeyDown(KeyCode.R))
        {
            FillFuel();
        }

        if (curSpeed > 0)
        {
            curfuel -= curSpeed * Time.deltaTime;
        }

        if (curfuel >= 0)
        {
            Booster();
        }
        else
        {
            curSpeed = 0;
            Debug.Log("NO FUEL");
        }
    }

    public void FillFuel()
    {
        curfuel = maxfuel;
    }

    private void Booster()
    {
        if (Input.GetKey(KeyCode.LeftShift) && _chargingTime < 1.1f)
            _chargingTime += 1 * Time.deltaTime;
        else if (_chargingTime > 0)
            _chargingTime -= 1 * Time.deltaTime;

        if (_chargingTime > 1)
        {
            maxSpeed = 15;
            curSpeed = maxSpeed;
        }
        else if (_chargingTime < 1 && maxSpeed > 5)
        {
            if (_spaceShipDir.y < 0)
            {
                maxSpeed -= acceleration * Time.deltaTime;
            }

            maxSpeed -= 5 * Time.deltaTime;
            curSpeed = maxSpeed;
        }
    }


    public void Move()
    {
        if (curSpeed > 0)
            transform.Rotate(0, 0, -_spaceShipDir.x * Time.deltaTime * rotSpeed);

        foreach (SpaceObject spaceObject in spaceObjects)
        {
            if (isCrash)// && _crashUFORigid != null)
            {
                spaceObject.SetDir(-transform.up * 30);
            }
            else
                spaceObject.SetDir(-transform.up * Acceleration());

        }

        //우주배경
        background.SetDir(Acceleration() * Time.deltaTime * transform.up);
    }

    private float Acceleration()
    {
        LimitSpeed();

        if (_spaceShipDir.y > 0)
        {
            curSpeed += acceleration * Time.deltaTime;
        }
        if (_spaceShipDir.y < 0)
        {
            curSpeed -= acceleration * Time.deltaTime;
        }
        return Mathf.Clamp(curSpeed, 0, maxSpeed);
    }

    private void LimitSpeed()
    {
        if (curSpeed > maxSpeed) curSpeed = maxSpeed;
        if (curSpeed < 0f) curSpeed = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("UFO"))
        {
            _crashUFORigid = collision.gameObject.GetComponent<Rigidbody2D>();
            StartCoroutine(CrashCheck(collision));
            transform.DOShakePosition(0.5f);
            _crashUFORigid = null;
        }
    }

    IEnumerator CrashCheck(Collider2D collision)
    {
        isCrash = true;
        yield return new WaitForSeconds(0.2f);
        isCrash = false;

        if (collision == null)
            yield break;

        Destroy(collision.gameObject);
    }
}
