using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        UpdateBoard();
    }

    protected override IEnumerator MoveDelay(Vector3 destinationPos, float delayTime)
    {
        yield return StartCoroutine(base.MoveDelay(destinationPos,delayTime));
        UpdateBoard();
    }

    void UpdateBoard()
    {
        if (m_board != null)
        {
            m_board.UpdatePlayerNode();
        }
    }
}
