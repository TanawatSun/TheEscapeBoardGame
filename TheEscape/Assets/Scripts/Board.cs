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

    Node m_goalNode;
    public Node GoalNode { get { return m_goalNode; } }

    [SerializeField] GameObject goalNodePrefab;
    [SerializeField] float drawGoalTime;
    [SerializeField] float drawGoalDelay;
    [SerializeField] iTween.EaseType goalNodeEaseType = iTween.EaseType.easeOutExpo;

    private void Awake()
    {
        m_player = Object.FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
        GetNodeList();
        m_goalNode = FindGoalNode();
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

    Node FindPlayerNode()
    {
        if (m_player != null && !m_player.isMove)
        {
            return FindNode(m_player.transform.position);
        }
        return null;
    }

    public Node FindGoalNode()
    {
        return m_allNode.Find(n => n.isNodeGoal);
    }

    public void UpdatePlayerNode()
    {
        m_playerNode = FindPlayerNode();
    }

    public void DrawGoalNode()
    {
        if (goalNodePrefab != null && m_goalNode != null)
        {
            GameObject goalNodeInstance = Instantiate(goalNodePrefab, m_goalNode.transform.position, Quaternion.identity);
            iTween.ScaleFrom(goalNodeInstance, iTween.Hash(
                "scale",Vector3.zero,
                "time",drawGoalTime,
                "delay",drawGoalDelay,
                "easetype",goalNodeEaseType
                ));
        }
    }

    public void AwakeBoard()
    {
        if (m_playerNode != null)
        {
            m_playerNode.AwakeNodes();
        }
    }


}
