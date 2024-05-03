using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombineColor : MonoBehaviour
{
    public Animator ClearAnim;

    private Material _mat;

    [SerializeField] private ParticleSystem clearParticle;

    private void Awake()
    {
        _mat = GetComponent<MeshRenderer>().material;

		ClearAnim = GameObject.Find("ClearUIAnim").GetComponent<Animator>();

		clearParticle = GameObject.Find("ClearParticle").GetComponent<ParticleSystem>();
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			_mat.color += other.gameObject.GetComponent<Renderer>().material.color;
			_mat.SetColor("_EmissionColor", _mat.color);
			Destroy(other.gameObject);
			ClearAnim.SetTrigger("Clear");

			BoxManager.Instance.CleanBox();
            clearParticle.Play();
            StageManager.Instance.ClearStage();
		}
	}


	private void OnCollisionEnter(Collision collision)
	{

	}
}
