using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WierdOrgan : MonoBehaviour
{
    InfoBox infoBox;

    bool smashed = false;

    string name = "Wierd organ";
    string description = "Organ you don't recognize. It pulses slightly.";
    private void Awake()
    {
        GetComponent<ChoiseEvent>().AddEvents(new List<UnityAction> { PokeIt, SmashIt, LeaveItBe });
        GetComponent<ChoiseEvent>().AddTitles(new List<string> { "Touch it", "Smash it", "Leave it be" });
        GetComponent<Interactable>().name = name;
        GetComponent<Interactable>().description = description;
        GetComponent<Interactable>().Inisiate();

        infoBox = GameObject.Find("Default UI").transform.GetChild(0).GetComponent<InfoBox>();

    }
    public void PokeIt()
    {
        if(smashed)
        {
            infoBox.AddMessage(name, "Warm yellow liquid stains your fingers. It feels stiky and irritates your skin.");
            infoBox.SetText(this.gameObject);
        } else
        {
            infoBox.AddMessage(name, "Organ feels warm and dense. Something seemes to be flowing through it.");
            infoBox.SetText(this.gameObject);
        }
        
    }

    public void SmashIt()
    {
        smashed = true;
        GetComponent<ChoiseEvent>().ClearEvents();
        GetComponent<ChoiseEvent>().ClearTitles();

        GetComponent<ChoiseEvent>().AddEvents(new List<UnityAction> { PokeIt, LeaveItBe });
        GetComponent<ChoiseEvent>().AddTitles(new List<string> { "Touch it", "Leave it be" });

        GetComponent<Interactable>().name = "Smashed organ";
        GetComponent<Interactable>().description = "Yellowish liquid slowley flows from ruptured organ.";
        name = GetComponent<Interactable>().name;
        description = GetComponent<Interactable>().description;

        infoBox.AddMessage(name, "You smash the organ and a lot of yellowish liquid spurts from it.");
        infoBox.SetText(this.gameObject);
       
    }

    public void LeaveItBe()
    {
        infoBox.DeactivateInfoBox();
    }
}
