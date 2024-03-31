using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class TitleScene : MonoBehaviour
{
    [SerializeField] private Transform info;
    [SerializeField] private Transform spaceShip;

    private void Start()
    {
        info.transform.localScale = new Vector3(1, 0, 1);
        spaceShip.DOScale(0.9f, 1.5f).SetUpdate(true).SetLoops(-1, LoopType.Yoyo);
    }

    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Main3D");
    }
    public void OnClickInfoButton()
    {
        Time.timeScale = 0;
        info.DOScaleY(1, 0.5f).SetUpdate(true).SetEase(Ease.InOutCubic);
    }
    public void OnClickInfoExitButton()
    {
        info.DOScaleY(0, 0.5f).SetUpdate(true).SetEase(Ease.InOutCubic)
            .OnComplete(() => Time.timeScale = 1);
    }
}
