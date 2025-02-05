using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBridge : MonoBehaviour
{
    public PathNode FindClosestNode()
    {
        PathNode[] allNodes = FindObjectsOfType<PathNode>();

        float shortestDist = float.MaxValue;
        PathNode closest_node = null;

        for (int i = 0; i < allNodes.Length; i++)
        {
            if(allNodes[i].available)
            {
                if (Vector2.Distance(transform.position, allNodes[i].transform.position) < shortestDist)
                {
                    closest_node = allNodes[i];
                    shortestDist = Vector2.Distance(transform.position, allNodes[i].transform.position);
                }
            }
        }
        return closest_node;
    }
}
