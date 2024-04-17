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

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			_mat.color += other.gameObject.GetComponent<Renderer>().material.color;
			_mat.SetColor("_EmissionColor", _mat.color);
			Destroy(other.gameObject);
			ClearAnim.SetTrigger("Clear");

			StartCoroutine(LevelClear());
		}
	}


	private void OnCollisionEnter(Collision collision)
	{

	}

	IEnumerator LevelClear()
	{
		yield return new WaitForSeconds(1);
		StageManager.Instance.ClearStage();
	}
}
