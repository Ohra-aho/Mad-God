using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    public PathNode cameFrom;
    public List<PathNode> connections;

    public float gScore;
    public float hScore;

    public bool available = true;

    [HideInInspector] public int grid_width;
    [HideInInspector] public int grid_height;

    int delay_frames = 10;


    public float FScore()
    {
        return gScore + hScore;
    }

    private void Update()
    {
        if (delay_frames > -3) delay_frames--;
        if (delay_frames == 0)
        {
            Destroy(GetComponent<CircleCollider2D>());
        }
    }

    private void OnDrawGizmos()
    {
        if(available)
        {
            Gizmos.color = Color.blue;
            if (connections.Count > 0)
            {
                for (int i = 0; i < connections.Count; i++)
                {
                    Gizmos.DrawLine(transform.position, connections[i].transform.position);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.layer == 6 || other.gameObject.layer == 7)
        {
            if (available) available = false;
            for(int i = 0; i < connections.Count; i++)
            {
                //Above
                if(connections[i].transform.GetSiblingIndex() == transform.GetSiblingIndex() + grid_width)
                {
                    connections[i].GetComponent<PathNode>().RemoveDiagonalConnections(false);
                }
                //Below
                if (connections[i].transform.GetSiblingIndex() == transform.GetSiblingIndex() - grid_width)
                {
                    connections[i].GetComponent<PathNode>().RemoveDiagonalConnections(true);
                }
                connections[i].connections.Remove(this);
            }
            connections.Clear();
        }
    }

    private void RemoveDiagonalConnections(bool up)
    {
        List<PathNode> nodes_to_disconnect = new List<PathNode>();
        if(up)
        {
            for(int i = 0; i < connections.Count; i++)
            {
                if(connections[i].transform.GetSiblingIndex() == transform.GetSiblingIndex() + grid_width + 1)
                {
                    nodes_to_disconnect.Add(connections[i]);
                    connections[i].GetComponent<PathNode>().connections.Remove(this);
                }

                if (connections[i].transform.GetSiblingIndex() == transform.GetSiblingIndex() + grid_width - 1)
                {
                    nodes_to_disconnect.Add(connections[i]);
                    connections[i].GetComponent<PathNode>().connections.Remove(this);
                }
            }
        } else
        {
            for (int i = 0; i < connections.Count; i++)
            {
                if (connections[i].transform.GetSiblingIndex() == transform.GetSiblingIndex() - grid_width + 1)
                {
                    nodes_to_disconnect.Add(connections[i]);
                    connections[i].GetComponent<PathNode>().connections.Remove(this);
                }

                if (connections[i].transform.GetSiblingIndex() == transform.GetSiblingIndex() - grid_width - 1)
                {
                    nodes_to_disconnect.Add(connections[i]);
                    connections[i].GetComponent<PathNode>().connections.Remove(this);
                }
            }
        }
        connections.RemoveAll(node => nodes_to_disconnect.Contains(node));
    }
}
