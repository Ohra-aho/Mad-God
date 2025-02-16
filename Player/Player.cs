using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject interact_target;

    public List<Item> items;

    //Combat shit
    public GameObject base_actions;
    public GameObject right_hand;
    public GameObject left_hand;
    public GameObject left_leg;
    public GameObject right_leg;

    private void Start()
    {
        items = new List<Item>();
    }
}
