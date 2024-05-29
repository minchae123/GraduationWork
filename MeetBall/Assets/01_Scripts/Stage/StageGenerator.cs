using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class StageGenerator : MonoBehaviour
{
    public List<GameObject> Blocks;
    public List<Vector3> SaveBlocks;

    private float _radius = 4f;

    public bool isSelected;

    private void Awake()
    {
        foreach (var block in Blocks)
        {
            SaveBlocks.Add(block.transform.position);
        }
    }

    private void Start()
    {
        if (transform.parent == GameObject.Find("Minimap").transform) isSelected = false;
        else isSelected = true;

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
        SetIsInStage(false);

        yield return new WaitForSeconds(.2f);

        for (int i = 0; i < Blocks.Count; i++)
        {
            if (isSelected)
            {
                Blocks[i].transform.DOMove(SaveBlocks[i], .75f);
                yield return new WaitForSeconds(.5f / Blocks.Count);
            }
            else
                Blocks[i].transform.DOMove(SaveBlocks[i], .1f).SetEase(Ease.InExpo);
        }
        SetIsInStage(true);
    }

    private void SetIsInStage(bool value)
    {
        if (FindObjectOfType<StageManager>()) StageManager.Instance.SetIsInStage(value);
        else TutorialStageManager.Instance.SetIsInStage(value);
    }

    void ResetStage()
    {
        if (isSelected)
        {
            foreach (var block in Blocks)
            {
                block.transform.position = new Vector3(block.transform.position.x, 10, block.transform.position.z);
            }
        }
        else
        {
            int count = Blocks.Count;
            float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
            float angleIncrement = Mathf.PI * 2 * goldenRatio;

            for (int i = 0; i < count; i++)
            {
                float t = (float)i / count;
                float inclination = Mathf.Acos(1 - 2 * t); // theta: inclination angle
                float azimuth = angleIncrement * i;

                float x = _radius * Mathf.Sin(inclination) * Mathf.Cos(azimuth);
                float y = _radius * Mathf.Sin(inclination) * Mathf.Sin(azimuth);
                float z = _radius * Mathf.Cos(inclination);

                Blocks[i].transform.DOMove(new Vector3(x, y, z), .15f).SetEase(Ease.OutExpo);
            }
        }
    }
}
