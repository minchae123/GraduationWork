using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombineColor : MonoBehaviour
{
    public Animator ClearAnim;

    private Material _mat;

    private void Awake()
    {
        _mat = GetComponent<MeshRenderer>().material;

		ClearAnim = GameObject.Find("ClearUIAnim").GetComponent<Animator>();	
    }


	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			_mat.color += collision.gameObject.GetComponent<Renderer>().material.color;
			_mat.SetColor("_EmissionColor", _mat.color);
			Destroy(collision.gameObject);
			ClearAnim.SetTrigger("Clear");

			StartCoroutine(LevelClear());
		}
	}

	IEnumerator LevelClear()
	{
		yield return new WaitForSeconds(1);
		StageManager.Instance.ClearStage();
	}
}
