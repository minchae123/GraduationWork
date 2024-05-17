using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = transform.position + CameraMovement.Instance.curTransfrom.forward;
        Debug.Log(dir);
        transform.LookAt(dir);
    }
}
