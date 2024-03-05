using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public enum TileType
{
    EMPTY = 0,
    WALL = 1
}
public class Maze : MonoBehaviour
{
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private Tile wallTile;
    
    private int mazeSize = 35; // 35 * 35 모양의 미로
    private TileType[,] maze;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Generate();
    }

    private void Generate()
    {
        Init();
    }
    private void Init()
    {
        maze = new TileType[mazeSize, mazeSize];
        for (int i = 0; i < mazeSize; ++i) // 초기 설정
        {
            for(int j = 0; j < mazeSize; ++j)
            {
                if(i % 2 == 0 || j % 2 == 0) maze[i, j] = TileType.WALL; // 짝수는 우선 막어
                else maze[i, j] = TileType.EMPTY;

                DrawTile(i, j);
            }
        }

        for(int i=0;i<mazeSize;++i)
        {
            for(int j=0;j<mazeSize;++j)
            {
                if (i % 2 == 0 || j % 2 == 0) // 짝수는 이미 뚫려있음
                    continue;

                if (Random.Range(0, 2) == 0)
                {
                    maze[i, j + 1] = TileType.EMPTY; 
                }
                else
                {
                    maze[i+1, j] = TileType.EMPTY;
                }

                DrawTile(i, j);
            }
        }

        for (int i = 0; i < mazeSize; ++i) // 마지막
        {
            for (int j = 0; j < mazeSize; ++j)
            {
                if (i == 0 || i == mazeSize - 1 || j == 0 || j == mazeSize - 1)
                {
                    maze[i, j] = TileType.WALL; // 가장자리 마지막으로 막고 끝내기
                }
                DrawTile(i, j);
            }
        }
    }
    private void DrawTile(int i, int j)
    {
        Vector3Int pos = new Vector3Int(-mazeSize / 2 + i, -mazeSize / 2 + j, 0);

        if (maze[i, j] == TileType.WALL) 
        {
            tileMap.SetTile(pos, wallTile); //타일 생성
        }
        else
        {
            tileMap.SetTile(pos, null);
        }
    }
}
