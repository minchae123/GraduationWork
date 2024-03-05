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
    
    private int mazeSize = 35; // 35 * 35 ����� �̷�
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
        for (int i = 0; i < mazeSize; ++i) // �ʱ� ����
        {
            for(int j = 0; j < mazeSize; ++j)
            {
                if(i % 2 == 0 || j % 2 == 0) maze[i, j] = TileType.WALL; // ¦���� �켱 ����
                else maze[i, j] = TileType.EMPTY;

                DrawTile(i, j);
            }
        }

        for(int i=0;i<mazeSize;++i)
        {
            for(int j=0;j<mazeSize;++j)
            {
                if (i % 2 == 0 || j % 2 == 0) // ¦���� �̹� �շ�����
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

        for (int i = 0; i < mazeSize; ++i) // ������
        {
            for (int j = 0; j < mazeSize; ++j)
            {
                if (i == 0 || i == mazeSize - 1 || j == 0 || j == mazeSize - 1)
                {
                    maze[i, j] = TileType.WALL; // �����ڸ� ���������� ���� ������
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
            tileMap.SetTile(pos, wallTile); //Ÿ�� ����
        }
        else
        {
            tileMap.SetTile(pos, null);
        }
    }
}
