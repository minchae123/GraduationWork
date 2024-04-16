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
	[Header("플레이어")]
	[SerializeField] private Transform _player1;
	[SerializeField] private Transform _player2;

	[Header("수치")]
	[SerializeField] private float _distance;
	[SerializeField] private float _saveDis;

	public Vector3 _player1Dir { get; private set; } = Vector3.zero;
	public Vector3 _player2Dir { get; private set; } = Vector3.zero;

	private Dictionary<Save, Vector3> _saveDir = new Dictionary<Save, Vector3>();

	public void Determine()
	{
		float player1Dis = Vector3.Distance(_player1.position, transform.position);
		float player2Dis = Vector3.Distance(_player2.position, transform.position);

		if (player1Dis <= _saveDis && player2Dis <= _saveDis && SameWay())
			return;
		else
		{
			_player1Dir = Vector3.zero;
			_player2Dir = Vector3.zero;
		}

		if (player1Dis <= _saveDis)
			MoveBox(player1Dis, _player1, Save.P1);
		if (player2Dis <= _saveDis)
			MoveBox(player2Dis, _player2, Save.P2);
	}

	private bool SameWay()
	{
		_player1Dir = (transform.position - _player1.position).normalized;
		_player2Dir = (transform.position - _player2.position).normalized;

		return _player1Dir == -_player2Dir;
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
}
