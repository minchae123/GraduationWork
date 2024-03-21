using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeItem : MonoBehaviour
{
    private int itemLevel = 0;

    private Rigidbody2D rigid;
    private CircleCollider2D col;

    private SpriteRenderer sr;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();

        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        itemLevel = Random.Range(0, 3); // 0~2사이중하나
        MergeClass info = MergeManager.Instance.SetItem(itemLevel);

        sr.color = info.sprite;
        transform.localScale = new Vector2(info.scale, info.scale);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<MergeItem>(out MergeItem m))
        {
            int checkLevel = m.GetLevel();
            if(checkLevel == itemLevel)
            {
                if (gameObject.GetInstanceID() < m.gameObject.GetInstanceID()) // 둘중에 하나만 부서짐
                {
                    print("합체~");
                    Destroy(collision.gameObject);
                    MergeClass info = MergeManager.Instance.SetItem(++itemLevel);

                    sr.color = info.sprite;
                    transform.localScale = new Vector2(info.scale, info.scale);
                }
            }
        }
    }
    public int GetLevel() => itemLevel;
}

