using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenMap : MonoBehaviour
{
    ClickUiRect click;
    // Start is called before the first frame update
    void Start()
    {
        click = transform.Find("Compare").GetComponent<ClickUiRect>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
