using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    public InputAction movement;

    private Vector2 move_direction;
    public float speed = 5f;

    public void OnEnable()
    {
        movement.Enable();
    }

    public void OnDisable()
    {
        movement.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move_direction = movement.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(move_direction.x * speed, move_direction.y * speed);
    }
}
