using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControl : MonoBehaviour
{
    private Animator[] anis;

    private bool HisGameStart;
    // Start is called before the first frame update
    void Start()
    {
        anis = GetComponentsInChildren<Animator>();
        HisGameStart = false;
    }

    private void Update()
    {
        if (!HisGameStart && RestApi.GameStart )
        {
            SetAni(true);
            HisGameStart = true;
        }
        else if(HisGameStart && !RestApi.GameStart )
        {
            SetAni(false);
            HisGameStart = false;
        }
    }

    private void SetAni(bool which)
    {
        if (which)
        {
            for (int i = 0; i < anis.Length; i++)
            {
                anis[i].SetTrigger("Move");
            }
        }else 
        {
            for (int i = 0; i < anis.Length; i++)
            {
                anis[i].SetTrigger("Back");
            }
        }
        
        
    }
}
