using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Vector3 destination;
    public bool isMove = false;
    [SerializeField] iTween.EaseType easeType = iTween.EaseType.easeOutQuint;

    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float iTweenDelay = 0f;
    GameManager m_gameManager;

    protected Board m_board;
    protected Node m_currentNode;
    protected virtual void Awake()
    {
        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
        m_gameManager = Object.FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }
    protected virtual void Start()
    {
        UpdateCurrentNode();
    }

    public void Move(Vector3 destinationPos, float delayTime = 0.25f)
    {
        if (isMove)
        {
            return;
        }
        if (m_board != null)
        {
            Node targetNode = m_board.FindNode(destinationPos);
            if (targetNode != null && m_currentNode != null)
            {
                if (m_currentNode.LinkedNodes.Contains(targetNode))
                {
                    StartCoroutine(MoveDelay(destinationPos, delayTime));
                }
            }
          
        }

    }

    protected virtual IEnumerator MoveDelay(Vector3 destinationPos, float delayTime)
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

        while (Vector3.Distance(destinationPos, transform.position) > 0.01f)
        {
            yield return null;
        }

        iTween.Stop(gameObject);
        transform.position = destinationPos;
        isMove = false;
        UpdateCurrentNode();
    }

    public void MoveForword()
    {
        Vector3 newPos = transform.position + new Vector3(0f, 0f, Board.spacing);
        Move(newPos, 0);
    }
    public void MoveBackword()
    {
        Vector3 newPos = transform.position + new Vector3(0f, 0f, -Board.spacing);
        Move(newPos, 0);
    }
    public void MoveRight()
    {
        Vector3 newPos = transform.position + new Vector3(Board.spacing, 0f, 0f);
        Move(newPos, 0);
    }
    public void MoveLeft()
    {
        Vector3 newPos = transform.position + new Vector3(-Board.spacing, 0f, 0f);
        Move(newPos, 0);
    }

    protected void UpdateCurrentNode()
    {
        if (m_board != null)
        {
            m_currentNode = m_board.FindNode(transform.position);
        }
    }

}
