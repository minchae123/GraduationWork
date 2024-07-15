using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Books
{
    public string name;
    public List<Material> Mat;
}

public class BookList : MonoBehaviour
{
    public Books[] books;

    private void Awake()
    {
        foreach (var book in books)
        {
            for (int i = 0; i < book.Mat.Count; i++)
            {
                book.Mat[i].SetFloat("_Progress", 0);
            }
        }
    }

    //EndlessBook에서 바꾸는거 해야함

    //책 몇페이지 몇번째를 색 바꿀지
    public void ChangeProgress(int bookNum, int index)
    {
        StartCoroutine(ChangeParameter(bookNum, index));
    }

    private IEnumerator ChangeParameter(int bookNum, int index)
    {
        float startValue = 0f;
        float endValue = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < 1)
        {
            // 경과된 시간 비율
            float t = elapsedTime / 1;
            // 선형 보간을 사용하여 값을 계산
            float value = Mathf.Lerp(startValue, endValue, t);
            // 애니메이터 파라미터 설정
            books[bookNum].Mat[index].SetFloat("_Progress", value);

            // 경과된 시간 업데이트
            elapsedTime += Time.deltaTime;
            // 한 프레임 대기
            yield return null;
        }

        // 애니메이터 파라미터를 최종 값으로 설정 (마지막에 정확한 값 설정)
        books[bookNum].Mat[index].SetFloat("_Progress", endValue);
    }
}
