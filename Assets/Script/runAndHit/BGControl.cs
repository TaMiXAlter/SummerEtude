using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGControl : MonoBehaviour
{
    private HitSystem _hitSystem;
    private bool setup;
    public AudioClip RuningBG;
    // Start is called before the first frame update
    void Start()
    {
        _hitSystem = GameObject.Find("GameSystem").GetComponent<HitSystem>();
        setup = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_hitSystem.Hit&&!setup)
        {
            AudioSource AS = gameObject.GetComponent<AudioSource>();
            AS.Stop();
            AS.clip = RuningBG;
            AS.Play();
            setup = true;
        }
    }
}
