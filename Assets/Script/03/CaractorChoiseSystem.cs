using System.Collections;
using System.Collections.Generic;
using GM;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace CaractorChose{
    public class CaractorChoiseSystem : MonoBehaviour
    {
        private GameManager _gameManager;
        public int playerChoiseNow;
        public Animator[] FrameAnumators;
        public Animator Canva;
        public GameObject WBG;
        WBGControl wBGControl;

        private void Start()
        {
            _gameManager = GameObject.Find("GM").GetComponent<GameManager>();
            wBGControl = WBG.GetComponent<WBGControl>();
            playerChoiseNow = 0;
            WaitAndShow();
        }

        public void WaitAndShow(){
            FrameAnumators[playerChoiseNow].SetTrigger("FrameShine");
            Canva.SetTrigger("show");
        }

        public void confrom(){
            FrameAnumators[playerChoiseNow].SetTrigger("Conform");
            if(playerChoiseNow < (_gameManager.PlayerNum-1)){
                WBG.SetActive(false);
                Canva.SetTrigger("hide");
                wBGControl.setup();
                playerChoiseNow+=1;
                WaitAndShow();
            }else{
                SceneManager.LoadScene(7);
            }
        }

        
    }
}
