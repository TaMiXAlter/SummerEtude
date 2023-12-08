using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Mediapipe.Unity.Tutorial
{
public class poseControl : MonoBehaviour
{
    [Header ("取得姿勢")]
    public  static bool handRaisedL,handRaisedR;
    public static bool bendOverL,bendOverR; 
    public static bool cross;
    public static bool circle;
    public static bool RunR, RunL;
    void Update()
    {
        var pp = posetest.posepoint;
        if (SceneManager.GetActiveScene().buildIndex < 4 ||SceneManager.GetActiveScene().buildIndex == 7)
        {
            //舉手
            handRaisedL = (pp[19].y>pp[11].y);
            handRaisedR = (pp[20].y>pp[12].y);
            //彎腰
            float BendSlop = GetSlop(new Vector2(pp[12].x,pp[12].y),new Vector2(pp[11].x,pp[11].y));
            bendOverL = BendSlop > 1; 
            bendOverR = BendSlop <-1; 
            //打叉
            cross = (pp[15].y < pp[10].y&& pp[14].y <pp[10].y && pp[19].x>pp[20].x);
            //打圈
            circle = (pp[19].y>pp[0].y&&pp[20].y>pp[0].y&&pp[19].x > pp[11].x&& pp[20].x <pp[12].x  );
        }
        else if(SceneManager.GetActiveScene().buildIndex == 5)
        {
            //跑步
            RunR = GetSlop(new Vector2(pp[26].z, pp[26].y), new Vector2(pp[24].z, pp[24].y)) <-1;
            RunL = GetSlop(new Vector2(pp[25].z, pp[25].y), new Vector2(pp[23].z, pp[23].y)) <-1;
            // Debug.Log('R'+GetSlop(new Vector2(pp[26].z, pp[26].y), new Vector2(pp[24].z, pp[24].y)).ToString());
        }
        
        
        

    }

    float GetSlop(Vector2 p1, Vector2 p2)
    {
        float k = 0;
        
        k = (p2.y - p1.y) / (p2.x - p1.x);
        
        return k;
    }
}}
