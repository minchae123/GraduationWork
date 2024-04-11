using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    SpriteRenderer spriteRen;

    private void OnEnable()
    {
        spriteRen = GetComponent<SpriteRenderer>();

        int idx = Random.Range(0, sprites.Count);
        spriteRen.sprite = sprites[idx];

        Invoke("DestroyObj", 5);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponentInParent<PlayerHp>().OnDamage(20);
            DestroyObj();//풀링으로 바꾸기
        }
    }

    private void DestroyObj() => Destroy(gameObject);
}
