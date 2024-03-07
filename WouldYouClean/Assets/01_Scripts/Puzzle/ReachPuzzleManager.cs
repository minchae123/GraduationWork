using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachPuzzleManager : MonoSingleton<ReachPuzzleManager>
{
    public List<CheckArea> checkAreas;

    private void Start()
    {
        checkAreas = new List<CheckArea>(FindObjectsOfType<CheckArea>());
    }

    public void CheckAllArea()
    {
        int cnt = 0;        
        foreach (CheckArea checkArea in checkAreas)
        {
            if (!checkArea.IsReached)
            {
                if (++cnt > 2)
                {
                    return;
                }
            }
        }

        print("Ŭ����");
    } 

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, GameManager.Instance.mainCam.transform.forward);
            if (hit.collider != null)
            {
                PuzzleObject obj = hit.transform.gameObject.GetComponentInParent<PuzzleObject>();
                if (obj != null) 
                {
                    obj.Rotate();
                }
            }
        }
    }
}
