using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public string name;
    public string description;

    public List<InfoBox.Message> messages = new List<InfoBox.Message>();

    public List<Item> items;
    bool interacted = false;

    public UnityAction initial_interaction;

    private void Start()
    {
        //items = new List<Item>(); for now

    }

    private void Awake()
    {
        
        
    }

    public void Inisiate()
    {
        messages.Add(new InfoBox.Message()
        {
            Title = name,
            Content = description
        });
    }

    public void InitialInteraction()
    {
        if(!interacted) initial_interaction.Invoke();
        interacted = true;
    }
}
