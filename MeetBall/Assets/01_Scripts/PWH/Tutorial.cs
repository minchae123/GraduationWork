using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private RectTransform[] explainPos;
    [SerializeField] private string[] explain;

    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private RectTransform panel;

    private TutorialPanel tutorial;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(TutorialPannel());
    }

    IEnumerator Typing(string text)
    {
        yield return new WaitForSeconds(1f);

        StringBuilder builder = new StringBuilder();

        foreach (char ch in text)
        {
            builder.Append(ch);
            tutorialText.text = builder.ToString();

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1f);

        tutorialText.text = null;
        tutorial.CloseTutorial();
    }

    IEnumerator TutorialPannel()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < explainPos.Length; i++)
        {
            panel.transform.position = explainPos[i].transform.position;

            if (panel.TryGetComponent(out TutorialPanel tutorial))
            {
                this.tutorial = tutorial;
                tutorial.ShowTutorial(() => { StartCoroutine(Typing(explain[i])); });
            }

            yield return new WaitWhile(()=>tutorial.isWait);
        }

        Cursor.lockState = CursorLockMode.None;
    }
}
