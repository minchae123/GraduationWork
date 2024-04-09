using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _cleanRobot;
    [SerializeField] private GameObject _meteor;
    [SerializeField] private GameObject _ailen;

    [SerializeField] private float _meteorSpawnTime; 
    [SerializeField] private float _ailenSpawnTime;
    [SerializeField] private float _speed;

    private float spawnPosY;
    private float meteorPosX;
    private float ailenPosX;

    void Start()
    {
        spawnPosY = Camera.main.orthographicSize;

        meteorPosX = spawnPosY * Camera.main.aspect;
        ailenPosX = spawnPosY * Camera.main.aspect + 3;

        spawnPosY += 3f;

        StartCoroutine(SpawnMeteor());
        //StartCoroutine(SpawnAilen());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameObject obj = Instantiate(_cleanRobot);
            //플레이어 위치에서 소환
        }
    }

    IEnumerator SpawnMeteor()
    {
        while (true)
        {
            yield return new WaitForSeconds(_meteorSpawnTime);

            float posY = spawnPosY + Camera.main.transform.position.y;
            float posX = Random.Range(-meteorPosX, meteorPosX);
            posX += Camera.main.transform.position.x;

            var obj = Instantiate(_meteor);
            obj.transform.position = new Vector2(posX, posY);

            Vector2 dir = (Vector2.right + Vector2.down * 2).normalized;
            obj.GetComponent<Rigidbody2D>().velocity = dir * _speed;
        }
    }

    IEnumerator SpawnAilen()
    {
        while (true)
        {
            yield return new WaitForSeconds(_ailenSpawnTime);

            float posX = Random.Range(-ailenPosX, ailenPosX);
            float posY = Random.Range(-spawnPosY, spawnPosY);

            var obj = Instantiate(_ailen);
            obj.transform.position = new Vector2(posX, posY);
        }
    }
}
