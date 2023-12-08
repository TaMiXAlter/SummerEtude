using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightDir : MonoBehaviour
{
    private Light directionalLight;
    private void OnEnable()
    {
        directionalLight = GetComponent<Light>();
    }

    // Update is called once per frame
    private void Update()
    {
        Shader.SetGlobalVector("_LightDir", -transform.forward);
        Shader.SetGlobalFloat("_Intensity", directionalLight.intensity);
        Shader.SetGlobalFloat("_DiffuseShadowIntensity", directionalLight.color.a);
        Shader.SetGlobalColor("_LightColor", directionalLight.color);
        // Shader.SetGlobalColor(**)
    }


}
