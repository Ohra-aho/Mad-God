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
        GetComponent<Interactable>().items = SortByName(GetComponent<Interactable>().items);
        SetInteractionMessage();
        
        infoBox = GameObject.Find("Default UI").transform.GetChild(0).GetComponent<InfoBox>();
    }

    private void SetInteractionMessage()
    {
        string searchMessage = "You find: ";
        for (int i = 0; i < GetComponent<Interactable>().items.Count; i++)
        {
            //Invoke item so it works correctly
            GetComponent<Interactable>().items[i].on_inisiate.Invoke();

            //Name the items found. If find multiple of same item, display items name only ones
            if (!searchMessage.Contains(GetComponent<Interactable>().items[i].name))
            {
                if (GetComponent<Interactable>().items.Count == 1)
                {
                    searchMessage += GetComponent<Interactable>().items[i].name;
                }
                else
                {
                    int duplicates = CountSameItems(GetComponent<Interactable>().items, GetComponent<Interactable>().items[i].name);
                    i += duplicates - 1;
                    if (i < GetComponent<Interactable>().items.Count - 2)
                    {
                        if (duplicates > 1)
                            searchMessage += GetComponent<Interactable>().items[i].name + " x " + duplicates + ", ";
                        else
                            searchMessage += GetComponent<Interactable>().items[i].name + ", ";
                    }
                    else if (i == GetComponent<Interactable>().items.Count - 2)
                    {
                        if (duplicates > 1)
                            searchMessage += GetComponent<Interactable>().items[i].name + " x " + duplicates + " and ";
                        else
                            searchMessage += GetComponent<Interactable>().items[i].name + " and ";
                    }
                    else if (i == GetComponent<Interactable>().items.Count - 1)
                    {
                        if (duplicates > 1)
                            searchMessage += GetComponent<Interactable>().items[i].name + " x " + duplicates;
                        else
                            searchMessage += GetComponent<Interactable>().items[i].name;
                    }
                }
            }
        }

        if (GetComponent<Interactable>().items.Count == 0)
        {
            searchMessage = "It's empty";
        }

        GetComponent<Interactable>().messages.Add(
            new InfoBox.Message()
            {
                Title = name,
                Content = searchMessage
            }
        );
    }

    //Set box to be empty and add all its content to players inventory
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

    //Return how mutch of item with the same name is in a list
    private int CountSameItems(List<Item> items, string name)
    {
        int temp = 0;
        for(int i = 0; i < items.Count; i++)
        {
            if(items[i].name == name)
            {
                temp++;
            }
        }
        return temp;
    }

    private List<Item> SortByName(List<Item> items)
    {
        List<Item> temp = new List<Item>();
        
        for(int i = 0; i < items.Count; i++)
        {
            //Check if item in question is alredy sorted
            bool already_sorted = false;
            foreach(Item item in temp)
            {
                if(item.name == items[i].name)
                {
                    already_sorted = true;
                    break;
                }
            }

            if(!already_sorted)
            {
                foreach(Item item in items)
                {
                    if (item.name == items[i].name)
                        temp.Add(items[i]);    
                }
            }
        }

        return temp;
    }
}
