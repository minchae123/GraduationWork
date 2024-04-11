using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePuzzle : MonoBehaviour
{
    [SerializeField] private Cube cubePref;
    [SerializeField] private Transform cubeParent;

    [SerializeField] private Cube[,,] cubeList;

    private int currentHorizontal = 0;
    private int currentVertical = 0;
    private int currentFloor = 0;

    private void Awake()
    {
        currentHorizontal = 0;
        currentVertical = 0;
        currentFloor = 0;
    }
    private void Start()
    {
        cubeList = new Cube[10,10,10]; 

        // floor -> vertical -> horizontal
        for (int i = 0; i < 3; i++) 
        {
            for (int j = 0; j < 3; j++) 
            {
                for (int k = 0; k < 3; k++) 
                {
                    cubeList[i, j, k] = cubeParent.Find($"{i + 1}/{j + 1}/{k + 1}").GetComponent<Cube>();
                }
            }
        }

        cubeList[currentFloor, currentVertical, currentHorizontal]?.SetVisit();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            cubeList[currentFloor, currentVertical, currentHorizontal].ResetVisit();

            currentHorizontal++;
            SetCube();
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            cubeList[currentFloor, currentVertical, currentHorizontal].ResetVisit();

            currentVertical--;
            SetCube();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            cubeList[currentFloor, currentVertical, currentHorizontal].ResetVisit();

            currentHorizontal--;
            SetCube();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            cubeList[currentFloor, currentVertical, currentHorizontal].ResetVisit();

            currentVertical++;
            SetCube();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cubeList[currentFloor, currentVertical, currentHorizontal].ResetVisit();

            currentFloor++;
            SetCube();
        }
        if(Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            cubeList[currentFloor, currentVertical, currentHorizontal].ResetVisit();

            currentFloor--;
            SetCube();
        }
    }

    private void SetCube()
    {
        currentHorizontal = Mathf.Clamp(currentHorizontal, 0, 2);
        currentVertical = Mathf.Clamp(currentVertical, 0, 2);
        currentFloor = Mathf.Clamp(currentFloor, 0, 2);

        cubeList[currentFloor, currentVertical, currentHorizontal]?.SetVisit();
        print($"{currentFloor}Ãþ, v : {currentVertical}, h : {currentHorizontal}");
    }
}
