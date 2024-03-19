using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidden : MonoBehaviour
{
    [SerializeField] private HiddenSO hiddenSO;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Assignment(Sprite sprite, int idx)
    {

    }

    private void GetChildren(RectTransform parent)
    {
        RectTransform rect;
        foreach (GameObject item in parent)
        {
            rect = item.GetComponent<RectTransform>();
        }

    }
}
