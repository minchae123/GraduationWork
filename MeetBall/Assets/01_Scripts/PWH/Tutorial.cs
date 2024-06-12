using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Tutorial : MonoBehaviour, IPointerClickHandler
{
    [Header("Explain")]
    [SerializeField] private RectTransform[] explainPos;
    [TextArea] [SerializeField] private string[] explain;

    [SerializeField] private Transform[] uiLayer;
    [SerializeField] private Image fadePanel;

    private TextMeshProUGUI tutorialText;
    private TutorialPanel tutorial;

    private bool nextTut = false;

    void Awake()
    {
        tutorial = GetComponentInChildren<TutorialPanel>();
        tutorialText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public IEnumerator TutorialPannel()
    {
        tutorial.InitializeFirstTut();

        fadePanel.DOFade(0.9f, 1f);
        yield return new WaitForSeconds(1.2f);
        
        for (int i = 0; i < explain.Length; i++)
        {
            UiLayer(i);
            tutorial.SetFirstTutorial(explainPos[i], explain[i]);
            yield return new WaitUntil(() => nextTut);

            tutorial.NextTut();

            yield return new WaitForSeconds(1f);
            nextTut = false;
        }

        gameObject.SetActive(false);
    }

    private void UiLayer(int idx)
    {
        if (idx == uiLayer.Length)
        {
            fadePanel.DOFade(0, 1);
            fadePanel.transform.SetAsFirstSibling();
        }
        else
        {
            fadePanel.transform.SetAsLastSibling();
            uiLayer[idx].SetAsLastSibling();
            transform.SetAsLastSibling();
        }
    }

    public void ResetPanel()
    {
        tutorialText.text = null;
        tutorial.ResetFirstTut();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        nextTut = true;
    }
}
