using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private int _healthAmountPerSep = 5;
    [SerializeField]
    private float _barSize = 1f;
    [SerializeField]
    private Vector2 _sepSize;

    private Transform _barTrm, _seperator, _barBackground;

    private int _maxHealth = 0;
    private int _health = 0;

    private MeshFilter _sepMeshFilter;
    private Mesh _sepMesh;
    private MeshRenderer _sepMeshRenderer;

    private void Awake()
    {
        _barTrm = transform.Find("Bar");
        _barTrm.localScale = new Vector3(0, 1, 1);

        _seperator = transform.Find("SeperatorContainer/Seperator");
        _sepMeshFilter = _seperator.GetComponent<MeshFilter>();
        _sepMeshRenderer = _seperator.GetComponent<MeshRenderer>();

        _sepMeshRenderer.sortingOrder = 20;

        _barBackground = transform.Find("BarBackground");
        _barSize = _barBackground.localScale.x; // 1�� �����´�.
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SetHealth(100);
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
            _maxHealth = _health; //���� ����
            CalcSeperator(_maxHealth);
            gameObject.SetActive(false);
        }
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        _barTrm.DOScaleX((float)_health / _maxHealth, 0.4f);
        if (_health <= 0)
        {
            _maxHealth = 0;
        }

    }

    private void CalcSeperator(int value)
    {

        _sepMesh = new Mesh();
        SpriteRenderer sr = _barBackground.GetComponent<SpriteRenderer>();
        int sepCount = Mathf.FloorToInt((float)value / _healthAmountPerSep); //���йٰ� ��� ���;� �ϴ��� �� �� �ִ�.

        float boundSize = sr.bounds.size.x; //�̰� ��׶��� ���� ����Ƽ ������
        float calcSize = (boundSize / sepCount) * 0.1f;
        _sepSize.x = Mathf.Min(calcSize, _sepSize.x); //������ �����ѰŶ� ���� ����ѰŶ� ���߿��� ������ ��

        Vector3[] vertices = new Vector3[(sepCount - 1) * 4];
        Vector2[] uv = new Vector2[(sepCount - 1) * 4];
        int[] triangles = new int[(sepCount - 1) * 6];

        float barOneGap = _barSize / value; //ù��° �ٰ� ���ö����� �Ÿ�

        for (int i = 0; i < sepCount - 1; i++)
        {
            Vector3 pos = new Vector3(barOneGap * (i + 1) * _healthAmountPerSep, 0, 0);
            int vIndex = i * 4;
            vertices[vIndex + 0] = pos + new Vector3(-_sepSize.x, -_sepSize.y);
            vertices[vIndex + 1] = pos + new Vector3(-_sepSize.x, +_sepSize.y);
            vertices[vIndex + 2] = pos + new Vector3(+_sepSize.x, +_sepSize.y);
            vertices[vIndex + 3] = pos + new Vector3(+_sepSize.x, -_sepSize.y);

            uv[vIndex + 0] = Vector2.zero;
            uv[vIndex + 1] = new Vector2(0, 1);
            uv[vIndex + 2] = Vector2.one;
            uv[vIndex + 3] = new Vector2(1, 0);

            int tIndex = i * 6;
            triangles[tIndex + 0] = vIndex + 0;
            triangles[tIndex + 1] = vIndex + 1;
            triangles[tIndex + 2] = vIndex + 2;
            triangles[tIndex + 3] = vIndex + 0;
            triangles[tIndex + 4] = vIndex + 2;
            triangles[tIndex + 5] = vIndex + 3;
        }

        _sepMesh.vertices = vertices;
        _sepMesh.uv = uv;
        _sepMesh.triangles = triangles;

        _sepMeshFilter.mesh = _sepMesh;
    }
}