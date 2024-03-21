using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private RectTransform doorLock;
    [SerializeField] private string password;

    private string[] answer = { "Success!", "Failed" };
    private string passStr = null;
    private bool isPass = false;

    void Update()
    {
        if (isPass && passStr.Length >= password.Length)
        {
            if (password == passStr)
                StartCoroutine(GuessPassword(answer[0]));
            else
                StartCoroutine(GuessPassword(answer[1]));

            passStr = null;
            isPass = false;
        }
    }

    private IEnumerator GuessPassword(string result)
    {
        yield return new WaitForSeconds(0.5f);
        textBox.text = result;

        yield return new WaitForSeconds(1f);
        UndoLock(result);
    }

    private void UndoLock(string result)
    {
        textBox.text = null;

        if (result == answer[0])
            UIManager.Instance.ClosePanel(doorLock);
    }

    public void NumBtn(int num)
    {
        isPass = true;

        passStr += num.ToString();

        textBox.text = passStr;
    }
}
