using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Wifi;


namespace Mediapipe.Unity.Tutorial{
    public class StartPageControl : MonoBehaviour
    {
 
        public RectTransform Mask,RMask;
        float xposition;
        private void Start() {
           xposition= Mask.localPosition.x;
           StartCoroutine(moveMask());
           Destroy(GameObject.Find("TCPServer"));
        }

        IEnumerator moveMask(){
            if(Mask.localPosition.x>0 && !gameObject.GetComponent<AudioSource>().isPlaying )
            {
                StartCoroutine(soundload());
            }
            else if(RMask.localPosition.x < 0f )
            {
                RestApi restApi = GameObject.Find("APIsystem").GetComponent<RestApi>();
                GameObject.Find("TCPServer").GetComponent<tcp2>().closeS();
                StartCoroutine(restApi.putdatas("0"));
                yield return new WaitForSeconds(0.5f);
                UnityEditor.EditorApplication.isPlaying = false;
            }
            else{
                if(poseControl.handRaisedL && poseControl.handRaisedR && Mask.localPosition.x<=0 ){
                    Mask.localPosition = new Vector3(Mask.localPosition.x+2,0,0);
                    yield return new WaitForSeconds(0.02f);
                }
                else if (poseControl.cross)
                {
                    RMask.localPosition = new Vector3(RMask.localPosition.x-2,0,0);
                    yield return new WaitForSeconds(0.05f);
                }
                else if(!poseControl.handRaisedL && !poseControl.handRaisedR){
                    Mask.localPosition = new Vector3(xposition,0,0);
                    RMask.localPosition = new Vector3(140,0,0);
                }
            }

            yield return new WaitForFixedUpdate();
            yield return moveMask();
        }

        IEnumerator soundload()
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
            yield return new WaitForSeconds(0.2f);
            SceneManager.LoadScene(1);
        }
        
        
  
    }

}
