using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyEye))]
[RequireComponent(typeof(EnemyAttack))]
public class EnemyManager : TurnBaseManager
{
    EnemyMovement m_enemyMovement;
    EnemyEye m_enemyEye;
    Board m_board;
    EnemyAttack m_enemyAttack;
    protected override void Awake()
    {
        base.Awake();
        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
        m_enemyEye = GetComponent<EnemyEye>();
        m_enemyMovement = GetComponent<EnemyMovement>();
        m_enemyAttack = GetComponent<EnemyAttack>();
    }

    public void PlayTurn()
    {
        StartCoroutine(PlayerTurnRoutine());
    }

    IEnumerator PlayerTurnRoutine()
    {
        if (m_gameManager != null && !m_gameManager.IsGameOver)
        {
            m_enemyEye.UpdateEye();
            yield return new WaitForSeconds(0f);
            if (m_enemyEye.FoundPlayer)
            {
                m_gameManager.LoseLevel();
                Vector3 playerPos = new Vector3(m_board.PlayerNode.Coordinate.x, 0f, m_board.PlayerNode.Coordinate.y);
                m_enemyMovement.Move(playerPos, 0f);

                while (m_enemyMovement.isMove)
                {
                    yield return null;
                }

                m_enemyAttack.Attack();
                
            }
            else
            {
                m_enemyMovement.MoveOneTurn();
            }
        }
    }
}
