using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Needle : MonoBehaviour
{
    public void Inisiate()
    {
        GetComponent<Item>().use_item = Poke;
        GetComponent<Item>().name = "Needle";
        GetComponent<Item>().description = "It is in good enough condition.";
    }

    public void Poke()
    {
        Debug.Log("You fill it");
    }
}
