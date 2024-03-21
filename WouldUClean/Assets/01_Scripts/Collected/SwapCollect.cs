using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapCollect : MonoBehaviour
{
    [Header("¹öÆ°")]
    [SerializeField] private GameObject _planet;
    [SerializeField] private GameObject _trash;

    public void OnPlanetList()
    {
        _planet.SetActive(true);
        _trash.SetActive(false);

        CollectedPlanets.Instance.ShowContext(true);
    }

    public void OnTrashList()
    {
        _planet.SetActive(false);
        _trash.SetActive(true);

        CollectedPlanets.Instance.ShowContext(false);
    }
}
