using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    private Camera mainCam;

    [Header("UI")]
    [SerializeField] private GameObject btnContainer;
    [SerializeField] private Transform titleTrm;

    [Header("--")]
    [SerializeField] private Image fadeImg;
    [SerializeField] private Transform info;

    [Header("Etc")]
    [SerializeField] private Transform spaceShip;
    private ParticleSystem ps;

    private void Awake()
    {
        mainCam = Camera.main;
    }
    private void Start()
    {
        info.localScale = new Vector3(1, 0, 1);
        info.gameObject.SetActive(true);
        spaceShip.DOScale(0.9f, 1.5f).SetUpdate(true).SetLoops(-1, LoopType.Yoyo);

        ps = spaceShip.Find("BasicBoost").GetComponent<ParticleSystem>();

        fadeImg.color = Vector4.zero;
    }

    public void OnClickStartButton()
    {
        Cursor.lockState = CursorLockMode.Locked;
        btnContainer.SetActive(false);

        var main = ps.main;
        main.startLifetime = 1f;

        mainCam.transform.DOLocalMoveZ(-20f, 1f);

        DOVirtual.DelayedCall(0.2f, () =>
        {
            spaceShip.DOMoveY(16f, 3f).SetEase(Ease.InOutQuint);
            titleTrm.transform.DOLocalMoveY(0, 2.5f).SetEase(Ease.InOutQuint);
        });
        DOVirtual.DelayedCall(2f, () =>
        {
            fadeImg.DOFade(1, 1f).OnComplete(()=>
            {
                DOTween.Clear();
                SceneManager.LoadScene("Main3D");
            });
        });
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

    public void QuitGame()
	{
        Application.Quit();
	}
}
