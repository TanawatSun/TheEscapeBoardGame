using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static float spacing = 2f;
    public static readonly Vector2[] direction =
    {
        new Vector2(spacing , 0f),
        new Vector2(-spacing , 0f),
        new Vector2(0f , spacing),
        new Vector2(0f , -spacing)
    };

    List<Node> m_allNode = new List<Node>();
    public List<Node> AllNode { get { return m_allNode; } }

    Node m_playerNode;
    public Node PlayerNode { get { return m_playerNode; } }

    PlayerMovement m_player;

    private void Awake()
    {
        m_player = Object.FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
        GetNodeList();
    }

    public void GetNodeList()
    {
        Node[] nodeList = GameObject.FindObjectsOfType<Node>();
        m_allNode = new List<Node>(nodeList);
    }

    public Node FindNode(Vector3 pos)
    {
        Vector2 boardCoordinate = Utility.Vector2Round(new Vector2(pos.x, pos.z));
        return m_allNode.Find(n => n.Coordinate == boardCoordinate);
    }

    public Node FindPlayerNode()
    {
        if (m_player != null && !m_player.isMove)
        {
            return FindNode(m_player.transform.position);
        }
        return null;
    }

    public void UpdatePlayerNode()
    {
        m_playerNode = FindPlayerNode();
    }


}
