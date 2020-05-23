using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyEye))]
public class EnemyManager : TurnBaseManager
{
    EnemyMovement m_enemyMovement;
    EnemyEye m_enemyEye;
    Board m_board;

    protected override void Awake()
    {
        base.Awake();
        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
        m_enemyEye = GetComponent<EnemyEye>();
        m_enemyMovement = GetComponent<EnemyMovement>();
    }

    public void PlayTurn()
    {
        StartCoroutine(PlayerTurnRoutine());
    }

    IEnumerator PlayerTurnRoutine()
    {
        m_enemyEye.UpdateEye();
        yield return new WaitForSeconds(0f);
        m_enemyMovement.MoveOneTurn();
        
    }
}
