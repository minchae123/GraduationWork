using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Mine : MonoBehaviour
{
    private enum State { Emergency, Warning, Normal }
    private State currentState = State.Normal;

    private float EmergencyRange = 2f; //�̰� 3���� ���� �ʾƵ� ��� Ȯ�� �� �� 3���� �Ǵµ� ��� ��?
    private float WarningRange = 8f;

    private float maxIntensity = 3;

    private SpriteRenderer spriteRenderer;
    private Light2D light;
    private float lightAlpha;
    private bool isLightOn;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        light = GetComponentInChildren<Light2D>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, GameManager.Instance._playerTrm.position);
        float totalDistance = WarningRange - EmergencyRange;



        if (distanceToPlayer < EmergencyRange)
        {
            //SwitchState(State.Emergency);
            spriteRenderer.color = new Color(1, 1, 1, 1);
            //print(distanceToPlayer);
            light.color = new Color(light.color.r, light.color.g, light.color.b, 1);
        }
        else if (distanceToPlayer < WarningRange)
        {
            //SwitchState(State.Warning);
            spriteRenderer.color = new Color(1, 1, 1, 1 - ((distanceToPlayer - EmergencyRange) / totalDistance));
            light.intensity = Mathf.Clamp(maxIntensity - ((distanceToPlayer - EmergencyRange) / totalDistance * maxIntensity), 0, maxIntensity);
            light.color = new Color(light.color.r, light.color.g, light.color.b, lightAlpha);

            if (lightAlpha < 0)
            {
                lightAlpha += Time.deltaTime;
                isLightOn = true;
            }
            else if (lightAlpha > 1)
            {
                lightAlpha -= Time.deltaTime;
                isLightOn = false;
            }
            else
            {
                if (isLightOn)
                {
                    lightAlpha += Time.deltaTime;
                }
                else
                {
                    lightAlpha -= Time.deltaTime;
                }
            }
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
        if (collision.CompareTag("Player"))
        {
            print("���ڰ� �������ϴ� �� ���� �����ּ���!");
        }
    }
}
