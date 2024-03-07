using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PuzzleObject : MonoBehaviour
{
    private bool IsRotating = false;

    public void Rotate()
    {
        StartCoroutine(RotateCor());
    }

    private IEnumerator RotateCor()
    {
        yield return new WaitUntil(() => !IsRotating);
        IsRotating = true;

        Vector3 targetEulerAngles = transform.eulerAngles + new Vector3(0f, 0f, 90f);
        transform.DORotate(targetEulerAngles, 0.5f).OnComplete(() =>
        {
            ReachPuzzleManager.Instance.CheckAllAreaReached();
            IsRotating = false;
        });
    }
}
