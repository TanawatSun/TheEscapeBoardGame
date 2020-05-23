using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEye : MonoBehaviour
{
    public Vector3 directionToSearch = new Vector3(0f, 0f, 0f);

    Node m_node;
    Board m_board;

    bool m_foundPlayer = false;
    public bool FoundPlayer { get { return m_foundPlayer; } }

    private void Awake()
    {
        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
    }

    public void UpdateEye()
    {
        Vector3 worldSpacePosToSearch = transform.TransformVector(directionToSearch) + transform.position;
        if (m_board != null)
        {
            m_node = m_board.FindNode(worldSpacePosToSearch);
            if (m_node == m_board.PlayerNode)
            {
                m_foundPlayer = true;
            }
        }
    }
}
