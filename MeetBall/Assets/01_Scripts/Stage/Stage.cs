using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public static Stage Instance;

    private bool isInStage = false;

    [SerializeField] private Transform stageTrm;
    [SerializeField] private StageUI[] stages;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        stages = stageTrm.GetComponentsInChildren<StageUI>();

        for(int i = 0; i < stages.Length; ++i)
        {
            stages[i].SetNumber(i + 1);
        }
    }

    public void LoadStage(int stageNum)
    {
        //
        if (isInStage) return;

        isInStage = true;
    }

    public void ResetStage()
    {
        isInStage = false;
    }
}
