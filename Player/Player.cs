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
}
