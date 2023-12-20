using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlanetInSpace : SpaceObject
{
    [SerializeField] private float _radius;
    [SerializeField] private List<PlanetType> _planetType;

    [HideInInspector] public PlanetType _curType;
    public bool _isDetected;

    private CircleCollider2D _col;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.isKinematic = true;

        _col = GetComponent<CircleCollider2D>();

        transform.localScale = new Vector2(_radius * 2, _radius * 2);

        _curType = _planetType[Random.Range(0, _planetType.Count)];
    }

    private void Update()
    {
        if (_isDetected)
        {
            //TWINKLE
            Debug.Log("TWINKLE");

            if (Input.GetKeyDown(KeyCode.F))
            {
                //GO PLANET
                Debug.Log("LET'S GO");
            }
        }
    }
}
