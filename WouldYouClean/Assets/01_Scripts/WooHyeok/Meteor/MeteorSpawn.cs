using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _meteor;
    [SerializeField] private float _spawnTime;
    [SerializeField] private float _speed;

    private float meteorPosY;
    private float meteorPosX;

    void Start()
    {
        meteorPosY = Camera.main.orthographicSize;
        meteorPosX = meteorPosY * Camera.main.aspect;

        meteorPosY += 3f;

        StartCoroutine(SpawnMeteor());
    }

    IEnumerator SpawnMeteor()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnTime);

            float posX = Random.Range(-meteorPosX, meteorPosX);

            var obj = Instantiate(_meteor);
            obj.transform.position = new Vector2(posX, meteorPosY);

            Vector2 dir = (Vector2.right + Vector2.down * 2).normalized;
            obj.GetComponent<Rigidbody2D>().velocity = dir * _speed;
        }
    }
}
