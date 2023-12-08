
using System;
using UnityEngine;

public class LookAtCam : MonoBehaviour
{
    private void Start()
    {
        GameObject Cam = GameObject.Find("Camera");
        gameObject.transform.LookAt(Cam.transform);
        gameObject.transform.Rotate(new Vector3(0,0,90f));
    }
}
