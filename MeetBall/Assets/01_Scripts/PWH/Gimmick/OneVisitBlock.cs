using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneVisitBlock : MonoBehaviour
{
	private void OnCollisionExit(Collision collision)
	{
		Destroy(gameObject);//성빈이가 이펙트로 이것저것 깔쌈하게 해주길 바라면서 나는 여기서 마무리
	}
}
