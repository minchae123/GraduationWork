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
        ColorEnum targetColor = stage.targetColor;
        List<ColorEnum> needColors = new List<ColorEnum>();

        switch (targetColor)
        {
            case ColorEnum.NONE:
            case ColorEnum.RED:
            case ColorEnum.GREEN:
            case ColorEnum.BLUE:
                break;
            case ColorEnum.YELLOW:
                needColors.Add(ColorEnum.RED);
                needColors.Add(ColorEnum.GREEN);
                break;
            case ColorEnum.MAGENTA:
                needColors.Add(ColorEnum.RED);
                needColors.Add(ColorEnum.BLUE);
                break;
            case ColorEnum.MINT:
                needColors.Add(ColorEnum.GREEN);
                needColors.Add(ColorEnum.BLUE);
                break;
            case ColorEnum.WHITE:
                needColors.Add(ColorEnum.RED);
                needColors.Add(ColorEnum.GREEN);
                needColors.Add(ColorEnum.BLUE);
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
