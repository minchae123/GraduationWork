using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickUiRect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private HiddenSO hiddenSO;

    [SerializeField] private RectTransform[] InstPos;
    [SerializeField] private RectTransform spawnParent;

    private RectTransform saveL;
    private RectTransform saveR;

    private int count = 0;
    private int level = 0;

    private void ShowPanel(RectTransform panel)
    {
        panel.transform.DOScale(Vector2.one * 1f, 1.5f).SetEase(Ease.InOutQuint);
    }

    private void ClosePanel(RectTransform rect)
    {
        rect.transform.DOScale(Vector2.zero, 1f).SetEase(Ease.InOutQuint);
    }

    public void StartBtn()
    {
        ShowPanel(spawnParent);
        CreateLevel();
    }

    private void ChangeImg(Button btn)
    {
        btn.image.sprite = hiddenSO._circle;
    }

    private void FindDifferent()
    {
        count++;

        NextLevel(count == 3);
        GameDone(level < hiddenSO._left.Length);
    }

    private void GameDone(bool value)
    {
        if (value) return;

        ClosePanel(spawnParent);
    }

    private void NextLevel(bool value)
    {
        if (!value) return;

        level++;
        CreateLevel();
    }

    private void CreateLevel()
    {
        if (level >= hiddenSO._left.Length) return;

        RectTransform left = Instantiate(hiddenSO._left[level], spawnParent);
        RectTransform right = Instantiate(hiddenSO._right[level], spawnParent);

        saveL = left;
        saveR = right;

        ShowPanel(left);
        ShowPanel(right);

        OnBtnListener(left);

        left.anchoredPosition = InstPos[0].anchoredPosition;
        right.anchoredPosition = InstPos[1].anchoredPosition;

        count = 0;
    }

    private void OnBtnListener(RectTransform parent)
    {
        foreach (RectTransform item in parent)
        {
            Button btn = item.GetComponent<Button>();

            btn.onClick.AddListener(() => ChangeImg(btn));
            btn.onClick.AddListener(FindDifferent);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FailedMotion();
    }

    private void FailedMotion()
    {
        transform.DOShakePosition(0);
    }
}
