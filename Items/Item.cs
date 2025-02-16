using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [HideInInspector] public string name;
    [HideInInspector] public string description;

    [HideInInspector] public delegate void Use();
    public UnityEvent on_inisiate;
    [HideInInspector] public List<string> use_names = new List<string>();
    [HideInInspector] public List<Use> uses = new List<Use>();

    public void GiveData(string item_name, string item_description, List<string> names, List<Use> use)
    {
        if(use != null)  uses.AddRange(use);
        if(names != null) use_names.AddRange(names);
        uses.Add(Drop);
        use_names.Add("Drop");
        name = item_name;
        description = item_description;
    }

    public void Drop()
    {
        Debug.Log("Dropping item");
    }
}
