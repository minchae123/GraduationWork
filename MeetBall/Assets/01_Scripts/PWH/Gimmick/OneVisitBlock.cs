using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneVisitBlock : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Destroy(gameObject);//�����̰� ����Ʈ�� �̰����� ����ϰ� ���ֱ� �ٶ�鼭 ���� ���⼭ ������
    }
    private void OnCollisionExit(Collision collision)
	{
		Destroy(gameObject);//�����̰� ����Ʈ�� �̰����� ����ϰ� ���ֱ� �ٶ�鼭 ���� ���⼭ ������
	}
}
