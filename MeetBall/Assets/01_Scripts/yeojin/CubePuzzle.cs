using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePuzzle : MonoBehaviour
{
    public enum Route
    {
        none = 0,
        left = 1,
        right = 2,
        up = 3,
        down = 4,
        front = 5,
        back = 6
    }

    [SerializeField] private Cube cubePref;
    [SerializeField] private Transform cubeParent;

    [SerializeField] private Cube[,,] cubeList;

    private int currentHorizontal = 0;
    private int currentVertical = 0;
    private int currentFloor = 0;

    [SerializeField] private Stack<Route> routeStack;

    private void Awake()
    {
        routeStack = new Stack<Route>();
        cubeList = new Cube[10, 10, 10];

        currentHorizontal = 0;
        currentVertical = 0;
        currentFloor = 0;
    }
    private void Start()
    {
        routeStack.Push(Route.none);
        print(cubeParent.childCount);

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

        cubeList[currentFloor, currentVertical, currentHorizontal]?.SetStart();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            SetStack(Route.front, Route.back);
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            SetStack(Route.left, Route.right);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetStack(Route.back, Route.front);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SetStack(Route.right, Route.left);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetStack(Route.up, Route.down);
        }
        if(Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            SetStack(Route.down, Route.up);
        }
    }

    private void SetStack(Route currentR, Route checkR)
    {
        Route r = Route.none;
        bool IsPushed = false;
        if (routeStack.TryPeek(out r))
        {
            if (r != checkR)
            {
                IsPushed = true;
                routeStack.Push(currentR);
            }
            else
            {
                routeStack.Pop();
                cubeList[currentFloor, currentVertical, currentHorizontal]?.ResetVisit();
            }
        }

        CheckIdx(currentR, IsPushed);
    }

    private void CheckIdx(Route currentR, bool IsPushed)
    {
        PorM(currentR, 1);

        if (currentHorizontal > 2 || currentHorizontal < 0 || currentFloor > 2 || currentFloor < 0 || currentVertical > 2 || currentVertical < 0) 
        {
            if (IsPushed)
            {
                routeStack.Pop();
            }
        }
        else if(cubeList[currentFloor, currentVertical, currentHorizontal].Visit)
        {
            if (IsPushed)
            {
                PorM(currentR, -1);
                routeStack.Pop();
            }
        }
        else
        {
            SetCube();
        }

        currentHorizontal = Mathf.Clamp(currentHorizontal, 0, 2);
        currentVertical = Mathf.Clamp(currentVertical, 0, 2);
        currentFloor = Mathf.Clamp(currentFloor, 0, 2);
    }

    private void PorM(Route currentR, int checkI)
    {

        switch (currentR)
        {
            case Route.none:
                break;
            case Route.left:
                currentVertical-= checkI;
                break;
            case Route.right:
                currentVertical+= checkI;
                break;
            case Route.up:
                currentFloor+= checkI;
                break;
            case Route.down:
                currentFloor-= checkI;
                break;
            case Route.front:
                currentHorizontal+= checkI;
                break;
            case Route.back:
                currentHorizontal-= checkI;
                break;
        }
    }

    private void SetCube()
    {
        cubeList[currentFloor, currentVertical, currentHorizontal]?.SetVisit();
        //print($"{currentFloor}Ãþ, v : {currentVertical}, h : {currentHorizontal}");
        print($"{routeStack.Peek()}, {routeStack.Count}");
    }
}
