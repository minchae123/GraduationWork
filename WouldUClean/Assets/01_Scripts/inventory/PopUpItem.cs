using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopUpItem : MonoBehaviour
{
    private CanvasGroup group;
    [SerializeField] private TextMeshProUGUI itemText;
    [SerializeField] private Image itemIcon;

    private void OnEnable()
    {
        group = GetComponent<CanvasGroup>();
    }

    public void SetItemPanel(string text, Sprite icon)
    {
        itemText.text = text;
        itemIcon.sprite = icon;
        DestroyItemPanel();
    }

    private void DestroyItemPanel()
    {
        Sequence seq = DOTween.Sequence();
        seq.SetDelay(1.5f);
        seq.Append(group.DOFade(0, 0.4f));
        seq.AppendCallback(() =>
        {
            Destroy(gameObject);
        });
    }
}
