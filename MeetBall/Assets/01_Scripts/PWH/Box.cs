using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Save
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

        if (player1Dis <= _saveDis)
            MoveBox(player1Dis, _leftPlayer, Save.P1);
        if (player2Dis <= _saveDis)
            MoveBox(player2Dis, _rightPlayer, Save.P2);
    }

    private bool SameWay()
    {
        _leftPlayerDir = (transform.position - _leftPlayer.position).normalized;
        _rightPlayerDir = (transform.position - _rightPlayer.position).normalized;

        return _leftPlayerDir == -_rightPlayerDir;
    }

    private void MoveBox(float dis, Transform player, Save save)
    {
        if (dis > _distance)
            _saveDir[save] = MoveDir(player);
        else
            transform.position += _saveDir[save];
    }

    private Vector3 MoveDir(Transform player)
    {
        Vector3 playerPos = player.position;
        Vector3 toPlayerDir = playerPos - transform.position;

        return -toPlayerDir.normalized;
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
            Destroy(gameObject);
    }
    #endregion
}
