using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    //Will need some refining or will lead to repeated code
    public void Inisiate()
    {
        Item item = GetComponent<Item>();
        item.GiveData(
            "Bottle",
            "Empty glass bottle",
            new List<string> { "Fill", "Break", "Drop" },
            new List<Item.Use> { Fill, Break }
           );
    }

    public void Fill()
    {
        Debug.Log("You fill it");
    }

    public void Break()
    {
        Debug.Log("Break it");
    }

}
