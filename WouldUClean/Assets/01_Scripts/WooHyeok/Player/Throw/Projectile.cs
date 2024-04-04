using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private DivideObj _divideObj;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private LineRenderer _line;
    [SerializeField] private Camera _cam;
    [SerializeField] private Material _outLine;
    [SerializeField] private Material _originLine;

    [Header("수치")]
    [SerializeField] private float _limitDistamce;
    [SerializeField] private float _angle;
    [SerializeField] private float _step;

    private Inventory _inven;

    private Ray _ray;
    private RaycastHit _hit;
    private RaycastHit _enemyHit;
    private Transform _enemy;

    private string _hitTag;
    private bool _isStart;

    private void Start()
    {
        _inven = Inventory.Instance;
    }

    private void Update()
    {
        _ray = _cam.ScreenPointToRay(Input.mousePosition);

        StartRay();
        OutLineRay();
    }

    private void ResetLine()
    {
        if (_line.positionCount != 0)
            _line.positionCount = 0;
    }

    #region 시작
    private bool IsStart()
    {
        Vector2 wheel = Input.mouseScrollDelta;

        if (wheel.y != 0)
            _isStart = false;

        if (Input.GetMouseButtonDown(2))
            _isStart = !_isStart;

        if (_isStart)
            return _isStart;

        return
            _inven.currentSelectedItem?.itemData == null ||
            !_inven.invenDictionary.ContainsKey(_inven.currentSelectedItem.itemData);
    }

    private void StartRay()
    {
        if (IsStart())
        {
            ResetLine();
            return;
        } // 선택된 쓰레기가 없을 때

        ParabolaRay();
    }

    private void DrawParabola(Vector3 hitPos)
    {
        Vector3 direction = hitPos - _firePoint.position;
        Vector3 groundDirection = new Vector3(direction.x, 0, direction.z);
        Vector3 targetPos = new Vector3(groundDirection.magnitude, direction.y, 0);

        float height = targetPos.y + targetPos.magnitude / 3f;
        float angle;
        float v0;
        float time;

        height = Mathf.Max(0.01f, height);

        CalculatePathWithHeight(targetPos, height, out v0, out angle, out time);
        DrawPath(groundDirection.normalized, v0, angle, time, _step);
        FireTrash(groundDirection, v0, angle, time);
    }

    private void FireTrash(Vector3 direction, float v0, float angle, float time)
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DivideObj obj = Instantiate(_divideObj);
            obj.SetItemData(_inven.currentSelectedItem.itemData);
            _inven.RemoveItem(_inven.currentSelectedItem.itemData);

            StopAllCoroutines();
            StartCoroutine(Coroutine_Movement(obj, direction.normalized, v0, angle, time));
        }
    }
    #endregion

    #region Ray
    private void ParabolaRay()
    {
        if (Physics.Raycast(_ray, out _hit, 100))
            DrawParabola(_hit.point);
    }

    private void OutLineRay()
    {
        if (Physics.Raycast(_ray, out _enemyHit, _limitDistamce, LayerMask.GetMask("Trash") | LayerMask.GetMask("Enemy")))
        {
            if(_enemy != null && _enemy != _enemyHit.transform)
                SwitchTag(_originLine);

            _enemy = _enemyHit.transform;
            _hitTag = _enemyHit.transform.tag;

            SwitchTag(_outLine);
        }
        else
        {
            if (_enemy != null)
                SwitchTag(_originLine);
        }
    }

    private void SwitchTag(Material mat)
    {
        switch (_hitTag)
        {
            case "Trash":
                TrashTag(mat);
                break;
            case "Enemy":
                mat.color = Color.red;
                EnemyTag(mat);
                break;
        }
    }
    #endregion

    #region 아웃라인
    private void TrashTag(Material mat)
    {
        _enemy.GetComponentInChildren<MeshRenderer>().material = mat;
    }

    private void EnemyTag(Material mat)
    {
        _enemy.GetComponentInChildren<SkinnedMeshRenderer>().material = mat;
    }
    #endregion

    #region 계산
    private float QuadraticEquation(float a, float b, float c, float sign)
    {
        return (-b + sign * Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
    }

    private void CalculatePathWithHeight(Vector3 targetPos, float h, out float v0, out float angle, out float time)
    {
        float xt = targetPos.x;
        float yt = targetPos.y;
        float g = -Physics.gravity.y;

        float b = Mathf.Sqrt(2 * g * h);
        float a = (-0.5f * g);
        float c = -yt;

        float tplus = QuadraticEquation(a, b, c, 1);
        float tmin = QuadraticEquation(a, b, c, -1);
        time = tplus > tmin ? tplus : tmin;

        angle = Mathf.Atan(b * time / xt);

        v0 = b / Mathf.Sin(angle);
        if (v0 > _limitDistamce)
            v0 = _limitDistamce;
    }

    private void DrawPath(Vector3 direction, float v0, float angle, float time, float step)
    {
        step = Mathf.Max(0.01f, step);
        _line.positionCount = (int)(time / step) + 2;
        int count = 0;
        for (float i = 0; i < time; i += step)
        {
            float x = v0 * i * Mathf.Cos(angle);
            float y = v0 * i * Mathf.Sin(angle) - 0.5f * -Physics.gravity.y * Mathf.Pow(i, 2);
            _line.SetPosition(count, _firePoint.position + direction * x + Vector3.up * y);
            count++;
        }

        float xfinal = v0 * time * Mathf.Cos(angle);
        float yfinal = v0 * time * Mathf.Sin(angle) - 0.5f * -Physics.gravity.y * Mathf.Pow(time, 2);

        _line.SetPosition(count, _firePoint.position + direction * xfinal + Vector3.up * yfinal);
    }

    private void CalculatePath(Vector3 targetPos, float angle, out float v0, out float time)
    {
        float xt = targetPos.x;
        float yt = targetPos.y;
        float g = -Physics.gravity.y;

        float v1 = Mathf.Pow(xt, 2) * g;
        float v2 = 2 * xt * Mathf.Sin(angle) * Mathf.Cos(angle);
        float v3 = 2 * yt * Mathf.Pow(Mathf.Cos(angle), 2);
        v0 = Mathf.Sqrt(v1 / (v2 - v3));

        time = xt / (v0 * Mathf.Cos(angle));
    }

    IEnumerator Coroutine_Movement(DivideObj obj, Vector3 direction, float v0, float angle, float time)
    {
        float t = 0;
        while (t < time)
        {
            float x = v0 * t * Mathf.Cos(angle);
            float y = v0 * t * Mathf.Sin(angle) - (1f / 2f) * -Physics.gravity.y * Mathf.Pow(t, 2);
            obj.transform.position = _firePoint.position + direction * x + Vector3.up * y;
            t += Time.deltaTime;
            yield return null;
        }
    }
    #endregion
}
