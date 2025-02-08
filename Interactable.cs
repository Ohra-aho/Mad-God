using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [HideInInspector] public string name;
    [HideInInspector] public string description;

    public List<InfoBox.Message> messages = new List<InfoBox.Message>();

    public List<Item> items;
    bool interacted = false;

    [HideInInspector] public UnityAction initial_interaction;

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
