using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarManager : MonoBehaviour
{
    public static AStarManager instance;

    private void Start()
    {
        instance = this;

    }

    private void Awake()
    {
        instance = this;
    }

    public List<PathNode> GeneratePath(PathNode start, PathNode end)
    {
        List<PathNode> open_set = new List<PathNode>();

        foreach(PathNode n in FindObjectsOfType<PathNode>())
        {
            n.gScore = float.MaxValue;
            n.cameFrom = null;
        }

        start.gScore = 0;
        start.hScore = Vector2.Distance(start.transform.position, end.transform.position);
        open_set.Add(start);

        while(open_set.Count > 0)
        {
            int lowestF = default;

            for(int i = 1; i < open_set.Count; i++)
            {
                if(open_set[i].FScore() < open_set[lowestF].FScore())
                {
                    lowestF = i;
                }
            }

            PathNode currentNode = open_set[lowestF];
            open_set.Remove(currentNode);

            if(currentNode == end)
            {
                List<PathNode> path = new List<PathNode>();
                path.Insert(0, end);

                while (currentNode != start)
                {
                    currentNode = currentNode.cameFrom;
                    path.Add(currentNode);
                }

                path.Reverse();
                return path;
            }

            foreach(PathNode connectedNode in currentNode.connections)
            {
                float heldGScore = currentNode.gScore + Vector2.Distance(currentNode.transform.position, connectedNode.transform.position);

                if(heldGScore < connectedNode.gScore)
                {
                    connectedNode.cameFrom = currentNode;
                    connectedNode.gScore = heldGScore;
                    connectedNode.hScore = Vector2.Distance(connectedNode.transform.position, end.transform.position);

                    if(!open_set.Contains(connectedNode))
                    {
                        open_set.Add(connectedNode);
                    }
                }
            }
        }

        return null;
    }
}
