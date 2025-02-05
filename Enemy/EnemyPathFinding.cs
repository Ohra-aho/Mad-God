using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    public PathNode target;
    [HideInInspector] public GameObject true_target;
    AutomatedMovement moveController;
    public bool active = false;

    public PathNode currentNode;
    [HideInInspector] public List<PathNode> path;


    Vector3 targetPos;

    private void Awake()
    {
        path = new List<PathNode>();

        moveController = GetComponent<AutomatedMovement>();
        moveController.immobal = true;
        Aggro();
        currentNode = GetComponent<NodeBridge>().FindClosestNode();
    }

    // Update is called once per frame
    private void Update()
    {
        ControlPath();
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


    // Make enemy aggro and movement follow or assist this somehow
    public void ControlPath()
    {
        
        if(currentNode == null) currentNode = GetComponent<NodeBridge>().FindClosestNode();
        if (path != null && path.Count > 0)
        {
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
        if (Vector2.Distance(transform.position, path[x].transform.position) < 0.2f)
        {
            currentNode = path[x];
            path.RemoveAt(x);

            //Target moved too far from original positon
            if (Vector2.Distance(targetPos, true_target.transform.position) >= 2f)
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
            }
        }
       
    }

    private void CreatePath()
    {
        target = null;
        path.Clear();

        if(true_target != null)
        {
            targetPos = true_target.transform.position; //Might not be nessessary
            target = true_target.GetComponent<NodeBridge>().FindClosestNode();
            path = AStarManager.instance.GeneratePath(currentNode, target);
        }
    }
}
