using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Save
{
    P1,
    P2
}

public class Box : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private Transform _leftPlayer;
    [SerializeField] private Transform _rightPlayer;
    [SerializeField] private LayerMask _layer;

    [Header("수치")]
    [SerializeField] private float _distance;
    [SerializeField] private float _saveDis;

    public Vector3 _leftPlayerDir { get; private set; } = Vector3.zero;
    public Vector3 _rightPlayerDir { get; private set; } = Vector3.zero;

    private Dictionary<Save, Vector3> _saveDir = new Dictionary<Save, Vector3>();
    private Vector3 _abcd;

    private void Start()
    {
        _saveDir[Save.P1] = Vector3.zero;
        _saveDir[Save.P2] = Vector3.zero;
    }

    public void Determine()
    {
        float player1Dis = Vector3.Distance(_leftPlayer.position, transform.position);
        float player2Dis = Vector3.Distance(_rightPlayer.position, transform.position);

        if (player1Dis <= _saveDis && player2Dis <= _saveDis && SameWay())
            return;
        else
        {
            _leftPlayerDir = Vector3.zero;
            _rightPlayerDir = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            print(gameObject);
            print(player2Dis);
        }

        if (player1Dis <= _saveDis)
            MoveBox(player1Dis, _leftPlayer, Save.P1);
        if (player2Dis <= _saveDis)
            MoveBox(player2Dis, _rightPlayer, Save.P2);
    }

    private bool SameWay()
    {
        _leftPlayerDir = (transform.position - _leftPlayer.position).normalized;
        _rightPlayerDir = (transform.position - _rightPlayer.position).normalized;

        if (_leftPlayerDir == Vector3.zero) return false;
        return _leftPlayerDir == -_rightPlayerDir;
    }

    private void MoveBox(float dis, Transform player, Save save)
    {
        if (dis > _distance)
            _saveDir[save] = MoveDir(player);
        else
        {
            print("ss");
            transform.position += _saveDir[save];
        }
        _abcd = _saveDir[save];
    }

    private Vector3 MoveDir(Transform player)
    {
        Vector3 playerPos = player.position;
        Vector3 moveDir = playerPos - transform.position;

        return -moveDir.normalized;
    }

    #region 박스 낙하
    private bool _isFall = false;

    private void OnTriggerEnter(Collider other)
    {
        transform.GetComponent<Rigidbody>().useGravity = false;
        _isFall = false;
    }

    private void OnTriggerExit(Collider other)
    {
        transform.GetComponent<Rigidbody>().useGravity = true;
        _isFall = true;

        StartCoroutine(DeleteBox());

    }

    private IEnumerator DeleteBox()
    {
        yield return new WaitForSeconds(3);

        if (_isFall)
        {
            BoxManager.Instance.RemoveBox(this);
            Destroy(gameObject);
        }
    }
    #endregion
}
