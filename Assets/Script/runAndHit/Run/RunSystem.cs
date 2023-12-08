using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using GM;
using Mediapipe.Unity.Tutorial;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Wifi;

public class RunSystem : MonoBehaviour
{
    public GameObject[] UIs; //0,1 slider,2格數 ,3結束,, 1TCP
    public GameObject MP;
    GameObject Cam, Player;
    private bool setup,end;
    private Slider ball, people;
    private GameManager GM;
    private Animator PlayerAni;
    private int Hard;
    private HitSystem _hitSystem;
    //音效
    private AudioSource AS;
    public AudioClip AC;
    //
    private bool HisL, HisR;
    private float lenth;
    private int stage;

    private void Start()
    {
        //取得物件
        _hitSystem = gameObject.GetComponent<HitSystem>();
        GM = GameObject.Find("GM").GetComponent<GameManager>();
        Cam = GameObject.Find("Camera");
        Player = GameObject.Find("Player");
        MP.SetActive(false);
        setup = false;
        end = false;
        HisL = false;
        HisR = false;
        lenth = 0;
        //滑桿設定
        for (int i = 0; i < UIs.Length; i++)
        {
            UIs[i].SetActive(false);
        }

        people = UIs[0].GetComponent<Slider>();
        ball = UIs[1].GetComponent<Slider>();
        //難度設定
        Hard = GM.PlayerHard[GM.Playernow];
        //設定音效
        AS = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        bool hit = _hitSystem.Hit;
        if (!setup&&hit)
        {
            _setup();
            setup = true;
        }else if (hit &&setup)
        {
            if (ball.value + people.value > _hitSystem.Score)
            {
                end = true;
                GM.PlayerNowStage = stage;
                StartCoroutine(EndAnime());
            }
            else
            {
                PlayerMove();
                StartCoroutine(SliderMove());
                if (Player.transform.position.z > 1000*stage)
                {
                    stage += 1;
                    AS.PlayOneShot(AC);
                    UIs[2].GetComponent<TMP_Text>().SetText("格數:"+stage.ToString());
                }
            }
        }
        
    }

    void _setup()
    {
        AS.Stop();
        float score = _hitSystem.Score;
        //調整玩家和鏡頭位置
        Player.transform.Translate(2,0,0);
        Cam.transform.parent = Player.transform;
        //開啟滑桿
        for (int i = 0; i < UIs.Length-1; i++)
        {
            UIs[i].SetActive(true);
        }
        //滑桿最大值設定
        ball.maxValue = score;
        people.maxValue = score;
        ball.value = 0;
        people.value = 0;
        //抓animator
        PlayerAni = Player.GetComponentInChildren<Animator>();
        MP.SetActive(true);
        //把球棒變不見
        GameObject.Find("Baseball bat").SetActive(false);
    }

    IEnumerator SliderMove()
    {
        ball.value += 0.2f;
        people.value += (Hard+1)/10f;
        yield return new WaitForSeconds(0.1f);
    }

    void PlayerMove()
    {
        if (IsRun() &&lenth<14f)
        {
            lenth += 1.5f;
        }

        if (lenth>0f)
        {
            PlayerAni.SetBool("isrun",true);
            StartCoroutine(Run());
        }
        else
        {
            PlayerAni.SetBool("isrun",false);
        }
    }


    bool IsRun()
    {
        if (poseControl.RunR == false && HisR == true)
        {
            HisR = false;
            return true;
            
        }else if (poseControl.RunL == false && HisL == true)
        {
            HisL = false;
            return true;
        }
        else
        {
            HisL = poseControl.RunL;
            HisR = poseControl.RunR;
            return false;
        }
         
    }
    IEnumerator Run()
    {
        Player.transform.Translate(Vector3.forward*(1+lenth)*0.5f);
        lenth -= 0.1f;
        yield return new WaitForFixedUpdate();
    }

    IEnumerator EndAnime()
    {
        UIs[3].SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(4);
    }

}
