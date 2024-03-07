using UnityEngine;

public class Mine : MonoBehaviour
{
    private enum State { Emergency, Warning, Normal }
    private State currentState = State.Normal;

    public float EmergencyRange = 3f;
    public float WarningRange = 6f;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, GameManager.Instance._playerTrm.position);
        float totalDistance = WarningRange - EmergencyRange;

        if (distanceToPlayer < EmergencyRange)
        {
            //SwitchState(State.Emergency);
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
        else if (distanceToPlayer < WarningRange)
        {
            //SwitchState(State.Warning);
            spriteRenderer.color = new Color(1, 1, 1, 1 - ((distanceToPlayer - EmergencyRange) / totalDistance));
            print(1 - ((distanceToPlayer - EmergencyRange) / totalDistance));
        }
        else
        {
            //SwitchState(State.Normal);
            spriteRenderer.color = new Color(1, 1, 1, 0);
        }
    }

    //�̰��� �Ƹ� ������� ���� ���Դϴ�.
    void SwitchState(State newState)
    {
        if (currentState == newState)
            return;

        switch (newState)
        {
            case State.Emergency:
                print("���ڰ� �����ϰ� �Ҹ��� ������������");
                spriteRenderer.color = new Color(1, 1, 1, 1);
                break;
            case State.Warning:
                print("���ڰ� ������ ������ ����������  �Ҹ��� �� �� ��");
                spriteRenderer.color = new Color(1, 1, 1, 0.5f);
                break;
            case State.Normal:
                print("���ڰ� �Ⱥ��̰� ���� �� ���·�");
                spriteRenderer.color = new Color(1, 1, 1, 0);
                break;
        }

        currentState = newState;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            print("���ڰ� �������ϴ� �� ���� �����ּ���!");
        }
    }
}
