using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MaskableGraphic))]
public class ScreenFader : MonoBehaviour
{
    [SerializeField] Color solidColor = Color.white;
    [SerializeField] Color clearColor = new Color(1f, 1f, 1f, 0f);

    [SerializeField] float delay = 1f;
    [SerializeField] float timeToFade = 1f;
    [SerializeField] iTween.EaseType easeType = iTween.EaseType.easeOutExpo;

    MaskableGraphic maskableGraphic;
    private void Awake()
    {
        maskableGraphic = GetComponent<MaskableGraphic>();
    }

    void UpdateColor(Color newColor)
    {
        maskableGraphic.color = newColor;
    }

    public void FadeOut()
    {
        iTween.ValueTo(gameObject,iTween.Hash(
            "from",solidColor,
            "to",clearColor,
            "time",timeToFade,
            "delay",delay,
            "easetype",easeType,
            "onupdatetarget",gameObject,
            "onupdate","UpdateColor"
            ));
    }
    public void FadeIn()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", clearColor,
            "to", solidColor,
            "time", timeToFade,
            "delay", delay,
            "easetype", easeType,
            "onupdatetarget", gameObject,
            "onupdate", "UpdateColor"
            ));
    }
}
