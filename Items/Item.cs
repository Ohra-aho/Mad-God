using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [HideInInspector] public string name;
    [HideInInspector] public string description;

    public delegate void Use();
    public Use use_item;
    public UnityEvent on_inisiate;
}
