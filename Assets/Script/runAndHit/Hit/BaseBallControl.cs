using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBallControl : MonoBehaviour
{
    private GameObject Baseball;
    // Start is called before the first frame update
    void Start()
    {
        Baseball = GameObject.Find("baseball");
        StartCoroutine(WaitForAppear());
    }
    IEnumerator WaitForAppear()
    {
        Baseball.SetActive(false);
        yield return new WaitForSeconds(4f);
        Baseball.SetActive(true);
        yield break;
    }
}
