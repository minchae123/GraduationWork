using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePuzzle : MonoBehaviour
{
    public enum route
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

    [SerializeField] private Stack<route> routeStack;

    private void Awake()
    {
        routeStack = new Stack<route>();
        cubeList = new Cube[10, 10, 10];

        currentHorizontal = 0;
        currentVertical = 0;
        currentFloor = 0;
    }
    private void Start()
    {
        routeStack.Push(route.none);
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

        cubeList[currentFloor, currentVertical, currentHorizontal]?.SetVisit();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            SetStack(route.front, route.back);
            currentHorizontal++;
            SetCube();
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            SetStack(route.left, route.right);
            currentVertical--;
            SetCube();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetStack(route.back, route.front);
            currentHorizontal--;
            SetCube();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SetStack(route.right, route.left);
            currentVertical++;
            SetCube();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetStack(route.up, route.down);
            currentFloor++;
            SetCube();
        }
        if(Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            SetStack(route.down, route.up);
            currentFloor--;
            SetCube();
        }
    }

    private void SetStack(route currentR, route checkR)
    {
        route r = route.none;
        if (routeStack.TryPeek(out r))
        {
            if (r != checkR)
            {
                routeStack.Push(currentR); // 키 계쏙 눌리면 스택에 쌓여서... 이것만 막아주면 될듯 (범위 벗어날 경우에는 스택에 쌓이지 않도록)
                return;
            }

            routeStack.Pop();
            cubeList[currentFloor, currentVertical, currentHorizontal]?.ResetVisit();
        }
    }

    private void SetCube()
    {
        currentHorizontal = Mathf.Clamp(currentHorizontal, 0, 2);
        currentVertical = Mathf.Clamp(currentVertical, 0, 2);
        currentFloor = Mathf.Clamp(currentFloor, 0, 2);

        cubeList[currentFloor, currentVertical, currentHorizontal]?.SetVisit();
        //print($"{currentFloor}층, v : {currentVertical}, h : {currentHorizontal}");
        print($"{routeStack.Peek()}, {routeStack.Count}");
    }
}
