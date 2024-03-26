using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VacuumCleaner : MonoBehaviour
{
    public List<Transform> trashes;

    [Range(2,10)]
    public float rebootTime;
    [Range(0,20)]
    public float moveSpeed;

    // �����ϰ� ������ ã��
    private void Start()
    {
        FirstRun();
    }

    public void FirstRun()
    {
        trashes = new List<Transform>();

        Transform[] trasharr = GameObject.FindGameObjectsWithTag("Trash").Select(obj => obj.transform).ToArray(); // ������Ʈ�� transform array �� ����
        trashes.AddRange(trasharr); // �迭�� ����Ʈ �ȿ� �־���

        FindClosetTrash(); // ���� ����� ������ ã�� ����
    }

    private IEnumerator FindTrashes(Transform closestTrash)
    {
        Vector3 curPos = transform.position; // ����
        Vector3 targetPos = closestTrash.position; // ��ǥ

        float distance = Vector3.Distance(curPos, targetPos);
        float duration = distance / moveSpeed;

        float time = 0f;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(curPos, targetPos, time / duration); // ��� �̵�
            time += Time.deltaTime; //++
            yield return null;
        }


        trashes.Remove(closestTrash); // ã�� ������ ����Ʈ���� ���� (���̻� ã�� �ʿ�X)

        closestTrash.GetComponent<MeshRenderer>().material.color = Color.red; // Ȯ�ο�, ���� ����
        yield return new WaitForSeconds(rebootTime); // �� �� �ִٰ� �ٽ� ã�� ������
        FindClosetTrash(); // �ٽ� ã��

        yield return null;
    }

    private void FindClosetTrash()
    {
        if(trashes.Count == 0) // �� ã������ (����Ʈ�� �����ִ� �� ���ٸ�)
        {
            print("no trash left");
            return; // ���̻� �ڵ忡�� ���ư��� ��X
        }

        Transform closestTrash = null;

        float min = float.MaxValue;
        Vector3 curPos = transform.position; 

        foreach (Transform t in trashes)
        {
            float dis = Vector3.Distance(curPos, t.position);// ���� û�ұ��� pos�� �������� pos�� �Ÿ��� ���
            if (dis < min) // ���� ����� �� ����
            {
                closestTrash = t;
                min = dis;
            }
        }

        if (closestTrash == null) return; // Ȥ�� �𸣴ϱ�...

        StartCoroutine(FindTrashes(closestTrash)); // �� ������� ����
    }
}
