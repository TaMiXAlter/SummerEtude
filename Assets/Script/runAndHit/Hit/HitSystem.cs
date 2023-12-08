using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wifi;



public class HitSystem : MonoBehaviour
{
    public GameObject[] UIs;
    public GameObject Pitcher,Player,ball;
    Animator pitherAni, playerAni;
    private tcp2 tcpserver;
    public AudioClip ACStrike;
    public bool Hit;
    public float Score;

    private int strikeCount;
    
    private void Start()
    {
        for (int i = 0; i < UIs.Length; i++)
        {
            UIs[i].SetActive(false);
        }
        
    }

    void Update()
    {
        if (BoxDitect.InCollider)
        {
            tcpserver = GameObject.Find("TCPServer").GetComponent<tcp2>();
            tcpserver.clearHandValue();
            BoxDitect.InCollider = false;
        }
        if (BoxDitect.Outcollider)
        {
            if (tcpserver.HanadValue == 0f )
            {
                StartCoroutine(Strike());
            }
            else
            {
                Score = tcpserver.HanadValue;
                Pitcher.SetActive(false);
                
                StartCoroutine(showAni());
              
            }

            BoxDitect.Outcollider = false;
        }
        

    }
    IEnumerator Strike()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(ACStrike);
        UIs[0].SetActive(true);
        yield return new WaitForSeconds(1.5f);
        UIs[0].SetActive(false);
        pitherAni = Pitcher.GetComponentInChildren<Animator>();
        playerAni = Player.GetComponentInChildren<Animator>();
        playerAni.SetTrigger("strike");
        pitherAni.SetTrigger("strike");
        yield return new WaitForSeconds(4f);
        ball.GetComponent<BallFly>().GO();
        
    }

    IEnumerator showAni()
    {
        UIs[1].SetActive(true);
        yield return new WaitForSeconds(1f);
        playerAni = Player.GetComponentInChildren<Animator>();
        playerAni.SetTrigger("hit");
        UIs[1].SetActive(false);
        Hit = true;
        Score = tcpserver.HanadValue;
        UIs[2].SetActive(true);
        yield return new WaitForSeconds(1f);
        UIs[2].SetActive(false);
        
    }
}
