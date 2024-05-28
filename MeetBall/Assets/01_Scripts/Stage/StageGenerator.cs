using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class StageGenerator : MonoBehaviour
{
    public List<GameObject> Blocks;
    public List<Vector3> SaveBlocks;

    private float _radius = 3f;

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

        StartCoroutine(StageLoad());
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
            Blocks[i].transform.DOMove(SaveBlocks[i], .75f);
            //yield return new WaitForSeconds(.5f / Blocks.Count);
        }
        yield return null;
    }

    void ResetStage()
    {
        //foreach (var block in Blocks)
        //{
        //    block.transform.position = new Vector3(block.transform.position.x, 10, block.transform.position.z);
        //}
        int count = Blocks.Count;
        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;

        for (int i = 0; i < count; i++)
        {
            float t = (float)i / count;
            float inclination = Mathf.Acos(1 - 2 * t); // theta: inclination angle
            float azimuth = angleIncrement * i;

            float x = 8 * Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = 8 * Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            float z = 8 * Mathf.Cos(inclination);

            Blocks[i].transform.position = new Vector3(x, y, z);
        }
    }
}
