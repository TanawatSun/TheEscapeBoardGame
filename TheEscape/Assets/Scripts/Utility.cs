using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    public static Vector3 Vector3Round(Vector3 inputVector3)
    {
        return new Vector3(Mathf.Round(inputVector3.x), Mathf.Round(inputVector3.y), Mathf.Round(inputVector3.z));
    }

    public static Vector2 Vector2Round(Vector3 inputVector2)
    {
        return new Vector3(Mathf.Round(inputVector2.x), Mathf.Round(inputVector2.y));
    }
}
