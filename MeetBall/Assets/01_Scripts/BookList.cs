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

    //EndlessBook���� �ٲٴ°� �ؾ���

    //å �������� ���°�� �� �ٲ���
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
            // ����� �ð� ����
            float t = elapsedTime / 1;
            // ���� ������ ����Ͽ� ���� ���
            float value = Mathf.Lerp(startValue, endValue, t);
            // �ִϸ����� �Ķ���� ����
            books[bookNum].Mat[index].SetFloat("_Progress", value);

            // ����� �ð� ������Ʈ
            elapsedTime += Time.deltaTime;
            // �� ������ ���
            yield return null;
        }

        // �ִϸ����� �Ķ���͸� ���� ������ ���� (�������� ��Ȯ�� �� ����)
        books[bookNum].Mat[index].SetFloat("_Progress", endValue);
    }
}
