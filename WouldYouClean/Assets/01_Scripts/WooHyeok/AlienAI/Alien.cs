using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    [SerializeField] private float _waitMoveTime;
    [SerializeField] private float _runSpeed;

    public bool _isFollow = false;

    private Inventory _inven;
    [SerializeField] private Rigidbody2D _rb;

    private Vector2 _runningDir;
    private Vector2 _followDir;

    private float _saveSpeed;

    private void Awake()
    {
        _saveSpeed = _runSpeed;
        //_rb = GetComponent<Rigidbody2D>();
        _inven = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    void Start()
    {
        _runningDir = PlayerDir();
    }

    void Update()
    {
        if (_isFollow)
            _rb.velocity = PlayerDir() * _runSpeed;
        else
            _rb.velocity = _runningDir * _runSpeed;
    }

    private Vector2 PlayerDir()
    {
        return (GameObject.Find("Player").transform.position - transform.position).normalized;
    }

    public void ReChargingList()
    {
        _inven.UpdateSlotUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMoveMent>(out PlayerMoveMent player))
        {
            _inven.ClearItem();

            player.OnStillWarning();

            StartCoroutine(StopAlienMove());
        }
    }

    IEnumerator StopAlienMove()
    {
        _runSpeed = 0;

        yield return new WaitForSeconds(_waitMoveTime);

        _isFollow = false;
        _runSpeed = _saveSpeed;
    }
}
