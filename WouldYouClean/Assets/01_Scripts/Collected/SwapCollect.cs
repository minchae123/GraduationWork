using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapCollect : MonoBehaviour
{
    [SerializeField] GameObject _planet;
    [SerializeField] GameObject _trash;

    public void OnPlanetList()
    {
        _planet.SetActive(true);
        _trash.SetActive(false);
    }

    public void OnTrashList()
    {
        _planet.SetActive(false);
        _trash.SetActive(true);
    }
}
