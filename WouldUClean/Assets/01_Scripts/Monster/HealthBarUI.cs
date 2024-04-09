using DG.Tweening;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]

    private Transform _barTrm;

    private int _maxHealth = 0;
    private int _health = 0;


    private void Awake()
    {
        _barTrm = transform.Find("Bar");
        _barTrm.localScale = new Vector3(0, 1, 1);

        _maxHealth = transform.parent.GetComponent<EnemyHealth>().MaxHealth;
    }

    private void Start()
    {
        SetHealth(_maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SetHealth(_maxHealth);
            print("체력을 채우기");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SetHealth(_health - 5);
        }
    }

    public void SetHealth(int health)
    {
        _health = health;
        if (_maxHealth <= 0)
        {
            _maxHealth = _health; //최초 셋팅
        }

        _health = Mathf.Clamp(_health, 0, _maxHealth);
        _barTrm.DOScaleX((float)_health / _maxHealth, 0.4f);
        if (_health <= 0)
        {
            _maxHealth = 0;
        }
    }
}