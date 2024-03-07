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
    [Header("Maze Tile")]
    [SerializeField] private Tilemap mazeTilemap;
    [SerializeField] private Tile wallTile;
    [SerializeField] private Tile wall2Tile;
    
    private int mazeSize = 55; // 35 * 35 모양의 미로
    private TileType[,] maze;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Generate();
    }

    private void Generate()
    {
        mazeTilemap.ClearAllTiles();
        maze = new TileType[mazeSize, mazeSize];
        for (int i = 0; i < mazeSize; ++i) // 초기 설정
        {
            for (int j = 0; j < mazeSize; ++j)
            {
                if (i % 2 == 0 || j % 2 == 0) maze[i, j] = TileType.WALL; // 짝수는 우선 막어
                else maze[i, j] = TileType.EMPTY;

                DrawTile(i, j);
            }
        }

        for (int i = 0; i < mazeSize; ++i)
        {
            for (int j = 0; j < mazeSize; ++j)
            {
                if (i % 2 == 0 || j % 2 == 0) continue;
                if (i == mazeSize - 2 && j == mazeSize - 2) continue;
                if (i == mazeSize - 2)
                {
                    maze[i, j + 1] = TileType.EMPTY;
                    continue;
                }
                else if (j == mazeSize - 2)
                {
                    maze[i + 1, j] = TileType.EMPTY;
                    continue;
                }

                int rand = Random.Range(0, 3);
                if (rand == 0)
                {
                    maze[i, j + 1] = TileType.EMPTY;
                }
                else if (rand == 1)
                {
                    maze[i + 1, j] = TileType.EMPTY;
                }
                else
                {
                    maze[i, j + 1] = TileType.EMPTY;
                    maze[i + 1, j + 1] = TileType.EMPTY;
                    maze[i + 1, j] = TileType.EMPTY;
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
            if (j > 0 && maze[i, j - 1] == TileType.WALL) 
            {
                mazeTilemap.SetTile(pos, wall2Tile); //타일 생성
            }
            else mazeTilemap.SetTile(pos, wallTile); //타일 생성
        }
        else
        {
            mazeTilemap.SetTile(pos, null);
        }
    }
}
