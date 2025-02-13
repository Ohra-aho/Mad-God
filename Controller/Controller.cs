using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public InputAction open_menu;
    PauseMenu pause_menu;
    
    public bool stop = false;

    public void OnEnable()
    {
        open_menu.Enable();
        open_menu.performed += OnEPressed;
    }

    public void OnDisable()
    {
        open_menu.Disable();
        open_menu.performed -= OnEPressed;
    }

    private void Start()
    {
        pause_menu = GameObject.Find("Pause Menu").GetComponent<PauseMenu>();

    }

    private void OnEPressed(InputAction.CallbackContext context)
    {
        if (Time.timeScale > 0) Time.timeScale = 0;
        else Time.timeScale = 1;
        pause_menu.DisplayMenu();
    }

    public void ToggleStop()
    {
        stop = !stop;
    }
}
