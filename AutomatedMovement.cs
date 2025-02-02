using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomatedMovement : MonoBehaviour
{

    public Vector2 move_direction;
    public float speed = 1f;
    public bool immobal = false;
    Animator animator;
    GameObject visionCone;

    bool facing_left;
    private void Awake()
    {
        move_direction = new Vector2(0, 0);
        animator = GetComponent<Animator>();
        visionCone = transform.GetChild(0).gameObject;
    }

    public void FaceTowards(GameObject target)
    {
        if(!immobal)
        {
            move_direction = (target.transform.position - transform.position).normalized;
            if (move_direction.x != 0)
                facing_left = move_direction.x < 0;
        }
        ControlAnimation(move_direction);

    }

    public void RotateVisionCone()
    {
        float angle = Mathf.Atan2(move_direction.y, move_direction.x) * Mathf.Rad2Deg;
        visionCone.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    public void Move()
    {
        if (!immobal)
        {
            GetComponent<SpriteRenderer>().flipX = facing_left;
            GetComponent<Rigidbody2D>().velocity = new Vector2(move_direction.x * speed, move_direction.y * speed);
        } else
        {
            GetComponent<SpriteRenderer>().flipX = facing_left;
            move_direction = Vector2.zero;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void ControlAnimation(Vector2 direction)
    {
        if (move_direction != Vector2.zero)
        {
            animator.SetFloat("lastXvelocity", -move_direction.x);
            animator.SetFloat("lastYvelocity", move_direction.y);

            RotateVisionCone();
        }
        if (direction == Vector2.zero)
        {
            animator.SetBool("Moving", false);
        }
        else if (animator.GetBool("Moving") != true)
        {
            animator.SetBool("Moving", true);
            animator.SetFloat("xVelocity", -direction.x);
            animator.SetFloat("yVelocity", direction.y);
        }
        else
        {
            animator.SetFloat("xVelocity", -direction.x);
            animator.SetFloat("yVelocity", direction.y);
        }

    }
}
