using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using echo17.EndlessBook;
using echo17.EndlessBook.Demo02;
using System;


public class StageSelector : PageView
{
    [Serializable]
    public struct stageInfo
    {
        public string gameObjectName;
        public int stageNumber;
    }

    public stageInfo[] stageInfos;

    protected override bool HandleHit(RaycastHit hit, BookActionDelegate action = null)
    {
        if (action == null) return false;
        foreach (var stageInfo in stageInfos)
        {
            if (stageInfo.gameObjectName == hit.collider.gameObject.name)
            {
                print(stageInfo.gameObjectName);
                print(stageInfo.stageNumber);
                GameManager.Instance.Game.SetActive(true);
                StageManager.Instance.SetStageNumber(stageInfo.stageNumber);
                GameManager.Instance.Book.SetActive(false);
                return true;
            }

        }

        return false;
    }
}