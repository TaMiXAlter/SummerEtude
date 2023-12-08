using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class RestApi : MonoBehaviour
{
    public  string URl= "http://127.0.0.1:5000/gamedata/1";

    public void putData(string num)
    {
        StartCoroutine(putdatas(num));
    }
    
    
    public IEnumerator getdatas(){
        using(UnityWebRequest request = UnityWebRequest.Get(URl)){
            yield return request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.ConnectionError){
                Debug.LogError(request.error);
            }
            else{
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);
            }
        }
    }

    public IEnumerator  putdatas(string data){
        using (UnityWebRequest www = UnityWebRequest.Put(URl, data)){
            yield return www.SendWebRequest();
        }
    }
    
}
