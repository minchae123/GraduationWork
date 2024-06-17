using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombineColor : MonoBehaviour
{
    private MeshRenderer render;
    private Movement movement;

    private void Awake()
    {
        render = GetComponent<MeshRenderer>();
        movement = GetComponent<Movement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isInStage = false;

        if(FindObjectOfType<StageManager>()) isInStage = StageManager.Instance.IsInStage;
        else isInStage = TutorialStageManager.Instance.IsInStage;

        if (isInStage && other.TryGetComponent<Movement>(out Movement m))
        {
            Color c1 = render.sharedMaterial.GetColor("_PlayerColor");
            Color c2 = other.gameObject.GetComponent<MeshRenderer>().sharedMaterial.GetColor("_PlayerColor");

            Color combineColor = new Color(c1.r + c2.r, c1.g + c2.g, c1.b + c2.b, 1);
            //print($"c1: {c1}, c2: {c2}, combine: {combineColor}");

            render.sharedMaterial.SetColor("_PlayerColor", combineColor);

            //CameraMovement.Instance.CameraReset();
            PlayerManager.Instance.DestroyPlayer(m);

            OriginColorEnum enum1 = m.PlayerColor;
            OriginColorEnum enum2 = movement.PlayerColor;

            bool IsClear = GameManager.Instance.MergeColor(enum1, enum2);
            StageManager.Instance.SetIsInStage(false);

            StartCoroutine(UIAnim(IsClear));
        }
    }
    private IEnumerator UIAnim(bool isClear)
    {
        transform.DOMove(Vector3.zero - Camera.main.transform.forward * 2, 1, false);
        transform.DOScale(.5f, 1);
        yield return new WaitForSeconds(1);

        StageManager.Instance.ClearStage(isClear);
    }
}
