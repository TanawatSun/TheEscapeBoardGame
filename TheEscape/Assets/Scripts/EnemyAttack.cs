using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerManager m_player;
    private void Awake()
    {
        m_player = Obstacle.FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
    }
    public void Attack()
    {
        if (m_player != null)
        {
            m_player.Die();
        }
    }
}
