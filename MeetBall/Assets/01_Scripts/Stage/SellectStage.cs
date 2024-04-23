using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellectStage : MonoBehaviour
{
    [SerializeField] private Transform stageParent;
    public StageUI[] stages;

    private void Start()
    {
        stages = stageParent.GetComponentsInChildren<StageUI>();
        stages[0].Selected();
    }
}
