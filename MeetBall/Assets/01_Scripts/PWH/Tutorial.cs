using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private RectTransform[] explainPos;
    [SerializeField] private string[] explain;
    [SerializeField] private GameObject[] uiLayer;
    [SerializeField] private GameObject fadePanel;

    private TextMeshProUGUI tutorialText;
    private TutorialPanel tutorial;

    void Awake()
    {
        tutorial = GetComponentInChildren<TutorialPanel>();
        tutorialText = GetComponentInChildren<TextMeshProUGUI>();
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
       
        ResetPanel();
    }

    public IEnumerator TutorialPannel()
    {
        fadePanel.GetComponent<Image>().DOFade(0.9f, 1f);

        yield return new WaitForSeconds(1.2f);
        
        for (int i = 0; i < explainPos.Length; i++)
        {
            UiLayer(i);

            tutorial.GetComponent<RectTransform>().transform.position = explainPos[i].transform.position;
            tutorial.ShowTutorial(() => { StartCoroutine(Typing(explain[i])); });

            yield return new WaitWhile(() => tutorial.isWait);
        }

        Cursor.lockState = CursorLockMode.None;
    }

    private void UiLayer(int idx)
    {
        if (idx == uiLayer.Length)
        {
            fadePanel.GetComponent<Image>().DOFade(0, 1);
            fadePanel.transform.SetAsLastSibling();
        }
        else
        {
            fadePanel.transform.SetAsLastSibling();
            transform.SetAsLastSibling();
            uiLayer[idx].transform.SetAsLastSibling();
        }
    }

    public void ResetPanel()
    {
        tutorialText.text = null;
        tutorial.CloseTutorial();
    }
}
