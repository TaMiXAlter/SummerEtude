using System.Collections;
using System.Collections.Generic;
using GM;
using Mediapipe.Unity.Tutorial;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunCon : MonoBehaviour
{
    private GameManager _gameManager;
    public GameObject text,mask;
    private float loadNum;
    void Start()
    {
        _gameManager = GameObject.Find("GM").GetComponent<GameManager>();
        text.GetComponent<TMP_Text>().text = "玩家" + "\n在頭頂打圈表示準備完成";
        text.SetActive(true);
        loadNum = 0;
       
    }

    // Update is called once per frame
    void Update()
    {
        mask.transform.localPosition = new Vector3((-350f+loadNum),0f,0f);
        if (loadNum <450f && poseControl.circle)
        {
            
            loadNum += 4f;
        }else if (poseControl.circle)
        {
            text.SetActive(false);
            SceneManager.LoadScene(5);
        }
        else
        {
            loadNum = 0;
        }
    }
}
