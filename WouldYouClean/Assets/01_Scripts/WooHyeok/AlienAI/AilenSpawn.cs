using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilenSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _ailen;
    [SerializeField] private float _spawnTime;

    private float meteorPosY;
    private float meteorPosX;

    void Start()
    {
        meteorPosY = Camera.main.orthographicSize;
        meteorPosX = meteorPosY * Camera.main.aspect + 3;

        meteorPosY += 3;

        StartCoroutine(SpawnAilen());
    }

    IEnumerator SpawnAilen()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnTime);

            float posX = Random.Range(-meteorPosX, meteorPosX);
            float posY = Random.Range(-meteorPosY, meteorPosY);

            var obj = Instantiate(_ailen);
            obj.transform.position = new Vector2(posX, posY);
        }
    }
}
