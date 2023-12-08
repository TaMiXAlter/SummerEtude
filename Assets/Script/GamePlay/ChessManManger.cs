using System;
using System.Collections;
using System.Collections.Generic;
using GM;
using UnityEngine;

public class ChessManManger : MonoBehaviour
{
    private GameManager _gameManager;
    public Material[] Materials;
    public List<GameObject> Players ;
    public GameObject CheseMan;
    public float lenth;
    void Start()
    {
        _gameManager = GameObject.Find("GM").GetComponent<GameManager>();
        for (int i = 0; i < _gameManager.PlayerNum; i++)
        {
            Players.Add(Instantiate(CheseMan,gameObject.transform));
            Players[i].AddComponent<LookAtCam>();
            Players[i].transform.localPosition = ChessManSetup(_gameManager.PlayerStage[i]);
            Players[i].GetComponentInChildren<Renderer>().material = Materials[PlayerMati(_gameManager.PlayerCaractors[i])];
        }
    }

    Vector3 ChessManSetup(int i)
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

    int PlayerMati(string PN)
    {
        int i = -1;
        switch (PN){
            case "bear":
                i=  0;
                break;
            case "bigcat":
                i=  1;
                break;
            case "cat":
                i=  2;
                break;
            case "duck":
                i=  3;
                break;
            case "giraff":
                i=  4;
                break;
            case "panda":
                i=  5;
                break;
        }

        return i;
    }
}
