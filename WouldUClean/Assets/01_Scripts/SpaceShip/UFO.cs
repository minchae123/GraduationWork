using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    [Header("InputSystem")]
    [SerializeField] private InputReader _input;

    private Vector2 _spaceShipDir;
    public SpaceObject[] spaceObjects;

    [SerializeField] private SpaceBackground background;

    private void OnMove(Vector2 value)
    {
        _spaceShipDir = value;
    }

    void Start()
    {
        _input.OnMovement += OnMove;
        spaceObjects = GameObject.FindObjectsOfType<SpaceObject>();
    }

    void Update()
    {
        Move();
    }
    public void Move()
    {
        foreach (SpaceObject spaceObject in spaceObjects)
        {
            spaceObject.SetDir(-_spaceShipDir * 4);
        }

        background.SetDir(_spaceShipDir * Time.deltaTime);
    }
}
