using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour, Item
{
    public void Rotation(bool value)
    {
        Vector3 rotation = CameraMovement.Instance.curTransfrom.transform.position.normalized;
        RoundVector(ref rotation);
        //print(rotation);
        transform.rotation = Quaternion.Euler(rotation);
    }

    private void RoundVector(ref Vector3 rot)
    {
        rot.x = Mathf.Round(rot.x) * 90;
        rot.y = Mathf.Round(rot.y) * 90;
        rot.z = Mathf.Round(rot.z) * 90;
    }
}
