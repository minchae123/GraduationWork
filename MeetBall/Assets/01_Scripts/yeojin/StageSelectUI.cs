using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageSelectUI : MonoBehaviour, IPointerClickHandler
{
    private bool isClick = false;
    private List<SelectUI> selectUI;

    private void Start()
    {
        selectUI = transform.parent.GetComponentsInChildren<SelectUI>().ToList();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isClick = !isClick;

        StartCoroutine(ClickAnimation(isClick)); // 애니메이션 실행
    }

    private IEnumerator ClickAnimation(bool isClick)
    {
        if (isClick)
        {
            foreach(SelectUI s in selectUI)
            {
                s.MoveToSelect();
                yield return new WaitForSeconds(0.3f);
            }
        }
        else
        {
            foreach (SelectUI s in selectUI)
            {
                s.MoveToOrigin();
                yield return new WaitForSeconds(0.3f);
            }
        }
    }

    public void OnClickStage()
    {
        // 스테이지로 이동
    }
    public void OnClickPainting()
    {
        // 페인트
    }
}
