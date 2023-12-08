using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallRestAPi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RestApi restApi = GameObject.Find("APIsystem").GetComponent<RestApi>();
        StartCoroutine(restApi.putdatas("1"));
    }
}
