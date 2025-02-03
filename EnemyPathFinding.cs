using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    public PathNode target;
    public GameObject true_target;
    AutomatedMovement moveController;
    public bool active = false;

    public PathNode currentNode;
    [HideInInspector] public List<PathNode> path;

    bool test = false;

    Vector3 targetPos;

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
        currentNode = GetComponent<NodeBridge>().FindClosestNode();
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


    // Make enemy aggro and movement follow or assist this somehow
    public void CreatePath()
    {
        if(true_target != null)
        {
            if(currentNode == null) currentNode = GetComponent<NodeBridge>().FindClosestNode();

            if (true_target.transform.position != targetPos) {
                targetPos = true_target.transform.position;
                target = true_target.GetComponent<NodeBridge>().FindClosestNode();
                path.Clear();
            }
            if (path != null && path.Count > 0)
            {
                int x = 0;
                target = path[x];

                //If in vasinity of target node, move toward the next one
                if (Vector2.Distance(transform.position, path[x].transform.position) < 0.2f)
                {
                    currentNode = path[x];
                    path.RemoveAt(x);
                }
                if (path.Count == 0)
                {
                    target = null;
                    moveController.StopMoving();
                }
            }
            else
            {
                if (target != null)
                {
                    path = AStarManager.instance.GeneratePath(currentNode, target);
                }
            }
        }
    }
}
