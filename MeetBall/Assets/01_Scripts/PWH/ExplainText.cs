using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ExplainText : MonoBehaviour
{
    [SerializeField] private string[] explain;

    private TutorialPanel panel;
    private TextMeshProUGUI explainTxt;

    void Start()
    {
        panel = GetComponent<TutorialPanel>();
        explainTxt = GetComponentInChildren<TextMeshProUGUI>();

        StartCoroutine(ShowPanel());
    }

    IEnumerator ShowPanel()
    {
        yield return new WaitForSeconds(1f);

        panel.ShowTutorial(()=> StartCoroutine(Typing(explain[PlayerPrefs.GetInt(GameManager.Instance.stage)])));
    }

    IEnumerator Typing(string text)
    {
        StringBuilder builder = new StringBuilder();

        foreach (char ch in text)
        {
            builder.Append(ch);
            explainTxt.text = builder.ToString();

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1f);

        explainTxt.text = null;
        panel.CloseTutorial();
    }
}
