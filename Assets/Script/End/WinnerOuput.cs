using System.Collections;
using System.Collections.Generic;
using GM;
using UnityEngine;

public class WinnerOuput : MonoBehaviour
{
    public RuntimeAnimatorController EndAni;
    public GameObject[] EndPlayers;
    private GameManager GM;

    private Animator WinnerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GM").GetComponent<GameManager>();
        GameObject WinnerPlayer = Instantiate(EndPlayers[GM.Playernow],gameObject.transform);
        WinnerAnimator = WinnerPlayer.GetComponent<Animator>();
        WinnerAnimator.runtimeAnimatorController = EndAni;
        StartCoroutine(RandomPose());
    }

    IEnumerator RandomPose()
    {
        yield return new WaitForSeconds(3f);
        WinnerAnimator.SetBool("Chose",Random.Range(0,2)==1);
        yield return RandomPose();
    }
}
