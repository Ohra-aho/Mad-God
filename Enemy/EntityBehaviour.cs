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

    public GameObject home;

    Coroutine behaviourCR;
    public enum StateMachine
    {
        Roam, // If no priority target found
        Chase, // If priority target found, chase it
        Flee, // Move away from target
        Move_towards, // Move towards set target
        Look_around //Look ones to all directions
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
            case StateMachine.Look_around:
                LookAround();
                break;
        }
        Debug.Log(current_state);
        // If no target found
        if (current_state != StateMachine.Roam && !target_available)
        {
            BasicStateManagement(StateMachine.Roam, true);
        }
        //If at paths end but target not reached
        else if (current_state != StateMachine.Look_around && target_available && aggro && !sight.target_in_sight)
        {
            BasicStateManagement(StateMachine.Look_around, false);
        }
        // If target found
        else if(current_state != StateMachine.Chase && target_available && aggro && sight.target_in_sight)
        {
            BasicStateManagement(StateMachine.Chase, true);
        }
        //If there is a target to move towards but not actively pursue
        else if (current_state != StateMachine.Move_towards && target_available && !aggro)
        {
            BasicStateManagement(StateMachine.Move_towards, true);
        }

    }

    private void BasicStateManagement(StateMachine state, bool clear_path)
    {
        current_state = state;
        if(clear_path) path_finder.ClearPath();
        if (behaviourCR != null) { 
            StopCoroutine(behaviourCR);
            behaviourCR = null;
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
        }
        else
        {
            //Move to randon location
            if(home != null)
            {
                if(path_finder.at_the_end || path_finder.path.Count == 0)
                {
                    int index = Random.Range(0, home.GetComponent<EntityHome>().home_nodes.Count);
                    path_finder.true_target = home.GetComponent<EntityHome>().home_nodes[index].gameObject;
                }
                path_finder.ControlPath();
            }
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

    private void LookAround()
    {
        if(!path_finder.at_the_end)
        {
            path_finder.ControlPath();
        } else
        {
            move_controller.StopMoving();
            if(behaviourCR == null)
            {
                behaviourCR = StartCoroutine(Rotate());
            }
        }
    }

    IEnumerator Rotate()
    {
        int x = 1;
        while(!sight.target_in_sight && x > 0)
        {
            move_controller.move_direction = RotateVector(move_controller.move_direction, 45f);
            move_controller.FaceTowards(path_finder.target);
            yield return new WaitForSeconds(1f);

            move_controller.move_direction = RotateVector(move_controller.move_direction, 90f);
            move_controller.FaceTowards(path_finder.target);
            yield return new WaitForSeconds(1f);

            move_controller.move_direction = RotateVector(move_controller.move_direction, -90f);
            move_controller.FaceTowards(path_finder.target);
            yield return new WaitForSeconds(1f);

            move_controller.move_direction = RotateVector(move_controller.move_direction, -45f);
            move_controller.FaceTowards(path_finder.target);
            yield return new WaitForSeconds(1f);

            move_controller.move_direction = RotateVector(move_controller.move_direction, -90f);
            move_controller.FaceTowards(path_finder.target);
            yield return new WaitForSeconds(1f);

            move_controller.move_direction = RotateVector(move_controller.move_direction, 90f);
            move_controller.FaceTowards(path_finder.target);
            yield return new WaitForSeconds(1f);

            x--;
        }
        if(!sight.target_in_sight)
        {
            aggro = false;
            path_finder.true_target = null;
            target_available = false;
        }
    }

    Vector2 RotateVector(Vector2 v, float angleDegrees)
    {
        float radians = angleDegrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
    }

    private void RecognizeHome(GameObject obj)
    {
        if (home == null && obj.GetComponent<EntityHome>())
        {
            home = obj;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        RecognizeHome(collision.gameObject);
    }

}
