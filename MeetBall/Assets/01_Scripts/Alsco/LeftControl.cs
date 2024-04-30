using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Rendering;
using UnityEngine;
using UnityEngine.Rendering.LookDev;

struct WASD
{
    public Vector3 w;
    public Vector3 s;
    public Vector3 a;
    public Vector3 d;
}

public class LeftControl : MonoBehaviour
{
    private WASD WASD;
    private RaycastHit hit;
    private Ray[] ray = new Ray[6];

    [SerializeField] private LayerMask whatIsBox;
    [SerializeField] private StageSO stageinfo;

    private int curCount;
    private int maxCount;
    private Vector3 startPos;
    public Vector3 direction;

    private bool[] isCanMove = new bool[6];

    private void Start()
    {
        //상하좌우앞뒤
        ray[0].direction = transform.up;
        ray[1].direction = -transform.up;
        ray[2].direction = -transform.right;
        ray[3].direction = transform.right;
        ray[4].direction = transform.forward;
        ray[5].direction = -transform.forward;

        startPos = transform.position;

        curCount = -1;
        maxCount = stageinfo.LmoveCnt;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            RayCheck();
        }

        if (curCount < maxCount)
        {
            direction = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.W) && isCanMove[4])
            {
                direction = WASD.w;
            }
            if (Input.GetKeyDown(KeyCode.S) && isCanMove[5])
            {
                direction = WASD.s;
            }
            if (Input.GetKeyDown(KeyCode.D) && isCanMove[3])
            {
                direction = WASD.d;
            }
            if (Input.GetKeyDown(KeyCode.A) && isCanMove[2])
            {
                direction = WASD.a;
            }
            if (Input.GetKeyDown(KeyCode.Space) && isCanMove[0])
            {
                direction = Vector3.up;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && isCanMove[1])
            {
                direction = Vector3.down;
            }

            BoxManager.Instance.boxDec(transform);

            transform.position += direction;
        }
    }

    public void RayCheck()
    {
        for (int i = 0; i < ray.Length; i++)
        {
            ray[i].origin = transform.position;
            Debug.DrawRay(ray[i].origin, ray[i].direction);

            if (Physics.Raycast(ray[i], out hit, 0.5f, whatIsBox))
            {
                if (hit.collider.TryGetComponent<MapCube>(out MapCube m))
                {
                    if (m.isVisit) // 방문을 한 곳인데
                    {
                        if (mapVisited.TryPeek(out MapCube checkM))
                        {
                            if (m == checkM) // 전에 바로 왔던 곳일 경우
                            {
                                isCanMove[i] = true; // 가능
                            }
                            else
                            {
                                isCanMove[i] = false; // 불가능
                            }
                        }
                    }
                    else // 방문 안 한 곳이면
                    {
                        isCanMove[i] = true; // 가능
                    }
                }
                else
                {
                    isCanMove[i] = true; // 가능
                }
            }
            else
            {
                isCanMove[i] = false;
            }
        }
    }

    public void Move(DIRECTION dir)
    {
        switch (dir)
        {
            case DIRECTION.East:
                {
                    WASD.w = -Vector3.right;
                    WASD.s = Vector3.right;
                    WASD.a = -Vector3.forward;
                    WASD.d = Vector3.forward;

                    ray[2].direction = -transform.forward;
                    ray[3].direction = transform.forward;
                    ray[4].direction = -transform.right;
                    ray[5].direction = transform.right;
                }
                break;
            case DIRECTION.West:
                {
                    WASD.w = Vector3.right;
                    WASD.s = -Vector3.right;
                    WASD.a = Vector3.forward;
                    WASD.d = -Vector3.forward;

                    ray[2].direction = transform.forward;
                    ray[3].direction = -transform.forward;
                    ray[4].direction = transform.right;
                    ray[5].direction = -transform.right;
                }
                break;
            case DIRECTION.South:
                {
                    WASD.w = Vector3.forward;
                    WASD.s = -Vector3.forward;
                    WASD.a = -Vector3.right;
                    WASD.d = Vector3.right;

                    ray[2].direction = -transform.right;
                    ray[3].direction = transform.right;
                    ray[4].direction = transform.forward;
                    ray[5].direction = -transform.forward;
                }
                break;
            case DIRECTION.North:
                {
                    WASD.w = -Vector3.forward;
                    WASD.s = Vector3.forward;
                    WASD.a = Vector3.right;
                    WASD.d = -Vector3.right;

                    ray[2].direction = transform.right;
                    ray[3].direction = -transform.right;
                    ray[4].direction = -transform.forward;
                    ray[5].direction = transform.forward;
                }
                break;
        }
    }

    public MapCube beforeCube;
    private Stack<MapCube> mapVisited = new Stack<MapCube>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Moveable")) // 
        {
            if (other.TryGetComponent<MapCube>(out MapCube m))
            {
                if (m.isVisit)
                {
                    if (mapVisited.TryPeek(out MapCube checkM))
                    {
                        if (m == checkM)
                        {
                            mapVisited.Pop();
                            checkM.CancelVisit();

                            beforeCube = m;

                            curCount--;
                        }
                    }
                }
                else
                {
                    if (beforeCube != null && beforeCube != m)
                    {
                        mapVisited.Push(beforeCube);
                        beforeCube.SetVisit();
                    }
                    beforeCube = m;
                    curCount++;
                }
            }
            else
            {
                curCount++;
            }
        }
    }
}
