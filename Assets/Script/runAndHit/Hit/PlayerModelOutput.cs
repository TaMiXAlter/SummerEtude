using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class PlayerModelOutput : MonoBehaviour
{
    GM.GameManager gameManager;
    public GameObject[] hittters;
    public GameObject player;
    private int PlayerNowCaractor;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GM").GetComponent<GM.GameManager>();
        
        switch (gameManager.PlayerCaractors[gameManager.Playernow]){
            case "bear":
                PlayerNowCaractor = 0;
                break;
            case "bigcat":
                PlayerNowCaractor = 1;
                break;
            case "cat":
                PlayerNowCaractor = 2;
                break;
            case "duck":
                PlayerNowCaractor = 3;
                break;
            case "giraff":
                PlayerNowCaractor = 4;
                break;
            case "panda":
                PlayerNowCaractor = 5;
                break;
        }

        player = Instantiate(hittters[PlayerNowCaractor],gameObject.transform);

    }

    
}
