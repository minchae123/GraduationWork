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

    public int curCount;
    private int totalCount;
    public int TotalCount => totalCount;

    public PlayerDir playerEnum;

    public Vector3 direction { get; set; }

    [SerializeField] private bool[] isCanMove = new bool[6];

    public MeshRenderer render;

    private void Awake()
    {
        camMovement = FindObjectOfType<CameraMovement>();
        
        curCount = 0;
        totalCount = 0;

        RayCheck();
    }

    private void Update()
    {
        BoxManager.Instance.boxDec(transform);
        //print(totalCount);
        RayCheck();
    }

    public void SetPlayer(Color color, int moveCnt)
    {
        render = GetComponent<MeshRenderer>();
        render.sharedMaterial.SetColor("_PlayerColor", color);
        totalCount = moveCnt;

        print($"MOVEMENT SETPLAYER -> {this.gameObject.name} : t {totalCount}, T {TotalCount}");
    }

    public void MoveLeft()
    {
        RayCheck();

        direction = (-camMovement.curTransfrom.transform.right);
        BoxManager.Instance.boxDec(transform);

        if (isCanMove[2] && curCount < totalCount && direction != Vector3.zero)
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

        if (isCanMove[3] && curCount < totalCount && direction != Vector3.zero)
        {
            transform.position += direction;

            curCount++;
        }
    }

    public void MoveUp()
    {
        RayCheck();

        direction = (camMovement.curTransfrom.transform.up);
        BoxManager.Instance.boxDec(transform);

        if (isCanMove[0] && curCount < totalCount && direction != Vector3.zero)
        {
            transform.position += direction;

            curCount++;
        }
    }

    public void MoveDown()
    {
        RayCheck();

        direction = (-camMovement.curTransfrom.transform.up);
        BoxManager.Instance.boxDec(transform);

        if (isCanMove[1] && curCount < totalCount && direction != Vector3.zero)
        {
            print(direction);
            transform.position += direction;

            curCount++;
        }
    }

    public void RayCheck()
    {
		ray[0].direction = camMovement.curTransfrom.transform.up; // y up
		ray[1].direction = -camMovement.curTransfrom.transform.up; // y down
		ray[2].direction = -camMovement.curTransfrom.transform.right; // x left
        ray[3].direction = camMovement.curTransfrom.transform.right; // x right
        ray[4].direction = camMovement.curTransfrom.transform.forward; // z up
        ray[5].direction = -camMovement.curTransfrom.transform.forward; // z down

        for (int i = 0; i < ray.Length; i++)
        {
            ray[i].origin = transform.position;

            Debug.DrawRay(ray[i].origin, ray[i].direction);

            //if (camMovement._dir == Direction.Down || camMovement._dir == Direction.Up)
            //{
            //    ray[4].direction = -camMovement.curTransfrom.transform.forward; // z up
            //    ray[5].direction = camMovement.curTransfrom.transform.forward; // z down
            //}
            //else
            //{
            //    ray[4].direction = camMovement.curTransfrom.transform.up; // z up
            //    ray[5].direction = -camMovement.curTransfrom.transform.up; // z down
            //}

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