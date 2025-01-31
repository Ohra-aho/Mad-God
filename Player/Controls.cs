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
        DefaultUI = GameObject.Find("Default UI");
    }

    // Update is called once per frame
    void Update()
    {
        move_direction = movement.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if(!immobal)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(move_direction.x * speed, move_direction.y * speed);

            if (move_direction == new Vector2(0, 1))
            {
                ChangeInteractionDirection(0);
            }
            else if (move_direction == new Vector2(1, 0))
            {
                ChangeInteractionDirection(1);
            }
            else if (move_direction == new Vector2(0, -1))
            {
                ChangeInteractionDirection(2);
            }
            else if (move_direction == new Vector2(-1, 0))
            {
                ChangeInteractionDirection(3);
            }
        }
    }

    private void ChangeInteractionDirection(int child)
    {
        //Deactivate all interactors
        for(int i = 0; i < interaction_holder.transform.childCount; i++)
        {
            if (interaction_holder.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                interaction_holder.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        //Activate interactor corresponing to movedirection
        if(!interaction_holder.transform.GetChild(child).gameObject.activeInHierarchy)
        {
            interaction_holder.transform.GetChild(child).gameObject.SetActive(true);
        }
    }

    private void OnSpacePressed(InputAction.CallbackContext context)
    {
        Interact();
    }
    public void Interact()
    {

        if (GetComponent<Player>().interact_target != null)
        {
            immobal = true;
            DisplayInteractInfo(
                GetComponent<Player>().interact_target
            );
        }
    }

    private void DisplayInteractInfo(GameObject info)
    {
        GameObject infoBox;

        if (DefaultUI.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            infoBox = DefaultUI.transform.GetChild(0).gameObject;
            infoBox.GetComponent<InfoBox>().StopTextDisplay();
        } else
        {
            DefaultUI.transform.GetChild(0).gameObject.SetActive(true);
            infoBox = DefaultUI.transform.GetChild(0).gameObject;

            infoBox.GetComponent<InfoBox>().SetText(
                info.GetComponent<Interactable>().name,
                info.GetComponent<Interactable>().description,
                info
            );
        }

        
    }
}
