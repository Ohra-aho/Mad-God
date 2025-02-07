using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    InfoBox infoBox;

    string name = "Wooden box";
    string description = "You search the box...";

    private void Awake()
    {
        GetComponent<Interactable>().name = name;
        GetComponent<Interactable>().description = description;
        GetComponent<Interactable>().initial_interaction = Search;
        GetComponent<Interactable>().Inisiate();
        GetComponent<Interactable>().messages.Add(
            new InfoBox.Message()
            {
                Title = name,
                Content = "You find: propably something good???"
            }
        );
        infoBox = GameObject.Find("Default UI").transform.GetChild(0).GetComponent<InfoBox>();
    }

    public void Search()
    {
        GetComponent<Interactable>().messages.Clear();
        GetComponent<Interactable>().messages.Add(
            new InfoBox.Message()
            {
                Title = name,
                Content = "It's empty"
            }
        );

        GameObject player = GameObject.Find("Player");
        player.GetComponent<Player>().items.AddRange(GetComponent<Interactable>().items);
        GetComponent<Interactable>().items.Clear();
    }
}
