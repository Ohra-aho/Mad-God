using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Controls : MonoBehaviour
{
    public InputAction movement;
    public InputAction interact;

    private Vector2 move_direction;
    public float speed = 5f;

    public GameObject interaction_holder;

    public bool immobal = false;

    GameObject DefaultUI;

    Animator animator;

    bool facing_left;

    Controller controller;

    public void OnEnable()
    {
        movement.Enable();
        interact.Enable();
        interact.performed += OnSpacePressed;
    }

    public void OnDisable()
    {
        movement.Disable();
        interact.Disable();
        interact.performed -= OnSpacePressed;
    }

    private void Awake()
    {
        move_direction = new Vector2(0, -1);
        DefaultUI = GameObject.Find("Default UI");
        animator = GetComponent<Animator>();
        controller = GameObject.Find("Controller").GetComponent<Controller>();
        StartCoroutine(DirectionBuffer());
    }

    // Update is called once per frame
    void Update()
    {
        if(!controller.stop)
        {
            move_direction = movement.ReadValue<Vector2>();
            if (move_direction.x != 0)
            {
                facing_left = move_direction.x < 0;
            }

            ControlAnimation(move_direction);
        }
    }

    private void FixedUpdate()
    {
        if(!controller.stop)
        {
            if (!immobal)
            {
                GetComponent<SpriteRenderer>().flipX = facing_left;
                GetComponent<Rigidbody2D>().velocity = new Vector2(move_direction.x * speed, move_direction.y * speed);
            }
        } else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private IEnumerator DirectionBuffer()
    {
        while(true)
        {
            if(move_direction != Vector2.zero)
            {
                
                animator.SetFloat("lastXvelocity", -move_direction.x);
                animator.SetFloat("lastYvelocity", move_direction.y);
                
                float angle = Mathf.Atan2(move_direction.y, move_direction.x) * Mathf.Rad2Deg;
                transform.GetChild(0).rotation = Quaternion.Euler(0, 0, angle - 90); // Adjust if sprite faces right
            } else
            {
                //ControlAnimation(move_direction);
            }
            yield return new WaitForSeconds(0.07f);
        }
    }

    private void ControlAnimation(Vector2 direction)
    {
        if (direction == Vector2.zero) {
            animator.SetBool("Moving", false);
        }
        else if (animator.GetBool("Moving") != true) {
            animator.SetBool("Moving", true);
            animator.SetFloat("xVelocity", -direction.x);
            animator.SetFloat("yVelocity", direction.y);
        } else
        {
            animator.SetFloat("xVelocity", -direction.x);
            animator.SetFloat("yVelocity", direction.y);
        }
        
    }

    private void OnSpacePressed(InputAction.CallbackContext context)
    {
        if(!controller.stop) Interact();
    }
    public void Interact()
    {

        if (GetComponent<Player>().interact_target != null)
        {
            immobal = true;
            DisplayInteractInfo(
                GetComponent<Player>().interact_target.GetComponent<Interactable>()
            );
        }
    }

    private void DisplayInteractInfo(Interactable info)
    {
        InfoBox infoBox;

        if (DefaultUI.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            infoBox = DefaultUI.transform.GetChild(0).gameObject.GetComponent<InfoBox>();
            infoBox.StopTextDisplay();
        }
        else
        {
            DefaultUI.transform.GetChild(0).gameObject.SetActive(true);
            infoBox = DefaultUI.transform.GetChild(0).gameObject.GetComponent<InfoBox>();

            infoBox.incomming_messages.AddRange(info.messages);

            infoBox.SetText(
                info.gameObject
            );
        }

        
    }
}
