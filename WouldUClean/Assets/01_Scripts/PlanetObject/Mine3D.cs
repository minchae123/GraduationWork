using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine3D : MonoBehaviour
{
    private enum State
    {
        None,
        Warn,
        Emergency
    }

    public GameObject BombEffect;

    private State _state;

    private float distanceToPlayer;
    private float totalDistance;

    private float EmergencyRange = 2f;
    private float WarningRange = 8f;

    private float maxIntensity = 4;
    private float lightAlpha;

    private bool isLightOn;

    MeshRenderer[] MineRenderers;
    Light light;

    private void Start()
    {
        _state = State.None;
        MineRenderers = GetComponentsInChildren<MeshRenderer>();
        light = GetComponentInChildren<Light>();
    }

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, GameManager.Instance._playerTrm.position);
        totalDistance = WarningRange - EmergencyRange;

        if (distanceToPlayer < EmergencyRange)
        {
            SwitchState(State.Emergency);
            light.intensity = maxIntensity;
        }
        else if (distanceToPlayer < WarningRange)
        {
            SwitchState(State.Warn);
            LightBlink();
        }
        else { SwitchState(State.None); }
    }

    void SwitchState(State newState) // ������ �����Ϸ��� ��������� �ʿ������..
    {
        if (_state == newState)
            return;

        switch (newState)
        {
            case State.Emergency:
                print("���ڰ� �����ϰ� �Ҹ��� ������������");
                break;
            case State.Warn:
                break;
            case State.None:
                print("���ڰ� �Ⱥ��̰� ���� �� ���·�");
                break;
        }
        _state = newState;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("��������");
            Instantiate(BombEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }


    private void ChangeColor(float alpha)
    {
        foreach (MeshRenderer renderer in MineRenderers)
        {
            renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, alpha);
        }
    }

    private void LightBlink()
    {
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
}
