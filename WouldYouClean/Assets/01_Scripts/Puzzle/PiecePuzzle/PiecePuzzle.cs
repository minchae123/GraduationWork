using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePuzzle : MonoBehaviour
{
	public List<MovePuzzle> puzzle;
	public Dictionary<MovePuzzle, bool> puzzles = new Dictionary<MovePuzzle, bool>();

	private void Start()
	{
		foreach(var i in puzzle)
		{
			puzzles.Add(i, false);
		}
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Q))
		{
			print(CheckAnswer());

		}
	}

	public bool CheckAnswer()
	{
		foreach(var i in puzzles)
		{
			if(i.Value == false)
				return false;
		}
		return true;
	}

	public void CorrectPuzzle(MovePuzzle p ,bool value)
	{
		puzzles[p] = value;
	}
}
