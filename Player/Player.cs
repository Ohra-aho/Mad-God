using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject interact_target;

    public List<Item> items;

    private void Start()
    {
        items = new List<Item>();
    }

    /*public void PickUpItem()
    {
        if(interact_target.GetComponent<Interactable>().items != null)
        {
            items.AddRange(interact_target.GetComponent<Interactable>().items);
            interact_target.GetComponent<Interactable>().items.Clear();
        }   
    }*/
}
