using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    [HideInInspector] public PathNode target;
    [HideInInspector] public GameObject true_target;
    AutomatedMovement moveController;

    public PathNode currentNode;
    [HideInInspector] public List<PathNode> path;


    Vector3 targetPos;
    [HideInInspector] public bool target_reached;

    public bool at_the_end = false;

    private void Awake()
    {
        path = new List<PathNode>();
        moveController = GetComponent<AutomatedMovement>();
        currentNode = GetComponent<NodeBridge>().FindClosestNode();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void FixedUpdate()
    {
    }

    // Make enemy aggro and movement follow or assist this somehow
    public void ControlPath()
    {
        if(currentNode == null) currentNode = GetComponent<NodeBridge>().FindClosestNode();
        if (path != null && path.Count > 0)
        {
            moveController.Move();
            FollowPath();
        }
        else
        {
            CreatePath();
        }
    }

    private void FollowPath()
    {
        int x = 0;
        target = path[x];
        moveController.FaceTowards(target);

        //If in vasinity of target node, move toward the next one
        if (Vector2.Distance(transform.position, path[x].transform.position) < 0.3f)
        {
            currentNode = path[x];
            path.RemoveAt(x);

            //Target moved too far from original positon
            if (
                Vector2.Distance(targetPos, true_target.transform.position) >= 2f && 
                transform.GetChild(0).GetChild(0).GetComponent<PlayerDetector>().target_in_sight
                )
            {
                target = null;
                path.Clear();
                moveController.NormalizeMovement();
            }
            //End of the path
            else if (path.Count == 0)
            {
                target = null;
                moveController.StopMoving();
                at_the_end = true;
            }
        }
       
    }

    public void CreatePath()
    {
        ClearPath();

        if(true_target != null)
        {
            targetPos = true_target.transform.position;
            target = true_target.GetComponent<NodeBridge>().FindClosestNode();
            path = AStarManager.instance.GeneratePath(currentNode, target);
            at_the_end = false;
        }
    }

    public void ClearPath()
    {
        target = null;
        path.Clear();
    }
}
