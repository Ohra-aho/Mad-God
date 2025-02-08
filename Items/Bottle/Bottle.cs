using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public void Inisiate()
    {
        GetComponent<Item>().use_item = Fill;
        GetComponent<Item>().name = "Bottle";
        GetComponent<Item>().description = "Empty glass bottle";
    }

    public void Fill()
    {
        Debug.Log("You fill it");
    }

}
