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

    //이것은 아마 사용하지 않을 것입니다.
    void SwitchState(State newState)
    {
        if (currentState == newState)
            return;

        switch (newState)
        {
            case State.Emergency:
                print("지뢰가 선명하고 소리는 삐이이이이이");
                spriteRenderer.color = new Color(1, 1, 1, 1);
                break;
            case State.Warning:
                print("지뢰가 가까이 갈수록 선명해지고  소리는 삐 삐 삐");
                spriteRenderer.color = new Color(1, 1, 1, 0.5f);
                break;
            case State.Normal:
                print("지뢰가 안보이고 모든게 원 상태로");
                spriteRenderer.color = new Color(1, 1, 1, 0);
                break;
        }

        currentState = newState;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            print("지뢰가 터졌습니다 뭘 할지 정해주세요!");
        }
    }
}
