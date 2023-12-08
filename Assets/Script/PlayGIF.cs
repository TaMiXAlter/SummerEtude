using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;
public class PlayGIF : MonoBehaviour
{
    private Image img;

    public Sprite[] sources;
    // Start is called before the first frame update
    void Start()
    {
        img = gameObject.GetComponent<Image>();
        StartCoroutine(waitAndChange(0));
    }

    IEnumerator waitAndChange(int i )
    {
        yield return new WaitForSeconds(1f);
        img.sprite = sources[i];
        if (i < sources.Length-1)
        {
            yield return waitAndChange(i + 1);
        }
        else
        {
            yield return waitAndChange(0);
        }
    }
}
