using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM{
        public class PlayerNumImgControl : MonoBehaviour
    {
        float[] PosX = new float[4]{0f,-95f,-186f,-285f};
        Transform TF ;
        int hisnum;
        private GameManager GM;

        private void Start()
        {
            GM = GameObject.Find("GM").GetComponent<GameManager>();
            TF = gameObject.transform;
            TF.localPosition = new Vector3(0,0,0);
            hisnum = 1;
        }

        void Update()
        {
            if(GM.PlayerNum != hisnum){
                StartCoroutine(SlideAni(GM.PlayerNum));
                hisnum = GM.PlayerNum;
            }
        }


        IEnumerator SlideAni(int x)
        {
            if(TF.localPosition.x != PosX[x-1]){
                float distance = (PosX[x-1] - TF.localPosition.x)/10;
                for(int i =0 ; i<10;i++){
                    TF.localPosition = new Vector3 (TF.localPosition.x+distance,0,0);
                    yield return new WaitForSeconds(0.05f);
                }
                
            }
        }
    }
}

