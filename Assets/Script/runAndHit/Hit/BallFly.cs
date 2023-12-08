using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class BallFly : MonoBehaviour
{
    public Transform GoodBallBox;
    // Start is called before the first frame update
    private void Start()
    {
        GO();
    }

    public void GO()
    {
        gameObject.transform.localPosition = new Vector3(1.1f, 2.3f, 0);
        gameObject.transform.LookAt(GoodBallBox);
        StartCoroutine(fly());
    }

    IEnumerator fly()
    {
        if (gameObject.transform.position.z > -10f)
        {
            gameObject.transform.Translate(new Vector3(0, 0, 1f));
        }
        else
        {
            yield break;
        }

        yield return new WaitForSeconds(0.016f);
        yield return fly();
    }
    
    

   
}
