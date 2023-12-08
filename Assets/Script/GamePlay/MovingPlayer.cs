
using System;
using System.Collections;
using GM;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingPlayer : MonoBehaviour
{
    private GameManager GM;
    private GameObject Player;
    private ChessManManger _chessManManger;
    private bool moving,end;
    public float lenth = 11;
    public GameObject NextPlayer;
    private void Start()
    {
        NextPlayer.SetActive(false);
        moving = false;
        end = false;
        _chessManManger = GameObject.Find("Players").GetComponent<ChessManManger>();
        GM = GameObject.Find("GM").GetComponent<GameManager>();
        
    }

    private void Update()
    {
        if (GM.PlayerStage[GM.Playernow]<19)
        {
            if (GM.PlayerNowStage > 0&&!moving)
            {
                Player = _chessManManger.Players[GM.Playernow];
                StartCoroutine(Moving());
            }
            else if(GM.PlayerNowStage == 0 &&!moving && !end )
            {
                StartCoroutine(ani());
            }
        }
        else
        {
            SceneManager.LoadScene(6);
        }
    }
    

    IEnumerator Moving()
    {
        moving = true;
        yield return new WaitForSeconds(0.5f);
        Player.transform.localPosition = ChessManMovestep(GM.PlayerStage[GM.Playernow]+1);
        gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
        GM.PlayerStage[GM.Playernow] += 1;
        GM.PlayerNowStage -= 1;
        moving = false;

    }

    IEnumerator ani()
    {
        addPlayer();
        NextPlayer.GetComponent<TMP_Text>().text = "玩家" + (GM.Playernow+1).ToString() + "準備打擊";
        NextPlayer.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(3);
    }
    Vector3 ChessManMovestep(int i)
    {
        float x = 0;
        float z= 0;
        switch (i)
        {
            case <5:
                x = lenth * (i % 5);
                break;
            case <10:
                x = lenth * 5;
                z = lenth * (i % 5);
                break;
            case <15:
                x = (lenth * 5) - lenth * (i % 5);
                z = lenth * 5;
                break;
            case <20:
                z = (lenth * 5) - lenth * (i % 5);
                break;
        }
        return new Vector3(x, 0, z);

    }

    void addPlayer()
    {
        end = true;
        if (GM.Playernow == (GM.PlayerNum - 1))
        {
            GM.Playernow = 0;
        }
        else
        {
            GM.Playernow++;
        }
    }
    
}
