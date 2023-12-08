using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mediapipe.Unity.Tutorial
{
public class SliderTest : MonoBehaviour
{
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();


    }

    // Update is called once per frame
    void Update()
    {
        if(poseControl.handRaisedL){
            StartCoroutine(ValueUp());
        }else{
            slider.value = 0;
        }
    }

    IEnumerator ValueUp(){
        slider.value+=0.01f;
        yield return new WaitForSeconds(0.1f);

    }
}}
