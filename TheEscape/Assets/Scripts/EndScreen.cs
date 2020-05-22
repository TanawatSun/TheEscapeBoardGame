using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EndScreen : MonoBehaviour
{
    public PostProcessProfile normalProfile;
    public PostProcessProfile blurProfile;
    public PostProcessVolume processingVolume;

    public void EnableCameraBlur(bool state)
    {
        if (normalProfile != null && blurProfile != null && processingVolume != null)
        {
            processingVolume.profile = (state) ? blurProfile : normalProfile;
        }
    }
}
