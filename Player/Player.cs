using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite battle_sprite;
    public GameObject interact_target;

    public List<Item> items;

    [HideInInspector] public int Max_HP = 100;
    [HideInInspector] public int max_focus = 5;
    [HideInInspector] public int HP = 100;
    [HideInInspector] public int focus = 5;

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
