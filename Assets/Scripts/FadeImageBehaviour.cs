using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImageBehaviour : MonoBehaviour
{

    [SerializeField]
    private Color fadeColor;
    [SerializeField]
    private float fadeSpeed = 0.03f;
    
    private void Start()
    {
        fadeColor = this.GetComponent<Image>().color;
        StartCoroutine(StartFade());
    }

    private IEnumerator StartFade()
    {
        while (true)
        {
            fadeColor.a -= fadeSpeed;
            if (fadeColor.a < 0)
            {
                yield break;
            }

             yield return this.GetComponent<Image>().color = fadeColor;
        }
    }
}
