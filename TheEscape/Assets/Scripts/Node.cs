using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    Vector2 m_coordinate;
    public Vector2 Coordinate { get { return Utility.Vector2Round(m_coordinate); } }

    List<Node> m_neighborsNode = new List<Node>();
    public List<Node> NeighborsNode { get { return m_neighborsNode; } }

    List<Node> m_linkNode = new List<Node>();
    public List<Node> LinkedNodes { get { return m_linkNode; } }

    Board m_board;


    [SerializeField] GameObject geometry;
    [SerializeField] float scaleTime = 0.3f;
    [SerializeField] iTween.EaseType easeType = iTween.EaseType.easeInExpo;
    [SerializeField] float delayTime = 1f;
    [SerializeField] GameObject linkPrefab;
    [SerializeField] LayerMask obstacleLayer;

    public bool isNodeGoal = false;

    private void Awake()
    {
        m_board = Object.FindObjectOfType<Board>();
        m_coordinate = new Vector2(transform.position.x, transform.position.z);
    }

    private void Start()
    {
        if (geometry != null)
        {
            geometry.transform.localScale = Vector3.zero;
        }
        AwakeNodes();
        if (m_board != null)
        {
            m_neighborsNode = FindNeighbors(m_board.AllNode);
        }
        
    }

    public void ShowGeometry()
    {
        if (geometry != null)
        {
            iTween.ScaleTo(geometry, iTween.Hash(
                "time",scaleTime,
                "scale",Vector3.one,
                "easetype",easeType,
                "delay",delayTime
                ));
        }
    }

    public List<Node> FindNeighbors(List<Node> nods)
    {
        List<Node> nodeList = new List<Node>();
        foreach (Vector2 dir in Board.direction)
        {
            Node foundNeighbors = nods.Find(n => n.Coordinate == Coordinate + dir);

            if (foundNeighbors != null && !nodeList.Contains(foundNeighbors))
            {
                nodeList.Add(foundNeighbors);
            }
        }
        return nodeList;
    }

    public void AwakeNodes()
    {
        ShowGeometry();
        AwakeNeighborsNode();
        /*if (!m_isAwake)
        {
            ShowGeometry();
            AwakeNeighborsNode();
            m_isAwake = true;
        }*/

    }

    void AwakeNeighborsNode()
    {
        StartCoroutine(AwakeNeighborsNodeDelay());
    }

    IEnumerator AwakeNeighborsNodeDelay()
    {
        yield return new WaitForSeconds(delayTime);
        foreach(Node node in m_neighborsNode)
        {
            if (!m_linkNode.Contains(node))
            {
                Obstacle obstacle = FindObstacle(node);
                if (obstacle == null)
                {
                    LinkNode(node);
                    node.AwakeNodes();
                }

            }

        }
    }

    void LinkNode(Node nodeTarget)
    {
        if (linkPrefab != null)
        {
            GameObject linkInstance = Instantiate(linkPrefab, transform.position, Quaternion.identity);
            linkInstance.transform.parent = transform;

            Link link = linkInstance.GetComponent<Link>();
            if (link != null)
            {
                link.DrawLink(transform.position, nodeTarget.transform.position);
            }

            if (!m_linkNode.Contains(nodeTarget))
            {
                m_linkNode.Add(nodeTarget);
            }

            if (!nodeTarget.LinkedNodes.Contains(this))
            {
                nodeTarget.LinkedNodes.Add(this);
            }
        }
    }

    Obstacle FindObstacle(Node targetNode)
    {
        Vector3 checkDirection = targetNode.transform.position - transform.position;
        RaycastHit raycastHit;
        if(Physics.Raycast(transform.position,checkDirection,out raycastHit, Board.spacing + 0.1f, obstacleLayer))
        {
            return raycastHit.collider.GetComponent<Obstacle>();
        }
        return null;
    }

}
