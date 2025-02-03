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

    public float FScore()
    {
        return gScore + hScore;
    }

    private void Update()
    {
        if(!available)
        {
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
                connections[i].connections.Remove(this);
            }
            connections.Clear();
            Destroy(GetComponent<CircleCollider2D>());

        }
    }
}
