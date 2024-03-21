using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Mine : MonoBehaviour
{
    private enum State { Emergency, Warning, Normal }
    private State currentState = State.Normal;

    private float EmergencyRange = 2f; //이걸 3으로 하지 않아도 계속 확인 할 때 3으로 되는데 어떻게 함?
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
        if (collision.CompareTag("Player"))
        {
            print("지뢰가 터졌습니다 뭘 할지 정해주세요!");
        }
    }
}
