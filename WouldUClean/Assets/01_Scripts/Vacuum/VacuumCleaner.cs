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

    // 시작하고 쓰레기 찾기
    private void Start()
    {
        FirstRun();
    }

    public void FirstRun()
    {
        trashes = new List<Transform>();

        Transform[] trasharr = GameObject.FindGameObjectsWithTag("Trash").Select(obj => obj.transform).ToArray(); // 오브젝트를 transform array 로 변경
        trashes.AddRange(trasharr); // 배열을 리스트 안에 넣어줌

        FindClosetTrash(); // 가장 가까운 쓰레기 찾기 시작
    }

    private IEnumerator FindTrashes(Transform closestTrash)
    {
        Vector3 curPos = transform.position; // 시작
        Vector3 targetPos = closestTrash.position; // 목표

        float distance = Vector3.Distance(curPos, targetPos);
        float duration = distance / moveSpeed;

        float time = 0f;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(curPos, targetPos, time / duration); // 계속 이동
            time += Time.deltaTime; //++
            yield return null;
        }


        trashes.Remove(closestTrash); // 찾은 쓰레기 리스트에서 삭제 (더이상 찾을 필요X)

        closestTrash.GetComponent<MeshRenderer>().material.color = Color.red; // 확인용, 추후 삭제
        yield return new WaitForSeconds(rebootTime); // 몇 초 있다가 다시 찾을 것인지
        FindClosetTrash(); // 다시 찾기

        yield return null;
    }

    private void FindClosetTrash()
    {
        if(trashes.Count == 0) // 다 찾았으면 (리스트에 남아있는 게 없다면)
        {
            print("no trash left");
            return; // 더이상 코드에서 돌아가는 거X
        }

        Transform closestTrash = null;

        float min = float.MaxValue;
        Vector3 curPos = transform.position; 

        foreach (Transform t in trashes)
        {
            float dis = Vector3.Distance(curPos, t.position);// 현재 청소기의 pos와 쓰레기의 pos의 거리를 계산
            if (dis < min) // 가장 가까운 거 갱신
            {
                closestTrash = t;
                min = dis;
            }
        }

        if (closestTrash == null) return; // 혹시 모르니까...

        StartCoroutine(FindTrashes(closestTrash)); // 그 쓰레기로 가자
    }
}
