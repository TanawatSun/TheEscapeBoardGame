using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector3 destination;
    public bool isMove = false;
    [SerializeField] iTween.EaseType easeType = iTween.EaseType.easeOutQuint;

    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float iTweenDelay = 0f;

    private void Start()
    {

    }

    public void Move(Vector3 destinationPos, float delayTime = 0.25f)
    {
        StartCoroutine(MoveDelay(destinationPos, delayTime));
    }

    IEnumerator MoveDelay(Vector3 destinationPos, float delayTime)
    {
        isMove = true;
        destination = destinationPos;
        yield return new WaitForSeconds(delayTime);

        iTween.MoveTo(gameObject, iTween.Hash(
            "x", destinationPos.x,
            "y", destinationPos.y,
            "z", destinationPos.z,
            "delay", iTweenDelay,
            "easetype", easeType,
            "speed", moveSpeed));

        while (Vector3.Distance(destinationPos,transform.position)>0.01f)
        {
            yield return null;
        }

        iTween.Stop(gameObject);
        transform.position = destinationPos;
        isMove = false;
    }

    public void MoveForword()
    {
        Vector3 newPos = transform.position + new Vector3(0f, 0f, 2f);
        Move(newPos,0);
    }
    public void MoveBackword()
    {
        Vector3 newPos = transform.position + new Vector3(0f, 0f, -2f);
        Move(newPos,0);
    }
    public void MoveRight()
    {
        Vector3 newPos = transform.position + new Vector3(2f, 0f, 0f);
        Move(newPos,0);
    }
    public void MoveLeft()
    {
        Vector3 newPos = transform.position + new Vector3(-2f, 0f, 0f);
        Move(newPos,0);
    }
}
