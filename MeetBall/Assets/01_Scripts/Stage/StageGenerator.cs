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

       StartCoroutine( StageLoad());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
            StageLoad();
    }

    IEnumerator StageLoad()
    {
        for (int i = 0; i < Blocks.Count; i++)
        {
            Blocks[i].transform.DOMove(SaveBlocks[i], .1f);
            yield return new WaitForSeconds(.1f);
        }
    }

    void ResetStage()
    {
        foreach (var block in Blocks)
        {
            block.transform.position = new Vector3(block.transform.position.x, 10, block.transform.position.z);
        }
    }
}
