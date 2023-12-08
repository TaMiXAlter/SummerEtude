using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CaractorChose{
    public class WBGControl : MonoBehaviour
    {
        public List<Sprite> animals,faces;
        public Image Caractor,hard;
        public Transform Mask,Mask2;
        public CaractorAnime caractorAnime;
        public CaractorChoiseSystem caractorChoiseSystem;
        GM.GameManager gameManager;
        int CaractorNum,HardNum;
        bool stage,wait;
        
        bool raisedHands,bendOverL,bendOverR,circle ;

        public AudioSource confir;


        void Start()
        {
            //參數設置
            setup();

            //抓GM
            gameManager = GameObject.Find("GM").GetComponent<GM.GameManager>();
            //設 List
        }


        void Update()
        {
            //取得資料
            raisedHands = Mediapipe.Unity.Tutorial.poseControl.handRaisedL &&Mediapipe.Unity.Tutorial.poseControl.handRaisedR;
            bendOverL = Mediapipe.Unity.Tutorial.poseControl.bendOverL  ;
            bendOverR = Mediapipe.Unity.Tutorial.poseControl.bendOverR;
            circle = Mediapipe.Unity.Tutorial.poseControl.circle;
            //設定角色圖片
            Caractor.sprite = animals[CaractorNum];
            hard.sprite = faces[HardNum];
            //選角色
            if(!stage){ 
                picking(true,animals,Mask);
                if(Mask.localPosition.y>0){
                    confir.PlayOneShot(confir.clip);
                    stage = true;
                }
            }
            //選難度
            else if(stage){
                picking(false,faces,Mask2);
                if(Mask2.localPosition.y>0){
                    confir.PlayOneShot(confir.clip);
                    //儲存角色
                    caractorAnime.Cractorset(caractorChoiseSystem.playerChoiseNow,animals[CaractorNum]);
                    string CaractorString = animals[CaractorNum].ToString();
                    string[] SLPCS = CaractorString.Split(' ');
                    CaractorString = SLPCS[0];
                    //儲存進GM
                    gameManager.PlayerCaractors.Add(CaractorString);
                    gameManager.PlayerHard.Add(HardNum);
                    //清除選過的角色圖
                    animals.Remove(animals[CaractorNum]);
                    //停止閃爍
                    caractorChoiseSystem.confrom();

                }
            }
            
        }

        int Choice(int data,int change,List<Sprite> list){
            if(data == (list.Count-1) && change == 1){
                data = 0;
            }else if(data == 0 && change == -1){
                data = (list.Count-1);
            }else{
                data += change;
            }
            return data;
        } 

        void picking(bool caractor,List<Sprite> list,Transform Mask){
            if(caractor){
                if(raisedHands&bendOverR&!wait){
                CaractorNum=Choice(CaractorNum,1,list);
                wait = true;
                }
                else if(raisedHands&bendOverL&!wait){
                    CaractorNum=Choice(CaractorNum,-1,list);
                    wait = true;
                }
            }
            else if (!caractor){
                if(raisedHands&bendOverR&!wait){
                HardNum=Choice(HardNum,1,list);
                wait = true;
                }
                else if(raisedHands&bendOverL&!wait){
                    HardNum=Choice(HardNum,-1,list);
                    wait = true;
                }
            }
            

            if(circle){
                Mask.localPosition = new Vector3(0,Mask.localPosition.y+1.5f,0);
            }
            else if(!bendOverL & !bendOverR){
                wait = false;
                Mask.localPosition = new Vector3(0,-91,0);
            }
        }


        public void setup(){
            CaractorNum = 0;
            HardNum = 0;

            stage = false;
            wait = false;

            Mask.localPosition = new Vector3(0,-91,0);
            Mask2.localPosition = new Vector3(0,-91,0);
        }

    }
}
