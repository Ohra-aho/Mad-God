using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    public PathNode target;
    AutomatedMovement moveController;
    public bool active = false;

    public PathNode currentNode;
    [HideInInspector] public List<PathNode> path;

    bool test = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        path = new List<PathNode>();

        moveController = GetComponent<AutomatedMovement>();
        moveController.immobal = true;
        Aggro();
        currentNode = FindClosestNode();
    }

    // Update is called once per frame
    private void Update()
    {
        CreatePath();
        moveController.FaceTowards(target);
    }

    private void FixedUpdate()
    {
        if(path != null && path.Count > 0)
        {
            moveController.Move();
        }
    }

    public void Aggro()
    {
        if(!active)
        {
            active = true;
            moveController.immobal = false;
        }
    }

    public void DeAggro()
    {
        if(active)
        {
            active = false;
            //moveController.immobal = true;
        }
    }

    public void CreatePath()
    {
        if(path != null && path.Count > 0)
        {
            int x = 0;
            target = path[x];

            //If in vasinity of target node, move toward the next one
            if(Vector2.Distance(transform.position, path[x].transform.position) < 0.5f)
            {
                currentNode = path[x];
                path.RemoveAt(x);
            }
            if(path.Count == 0)
            {
                target = null;
                moveController.StopMoving();
            }
        }
        else
        {
            //PathNode[] nodes = FindObjectsOfType<PathNode>();
            if (target != null && (path == null || path.Count == 0))
            {
                path = AStarManager.instance.GeneratePath(currentNode, target);
            }
        }
        test = true;
    }

    private PathNode FindClosestNode()
    {
        PathNode[] allNodes = FindObjectsOfType<PathNode>();

        float shortestDist = float.MaxValue;
        PathNode closest_node = null;

        for(int i = 0; i < allNodes.Length; i++)
        {
            if(Vector2.Distance(transform.position, allNodes[i].transform.position) < shortestDist)
            {
                closest_node = allNodes[i];
                shortestDist = Vector2.Distance(transform.position, allNodes[i].transform.position);
            }
        }

        return closest_node;
    }
}
