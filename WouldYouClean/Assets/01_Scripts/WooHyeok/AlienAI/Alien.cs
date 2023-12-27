using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    [SerializeField] private float _waitMoveTime;
    [SerializeField] private float _runSpeed;

    private Inventory _inven;
  
    private Rigidbody2D _rb;
    private List<InventoryItem> _saveMainInventory;
    private Dictionary<ObjectType, InventoryItem> _saveInvenDictionary;
    
    private Vector2 _runningDir;
    private float _saveSpeed;

    private void Awake()
    {
        _saveSpeed = _runSpeed;
        _rb = GetComponent<Rigidbody2D>();
        _inven = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    void Start()
    {
        _runningDir = (GameObject.Find("Player").transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = _runningDir * _runSpeed;
    }

    public void ReChargingList()
    {
        _inven.UpdateSlotUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerMoveMent>(out PlayerMoveMent player))
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

        _runSpeed = _saveSpeed;
    }
}
