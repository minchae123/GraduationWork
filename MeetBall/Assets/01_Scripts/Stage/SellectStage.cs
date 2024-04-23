using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SellectStage : MonoBehaviour
{
    [SerializeField] private StageListSO stageSO;
    [SerializeField] private StageUI stagePrefab;

    [SerializeField] private Transform stageParent;
    public StageUI[] stages;
    public float[] xPos;
    private int selectedStage = 0;

    private void Start()
    {
        xPos = new float[stageSO.Stages.Count];
        stages = new StageUI[stageSO.Stages.Count];

        for (int i = 0; i < stageSO.Stages.Count; ++i)
        {
            StageUI newStage = Instantiate(stagePrefab, stageParent);
            newStage.SetUI(stageSO.Stages[i], i + 1);

            stages[i] = newStage;
        }

        for(int i = 0; i < stages.Length; ++i)
        {
            xPos[i] = 900 - 290 * i;
        }

        stages[selectedStage].Selected();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            UpdateStageUI(-1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            UpdateStageUI(1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print(stages[selectedStage].stage);
        }
    }

    private void UpdateStageUI(int value)
    {
        stages[selectedStage].UnSelected();

        selectedStage += value;
        selectedStage = Mathf.Clamp(selectedStage, 0, stages.Length - 1);

        stageParent.DOLocalMoveX(xPos[selectedStage], 0.5f);
        stages[selectedStage].Selected();
    }
}
