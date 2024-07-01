using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Rendering;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEditor.PlayerSettings;

public enum PlayerDir
{
    left,
    right
}

public class Movement : MonoBehaviour
{
    private CameraMovement camMovement;
    private GimmickExplain gimmick;

    private RaycastHit hit;
    private Ray[] ray = new Ray[6];

    [SerializeField] private LayerMask whatIsBox;

    public int curCount;
    public int TotalCount => totalCount;
    private int totalCount;

    private OriginColorEnum playerColor;
    public OriginColorEnum PlayerColor => playerColor;
    public PlayerDir playerEnum;

    public Vector3 direction { get; set; }

    [SerializeField] private bool[] isCanMove = new bool[6];

    public MeshRenderer render;
    private bool isWaitMove = false;

    private void Awake()
    {
        camMovement = FindObjectOfType<CameraMovement>();
        gimmick = FindObjectOfType<GimmickExplain>();

        curCount = 0;
        totalCount = 0;

        RayCheck();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            MoveLeft();
        }
        if (FindObjectOfType<StageManager>() && StageManager.Instance.IsInStage) BoxManager.Instance.boxDec(transform);
        else if (FindObjectOfType<TutorialStageManager>() && TutorialStageManager.Instance.IsInStage) BoxManager.Instance.boxDec(transform);

    }

    public void SetPlayer(OriginColorEnum color, int moveCnt)
    {
        Color c = GameManager.Instance.FindColor(color);

        render = GetComponent<MeshRenderer>();
        render.sharedMaterial.SetColor("_PlayerColor", c);

        playerColor = color;
        totalCount = moveCnt;
    }
    public void SetPlayerColor(OriginColorEnum color)
    {
        Color c = GameManager.Instance.FindColor(color);
        render.sharedMaterial.SetColor("_PlayerColor", c);

        playerColor = color;
    }

    public void MoveLeft()
    {
        RayCheck();

        direction = (-camMovement.curTransfrom.transform.right);
        BoxManager.Instance.boxDec(transform);

        if (isCanMove[2] && curCount < totalCount && direction != Vector3.zero)
        {
            Move(direction);
        }
    }

    public void MoveRight()
    {
        RayCheck();

        direction = (camMovement.curTransfrom.transform.right);
        BoxManager.Instance.boxDec(transform);

        if (isCanMove[3] && curCount < totalCount && direction != Vector3.zero)
        {
            Move(direction);
        }
    }

    public void MoveUp()
    {
        RayCheck();

        direction = (camMovement.curTransfrom.transform.up);
        BoxManager.Instance.boxDec(transform);

        if (isCanMove[0] && curCount < totalCount && direction != Vector3.zero)
        {
            Move(direction);
        }
    }

    public void MoveDown()
    {
        RayCheck();

        direction = (-camMovement.curTransfrom.transform.up);
        BoxManager.Instance.boxDec(transform);

        if (isCanMove[1] && curCount < totalCount && direction != Vector3.zero)
        {
            Move(direction);
        }
    }

    private int Round(float f)
    {
        return Mathf.RoundToInt(f);
    }

    public void Move(Vector3 dir)
    {
        if (isWaitMove) return;

        if (gimmick.panel.isWait)
            gimmick.panel.CloseTutorial();

        Vector3 pos = new Vector3(Round(dir.x + transform.localPosition.x), Round(dir.y + transform.localPosition.y), Round(dir.z + transform.localPosition.z));
        StartCoroutine(MoveCoroutine(pos));
        //transform.DOLocalMove(pos, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => print("�� �ȿ����δ�")); //Dotween�� �� ���⼭ �ȵǴ��� ã��
        
        direction = Vector3.zero;

        curCount++;
    }

    private IEnumerator MoveCoroutine(Vector3 targetPos)
    {
        Vector3 startPos = transform.localPosition;
        Vector3 overshootPos = targetPos + (targetPos - startPos) * 0.1f; // ��ǥ�������� 20% �� �� ����
        Vector3 currentVelocity = Vector3.zero;

        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = initialScale; // ũ�⸦ 50%�� ���̱�

        float distance = Vector3.Distance(startPos, targetPos);
        float duration = Mathf.Clamp(distance * 0.5f, 0.05f, 0.1f); // �Ÿ� ��� �ð�, �ּ� 0.1��, �ִ� 1��
        float smoothTime = duration * 0.5f; // ó�� �̵��� 80% �ð� ���
        float elapsedTime = 0f;

        bool overshootReached = false;

        isWaitMove = true;

        while (!overshootReached || Vector3.Distance(transform.localPosition, targetPos) >= 0.01f)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            if (!overshootReached)
            {
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition, overshootPos, ref currentVelocity, smoothTime);

                // ���⿡ ���� ũ�⸦ ����
                float deltaX = Mathf.Abs(targetPos.x - startPos.x);
                float deltaY = Mathf.Abs(targetPos.y - startPos.y);
                float deltaZ = Mathf.Abs(targetPos.z - startPos.z);

                if (deltaX > deltaY && deltaX > deltaZ)
                {
                    // x������ �̵��� ��
                    targetScale.x = initialScale.x * 0.75f;
                    targetScale.y = initialScale.y;
                    targetScale.z = initialScale.z;
                }
                else if (deltaY > deltaX && deltaY > deltaZ)
                {
                    // y������ �̵��� ��
                    targetScale.x = initialScale.x;
                    targetScale.y = initialScale.y * 0.75f;
                    targetScale.z = initialScale.z;
                }
                else
                {
                    // z������ �̵��� ��
                    targetScale.x = initialScale.x;
                    targetScale.y = initialScale.y;
                    targetScale.z = initialScale.z * 0.75f;
                }

                transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

                if (Vector3.Distance(transform.localPosition, overshootPos) < 0.01f)
                {
                    overshootReached = true;
                    elapsedTime = 0f; // ��ǥ�������� �̵��� ���� ��� �ð� �ʱ�ȭ
                    currentVelocity = Vector3.zero; // �ӵ� �ʱ�ȭ
                }
            }
            else
            {
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPos, ref currentVelocity, smoothTime);

                // ũ�⸦ �����ϱ�
                transform.localScale = Vector3.Lerp(targetScale, initialScale, t);
            }

            yield return null;
        }

        isWaitMove = false;
        transform.localScale = initialScale;
        transform.localPosition = targetPos;
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