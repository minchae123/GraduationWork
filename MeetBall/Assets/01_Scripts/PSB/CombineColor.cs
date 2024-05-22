using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombineColor : MonoBehaviour
{
    private MeshRenderer render;

    private void Awake()
    {
        render = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Color c1 = render.sharedMaterial.GetColor("_PlayerColor");
            Color c2 = other.gameObject.GetComponent<MeshRenderer>().sharedMaterial.GetColor("_PlayerColor");

            Color combineColor = new Color(c1.r + c2.r, c1.g + c2.g, c1.b + c2.b, 1);
            //print($"c1: {c1}, c2: {c2}, combine: {combineColor}");

            render.sharedMaterial.SetColor("_PlayerColor", combineColor);
            CameraMovement.Instance.CameraReset();
            PlayerManager.Instance.DestroyPlayer(other.gameObject.GetComponent<Movement>());
            StartCoroutine(ClearAnim());
        }
    }

    private IEnumerator ClearAnim()
    {
        //float randomRot = Random.Range(-359, 359);
        //Camera.main.orthographic = false;
        transform.DOMove(Vector3.zero - Camera.main.transform.forward * 2, 1, false);
        transform.DOScale(.5f, 1);
        //transform.DORotate(new Vector3(randomRot, randomRot, randomRot), 1);
        yield return new WaitForSeconds(1);
        //Camera.main.orthographic = true;
    }
}
