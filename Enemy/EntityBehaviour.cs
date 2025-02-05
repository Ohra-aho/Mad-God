using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour
{
    EnemyPathFinding path_finder;
    PlayerDetector sight;
    AutomatedMovement move_controller;

    [HideInInspector] public bool target_available; // If there is target to move towards
    [HideInInspector] public bool aggro; // Will trigger chase is target is found
    public enum StateMachine
    {
        Roam, // If no priority target found
        Chase, // If priority target found, chase it
        Flee, // Move away from target
        Move_towards // Move towards set target
    }

    public StateMachine current_state = StateMachine.Roam;

    // Start is called before the first frame update
    void Start()
    {
        path_finder = GetComponent<EnemyPathFinding>();
        sight = transform.GetChild(0).GetChild(0).GetComponent<PlayerDetector>();
        move_controller = GetComponent<AutomatedMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ControlBehaviour();
    }

    private void ControlBehaviour()
    {
        switch(current_state)
        {
            case StateMachine.Roam:
                Roam();
                break;
            case StateMachine.Chase:
                Chase();
                break;
            case StateMachine.Move_towards:
                MoveTowards();
                break;
        }
        
        if(current_state != StateMachine.Roam && !target_available)
        {
            current_state = StateMachine.Roam;
            path_finder.ClearPath();
        } 
        else if(current_state != StateMachine.Chase && target_available && aggro)
        {
            current_state = StateMachine.Chase;
            path_finder.ClearPath();
        }
        else if (current_state != StateMachine.Move_towards && target_available && !aggro)
        {
            current_state = StateMachine.Move_towards;
            path_finder.ClearPath();
        }
    }

    private void Roam()
    {
        if (sight.target_in_sight)
        {
            //If target sighted, aggro toward them
            path_finder.true_target = sight.player;
            target_available = true;
            aggro = true;
            move_controller.immobal = false;
        }
        else
        {
            //Move to randon locations
            path_finder.ControlPath();
        }
    }

    private void MoveTowards()
    {
        path_finder.ControlPath();
    }

    private void Chase()
    {
        path_finder.ControlPath();
    }

    private void Flee()
    {

    }


}
