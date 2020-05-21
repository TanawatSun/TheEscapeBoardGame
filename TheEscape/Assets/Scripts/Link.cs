using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    [SerializeField] float borderWidth = 0.0003f;
    [SerializeField] float scaleTime = 1f;
    [SerializeField] float lineThickness = 0.5f;
    [SerializeField] float delayTime = 0.5f;

    [SerializeField] iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;

    public void DrawLink(Vector3 startPos , Vector3 endPos)
    {
        transform.localScale = new Vector3(lineThickness, 1f, 0f);
        Vector3 dirVector = endPos - startPos;
        float zScale = dirVector.magnitude - borderWidth * 2;
        Vector3 newScale = new Vector3(lineThickness, 1f, zScale);
        transform.rotation = Quaternion.LookRotation(dirVector);

        transform.position = startPos + (transform.forward * borderWidth);

        iTween.ScaleTo(gameObject, iTween.Hash(
            "time",scaleTime,
            "scale",newScale,
            "easetype",easeType,
            "delay",delayTime
            ));
    }
}
