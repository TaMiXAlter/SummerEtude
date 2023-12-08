using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class RestApi : MonoBehaviour
{
    public string URl;
    static public bool GameStart;
    private void Start()
    {
        StartCoroutine(getdatas());
    }

    IEnumerator getdatas(){
        using(UnityWebRequest request = UnityWebRequest.Get(URl)){
            yield return request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.ConnectionError){
                Debug.LogError(request.error);
            }
            else{
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);
                
                if ( stats[0].ToString() == "true")
                {
                    GameStart = true;
                }
                else
                {
                    GameStart = false;
                }
                Debug.Log(GameStart);
            }
            yield return new WaitForSeconds(1f);
            yield return getdatas();
        }

    }
}