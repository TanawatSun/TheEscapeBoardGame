using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    Stand,
    Patrol,
    Spinner

}

public class EnemyMovement : Movement
{
    public Vector3 directionToMove = new Vector3(0f, 0f, Board.spacing);
    public MoveType moveType = MoveType.Stand;

    public float standTime = 1f;

    public float enemyRotationTime = 1f;
    protected override void Awake()
    {
        base.Awake();
        faceDestination = true;
    }

    protected override void Start()
    {
        base.Start();
    }

    public void MoveOneTurn()
    {
        switch (moveType)
        {
            case MoveType.Patrol:
                Patrol();
                break;
            case MoveType.Stand:
                Stand();
                break;
            case MoveType.Spinner:
                Spin();
                break;
        }
        
    }

    void Stand()
    {
        StartCoroutine(StandCoroutine());
    }

    void Patrol()
    {
        StartCoroutine(PatrolCoroutine());
    }

    IEnumerator PatrolCoroutine()
    {
        Vector3 startPos = new Vector3(m_currentNode.Coordinate.x, 0f, m_currentNode.Coordinate.y);
        Vector3 newDestination = startPos + transform.TransformVector(directionToMove);
        Vector3 nextDestination = startPos + transform.TransformVector(directionToMove * 2f);
        Move(newDestination, 0f);
        while (isMove)
        {
            yield return null;
        }
        if (m_board != null)
        {
            Node newDestinationNode = m_board.FindNode(newDestination);
            Node nextDestinationNode = m_board.FindNode(nextDestination);

            if (nextDestinationNode == null || !newDestinationNode.LinkedNodes.Contains(nextDestinationNode))
            {
                destination = startPos;
                FacingDiraction();

                yield return new WaitForSeconds(enemyRotationTime);
            }

        }
        base.finishMoveEvent.Invoke();

    }

    IEnumerator StandCoroutine()
    {
        yield return new WaitForSeconds(standTime);
        base.finishMoveEvent.Invoke();
    }

    void Spin()
    {
        StartCoroutine(SpinRoutine());
    }

    IEnumerator SpinRoutine()
    {
        Vector3 localForward = new Vector3(0f, 0f, Board.spacing);
        destination = transform.TransformVector(localForward * -1f) + transform.position;

        FacingDiraction();
        yield return new WaitForSeconds(rotationTime);
        base.finishMoveEvent.Invoke();
    }
}
