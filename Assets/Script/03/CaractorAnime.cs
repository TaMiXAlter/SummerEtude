using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CaractorChose{
    public class CaractorAnime : MonoBehaviour
    {
        public Image[] Frames,Caractors;

        private void Start() {
            for(int i=0 ; i <Frames.Length;i++){
                Frames[i].color = new Color(255,255,255);
            }
        }
        public void Cractorset(int i ,Sprite animal ){
            Caractors[i].sprite =  animal;
            Caractors[i].color = Color.white;
        }

    }

}
