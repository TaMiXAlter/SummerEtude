using System.Collections;
using System.Collections.Generic;
using GM;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePlayer : MonoBehaviour
{
    private GameManager gameManager;
    bool wait;
    public Transform mask;

    void Start()
    {
        gameManager = GameObject.Find("GM").GetComponent<GameManager>();
        gameManager.PlayerNum = 1;
        wait = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        bool raisedHands = Mediapipe.Unity.Tutorial.poseControl.handRaisedL &&Mediapipe.Unity.Tutorial.poseControl.handRaisedR;
        bool bendOverL = Mediapipe.Unity.Tutorial.poseControl.bendOverL  ;
        bool bendOverR = Mediapipe.Unity.Tutorial.poseControl.bendOverR;
        bool circle = Mediapipe.Unity.Tutorial.poseControl.circle;

        if( !wait && bendOverR && raisedHands){
            wait =true;
            numchange(true);
        }else if(!wait && bendOverL &&raisedHands){
            wait =true;
            numchange(false);
        }else if(!bendOverL && !bendOverR ){
            wait = false;
        }
        
        if(circle){
            if(mask.localPosition.x >0 && !gameObject.GetComponent<AudioSource>().isPlaying)
            {
                StartCoroutine(SoundAndLoad());
            }
            else if(mask.localPosition.x <=0){
                mask.localPosition = new Vector3 (mask.localPosition.x+1.5f,0,0);
            }
        }
        else{
            mask.localPosition = new Vector3 (-120,0,0);
        }
    }

    void numchange(bool add){
        if(add){
            if(gameManager.PlayerNum <4){
                gameManager.PlayerNum++;
            }
            else {
                gameManager.PlayerNum =1;
            }
        }
        else if ( !add){
            if(gameManager.PlayerNum >1){
                gameManager.PlayerNum--;
            }
            else {
                gameManager.PlayerNum =4;
            }
        }
        
    }

    IEnumerator SoundAndLoad()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
        yield return new WaitForSeconds(0.2f);
        gameManager.PlayerStage = new int [gameManager.PlayerNum];
        SceneManager.LoadScene(2);
    }
}


