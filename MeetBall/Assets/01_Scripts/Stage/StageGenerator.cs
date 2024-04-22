using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class StageGenerator : MonoBehaviour
{
    public List<GameObject> Blocks;
    public List<Vector3> SaveBlocks;

    private void Awake()
    {
        foreach (var block in Blocks)
        {
            SaveBlocks.Add(block.transform.position);
        }
    }

    private void Start()
    {
        ResetStage();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
            StageLoad();
    }

    void StageLoad()
    {
        for (int i = 0; i < Blocks.Count; i++)
            Blocks[i].transform.DOMove(SaveBlocks[i], 1f);
    }

    void ResetStage()
    {
        foreach (var block in Blocks)
        {
            block.transform.position = new Vector3(Random.Range(5, 10), Random.Range(5, 10), Random.Range(5, 10));
        }
    }
}
