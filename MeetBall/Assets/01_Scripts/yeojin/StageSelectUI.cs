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

        StartCoroutine(ClickAnimation(isClick)); // �ִϸ��̼� ����
    }

    private IEnumerator ClickAnimation(bool isClick)
    {
        WaitForSeconds waitflag = new WaitForSeconds(0.3f);

        if (isClick)
        {
            foreach(SelectUI s in selectUI)
            {
                s.MoveToSelect();
                yield return waitflag;
            }
        }
        else
        {
            foreach (SelectUI s in selectUI)
            {
                s.MoveToOrigin();
                yield return waitflag;
            }
        }
    }

    public void OnClickStage()
    {
        // ���������� �̵�
    }
    public void OnClickPainting()
    {
        // ����Ʈ
    }
}
