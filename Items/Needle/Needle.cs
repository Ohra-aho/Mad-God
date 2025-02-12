using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Needle : MonoBehaviour
{
    public void Inisiate()
    {
        Item item = GetComponent<Item>();
        item.GiveData(
            "Needle",
            "It is in good enough condition.",
            new List<string> { "Poke" },
            new List<Item.Use> { Poke }
           );
    }

    public void Poke()
    {
        Debug.Log("Sharp needle");
    }
}
