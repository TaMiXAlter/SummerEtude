using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GM{
        public class GameManager : MonoBehaviour
    {
        //玩家人數
        public int PlayerNum;
        //玩家資料
        public List<string> PlayerCaractors;
        public List<int> PlayerHard;

        public int[] PlayerStage;
        //正在遊玩的人
        public int Playernow,PlayerNowStage;
        // Start is called before the first frame update
        void Start()
        {

            DontDestroyOnLoad(gameObject);
            // Playernow = 0;
            // PlayerNowStage = 0;
            
        }

        private void Update()
        {
            int BI = SceneManager.GetActiveScene().buildIndex;
            if (BI == 0)
            {
                Destroy(gameObject);
            }
            else if (BI > 2)
            {
                AudioSource AR = gameObject.GetComponentInChildren<AudioSource>();
                AR.Stop();
            }
        }
    }
}

