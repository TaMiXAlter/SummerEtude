using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class PitcherModelOutput : MonoBehaviour
{

    public GameObject[] Pitchers;

    // Start is called before the first frame update
    void Start()
    {
        int PitcherNum = Random.Range(0, Pitchers.Length);
        Instantiate(Pitchers[PitcherNum], gameObject.transform);
    }
}
