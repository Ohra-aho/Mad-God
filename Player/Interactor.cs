using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Interactable>()) transform.parent.parent.GetComponent<Player>().interact_target = other.gameObject;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Interactable>()) transform.parent.parent.GetComponent<Player>().interact_target = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        transform.parent.parent.GetComponent<Player>().interact_target = null;
    }
}
