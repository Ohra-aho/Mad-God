using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    [HideInInspector] public GameObject player;
    AutomatedMovement moveController;
    bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        //player = GameObject.Find("Player");
        moveController = GetComponent<AutomatedMovement>();
        moveController.immobal = true;
    }

    // Update is called once per frame
    private void Update()
    {
        moveController.FaceTowards(player);
    }

    private void FixedUpdate()
    {
        moveController.Move();
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
            moveController.immobal = true;
        }
    }
}
