using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDitect : MonoBehaviour
{
    static public bool InCollider, Outcollider;

    private void Start()
    {
        InCollider = false;
        Outcollider = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        InCollider = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Outcollider = true;
    }
    
}
