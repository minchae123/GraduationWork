using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;


public class Natural : MonoBehaviour
{
    PlayerController player;

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
    }

    public void NaturalDamage(PlanetInSpace planet)
    {
        if (planet.rainPlanet)
            Rain();
        if (planet.snowPlanet)
            Snow();
        if (planet.windPlanet)
            Wind();
    }

    private void Rain()
    {
        player._speed *= player.SlowPercent;
    }

    private void Snow()
    {
        //�� �����(������ �𸣰���)
    }

    private void Wind()
    {
        //Vector3 leftDirection = Vector3.Cross(transform.up, transform.forward);
        player._windDir = Vector3.left.normalized / 2;
    }
}
