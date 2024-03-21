using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeItemMove : MonoBehaviour
{
    private Rigidbody2D rigid;
    private bool IsClick = true;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (!IsClick) return;
        if(Input.GetMouseButton(0))
        {
            Vector3 mousePos = GameManager.Instance.mainCam.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, transform.position.y, 0);// 좌우로만 움직여야 하니까...
        }
        else if(Input.GetMouseButtonUp(0))
        {
            rigid.gravityScale = 1;
            IsClick = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MergeManager.Instance.SpawnMerge();
        Destroy(this);
    }
}
