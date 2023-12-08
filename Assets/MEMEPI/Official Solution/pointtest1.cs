using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mediapipe.Unity
{
public class pointtest1 : MonoBehaviour
{
     [SerializeField] private PoseLandmarkListAnnotationController _poseLandmarkListAnnotationController;
    // Start is called before the first frame update
    void Start()
    {

        Debug.Log(_poseLandmarkListAnnotationController);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
}
