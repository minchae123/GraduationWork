using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpShop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetText(string s)
    {
        text.text = s;
        DestroyItemPanel();
    }

    private void DestroyItemPanel()
    {
        Sequence seq = DOTween.Sequence();
        seq.SetDelay(1.5f);
        //seq.Append(transform.DOScale(0, 1f));
        seq.AppendCallback(() =>
        {
            Destroy(gameObject);
        });
    }
}
