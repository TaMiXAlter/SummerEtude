using System.Collections;
using System.Collections.Generic;
using GM;
using TMPro;
using UnityEngine;

public class HardShow : MonoBehaviour
{
    // Start is called before the first frame update    
    public GameObject Fire;
    void Start()
    {
        GameManager GM = GameObject.Find("GM").GetComponent<GameManager>();
        int Hard = GM.PlayerHard[GM.Playernow];
        gameObject.GetComponent<TMP_Text>().text = "玩家"+ (GM.Playernow+1).ToString()+"難度:";
        for (int i = 0; i < Hard+1; i++)
        {
            GameObject FireNOW = Instantiate(Fire, gameObject.transform);
            FireNOW.transform.localPosition =new Vector3 (100 + (i * 50), 0, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
