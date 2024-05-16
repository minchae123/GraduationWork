using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Rendering;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerDir
{
    left,
    right
}

struct WASD
{
    public Vector3 w;
    public Vector3 s;
    public Vector3 a;
    public Vector3 d;
}

public class Movement : MonoBehaviour
{
    private CameraMovement camMovement;

    private RaycastHit hit;
    private Ray[] ray = new Ray[6];

    [SerializeField] private WASD WASD;
    [SerializeField] private LayerMask whatIsBox;
    [SerializeField] private StageSO stageInfo;

    public int curCount;
    public int moveCount;
    public PlayerDir playerEnum;

    public Vector3 direction { get; set; }

    [SerializeField] private bool[] isCanMove = new bool[6];

    public MeshRenderer render;

    private void Awake()
    {
        //render = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        camMovement = FindObjectOfType<CameraMovement>();

        RayCheck();
    }

    private void Update()
    {
        BoxManager.Instance.boxDec(transform);

        RayCheck();

        //PrintDir();
    }

    public void PrintDir()
    {
        print("x+ " + camMovement.curTransfrom.transform.right);
        print("x- " + -camMovement.curTransfrom.transform.right);
        print("y+ " + camMovement.curTransfrom.transform.up);
        print("y- " + -camMovement.curTransfrom.transform.up);
        print("z+ " + camMovement.curTransfrom.transform.forward);
        print("z- " + camMovement.curTransfrom.transform.forward);
    }


    public void SetPlayer(Color color, int moveCnt)
    {
        render = GetComponent<MeshRenderer>();
        render.sharedMaterial.SetColor("_PlayerColor", color);
        moveCount = moveCnt;
    }

    public void MoveLeft()
    {
        RayCheck();

        direction = (-camMovement.curTransfrom.transform.right);
        BoxManager.Instance.boxDec(transform);

        if (isCanMove[2] && curCount < moveCount && direction != Vector3.zero)
        {
            print(direction);
            transform.position += direction;

            curCount++;
        }
    }

    public void MoveRight()
    {
        RayCheck();

        direction = (camMovement.curTransfrom.transform.right);
        BoxManager.Instance.boxDec(transform);

        if (isCanMove[3] && curCount < moveCount && direction != Vector3.zero)
        {
            print(direction);
            transform.position += direction;

            curCount++;
        }
    }

    public void MoveUp()
    {
        RayCheck();

        direction = Vector3Int.RoundToInt(camMovement.curTransfrom.transform.up);
        BoxManager.Instance.boxDec(transform);

        if (isCanMove[4] && curCount < moveCount && direction != Vector3.zero)
        {
            print(direction);
            transform.position += direction;

            curCount++;
        }
    }

    public void MoveDown()
    {
        RayCheck();

        direction = Vector3Int.RoundToInt(-camMovement.curTransfrom.transform.up);
        BoxManager.Instance.boxDec(transform);

        if (isCanMove[5] && curCount < moveCount && direction != Vector3.zero)
        {
            print(direction);
            transform.position += direction;

            curCount++;
        }
    }

    public void RayCheck()
    {
        ray[2].direction = -camMovement.curTransfrom.transform.right; // x left
        ray[3].direction = camMovement.curTransfrom.transform.right; // x right
        ray[4].direction = camMovement.curTransfrom.transform.forward; // z up
        ray[5].direction = -camMovement.curTransfrom.transform.forward; // z down

        for (int i = 0; i < ray.Length; i++)
        {
            ray[i].origin = transform.position;

            Debug.DrawRay(ray[i].origin, ray[i].direction);

            if (camMovement._dir == Direction.Down || camMovement._dir == Direction.Up)
            {
                ray[4].direction = -camMovement.curTransfrom.transform.forward; // z up
                ray[5].direction = camMovement.curTransfrom.transform.forward; // z down
            }
            else
            {
                ray[4].direction = camMovement.curTransfrom.transform.up; // z up
                ray[5].direction = -camMovement.curTransfrom.transform.up; // z down
            }

            if (Physics.Raycast(ray[i], out hit, 0.5f, whatIsBox))
            {
                Debug.DrawRay(ray[i].origin, ray[i].direction, Color.red);
                isCanMove[i] = true;
            }
            else
            {
                isCanMove[i] = false;
            }
        }
    }
}