using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DescriptionPanel : MonoBehaviour
{
    public Image colorImage;
    public List<Image> colorImageList;

    public void SetPanel(StageSO stage)
    {
        TargetColorEnum targetColor = stage.targetColor;
        List<OriginColorEnum> needColors = new List<OriginColorEnum>();

        switch (targetColor)
        {
            case TargetColorEnum.YELLOW:
                needColors.Add(OriginColorEnum.RED);
                needColors.Add(OriginColorEnum.GREEN);
                break;
            case TargetColorEnum.MAGENTA:
                needColors.Add(OriginColorEnum.RED);
                needColors.Add(OriginColorEnum.BLUE);
                break;
            case TargetColorEnum.MINT:
                needColors.Add(OriginColorEnum.GREEN);
                needColors.Add(OriginColorEnum.BLUE);
                break;
            case TargetColorEnum.WHITE:
                needColors.Add(OriginColorEnum.RED);
                needColors.Add(OriginColorEnum.GREEN);
                needColors.Add(OriginColorEnum.BLUE);
                break;
        }

        colorImageList.ForEach(c => DestroyImmediate(c.gameObject));
        colorImageList.Clear();

        for (int i = 0; i < needColors.Count; ++i)
        {
            Image image = Instantiate(colorImage, this.transform);

            colorImageList.Add(image);
            image.color = GameManager.Instance.FindColor(needColors[i]);
        }

        this.GetComponent<RectTransform>().sizeDelta = needColors.Count <= 2 ? new Vector2(270f, 150f) : new Vector2(380f, 150f);
        transform.localScale = new Vector3(0, 1, 1);
    }
    public void OpenPanel()
    {
        transform.DOScaleX(1f, 0.5f);
    }
    public void ClosePanel()
    {
        transform.DOScaleX(0f, 0.5f);
    }
}
