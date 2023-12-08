using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class RingTeachSystem : MonoBehaviour
{

    public Transform mask;

    private void Start()
    {
        mask.localPosition = new Vector3(-210, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        bool circle = Mediapipe.Unity.Tutorial.poseControl.circle;
        if(circle){
            if(mask.localPosition.x >0 && !gameObject.GetComponent<VideoPlayer>().isPlaying)
            {
                StartCoroutine(PlayVideo());
            }
            else{
                mask.localPosition = new Vector3 (mask.localPosition.x+1.5f,0,0);
            }
        }
        else
        {
            mask.localPosition = new Vector3(-210, 0, 0);
        }
    }

    IEnumerator PlayVideo()
    {
        GameObject.Find("Canvas").SetActive(false);
        gameObject.GetComponent<VideoPlayer>().Play();
        yield return new WaitForSeconds(17f);
        SceneManager.LoadScene(3);
    }
}
