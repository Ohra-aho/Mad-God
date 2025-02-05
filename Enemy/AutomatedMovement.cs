using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomatedMovement : MonoBehaviour
{

    public Vector2 move_direction;
    public float speed = 1f;
    [HideInInspector] public bool immobal = false;
    Animator animator;
    GameObject visionCone;

    bool facing_left;

    private void Awake()
    {
        move_direction = new Vector2(0, 1);
        animator = GetComponent<Animator>();
        visionCone = transform.GetChild(0).gameObject;
        //immobal = true;
    }

    private void FixedUpdate()
    {
        ControlAnimation();
    }

    public void FaceTowards(PathNode target)
    {
        if(target != null)
        {
            move_direction = (target.transform.position - transform.position).normalized;
            if (move_direction.x != 0)
                facing_left = move_direction.x < 0;
        }
        RotateVisionCone(
                transform.GetChild(0).GetChild(0).GetComponent<PlayerDetector>().player
            );
    }

    public void RotateVisionCone(GameObject target)
    {
        if(!transform.GetChild(0).GetChild(0).GetComponent<PlayerDetector>().target_in_sight)
        {
            float angle = Mathf.Atan2(move_direction.y, move_direction.x) * Mathf.Rad2Deg;
            visionCone.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        } else
        {
            Vector2 target_position = (target.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(target_position.y, target_position.x) * Mathf.Rad2Deg;
            visionCone.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }

    public void Move()
    {
        if (!immobal)
        {
            GetComponent<SpriteRenderer>().flipX = facing_left;
            GetComponent<Rigidbody2D>().velocity = new Vector2(move_direction.x * speed, move_direction.y * speed);
        } else
        {
            StopMoving();
        }
    }

    public void StopMoving()
    {
        GetComponent<SpriteRenderer>().flipX = facing_left;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void NormalizeMovement()
    {
        GetComponent<SpriteRenderer>().flipX = facing_left;
        move_direction = Vector2.zero;
    }

    private void ControlAnimation()
    {
        if (GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            animator.SetFloat("lastXvelocity", -move_direction.x);
            animator.SetFloat("lastYvelocity", move_direction.y);
            animator.SetBool("Moving", false);
        }
        /*if (immobal)
        {
            animator.SetBool("Moving", false);
        }*/
        else if (animator.GetBool("Moving") != true)
        {
            animator.SetBool("Moving", true);
            animator.SetFloat("xVelocity", -move_direction.x);
            animator.SetFloat("yVelocity", move_direction.y);
        }
        else
        {
            animator.SetFloat("xVelocity", -move_direction.x);
            animator.SetFloat("yVelocity", move_direction.y);
        }

    }
}
