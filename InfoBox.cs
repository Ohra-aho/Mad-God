using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InfoBox : MonoBehaviour
{
    private bool displayInProcess;
    IEnumerator textDisplay;
    string title;
    string description;

    GameObject eventTrigger;

    Controls player;

    public GameObject button;

    private IEnumerator DisplayText(string displayTitle, string displayDescription)
    {
        displayInProcess = true;

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = displayTitle;
        // Display the string one character at a time
        for (int i = 0; i < displayDescription.Length; i++)
        {
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().text += displayDescription[i];
            yield return new WaitForSeconds(0.05f);
        }
        ShowChoiseList();
        displayInProcess = false;
    }

    //Displays info box and eventuallu shows event description. All further functionalities start from here
    public void SetText(string displayTitle, string displayDescription, GameObject trigger)
    {
        title = displayTitle;
        description = displayDescription;
        eventTrigger = trigger;

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";

        ClearChoiseList();
        transform.GetChild(2).gameObject.SetActive(false);

        if (textDisplay != null) StopCoroutine(textDisplay);
        textDisplay = DisplayText(displayTitle, displayDescription);
        StartCoroutine(textDisplay);
    }

    public void StopTextDisplay()
    {
        if(displayInProcess)
        {
            StopCoroutine(textDisplay);
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = description;
            ShowChoiseList();
            displayInProcess = false;
        } else
        {
            player = GameObject.Find("Player").GetComponent<Controls>();
            player.immobal = false;
            ClearChoiseList();
            transform.GetChild(2).gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    public void ShowChoiseList()
    {
        if(eventTrigger.GetComponent<ChoiseEvent>())
        {
            transform.GetChild(2).gameObject.SetActive(true);
            for (int i = 0; i < eventTrigger.GetComponent<ChoiseEvent>().events.Count; i++)
            {
                GameObject newButton = Instantiate(button, transform.GetChild(2));
                newButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = eventTrigger.GetComponent<ChoiseEvent>().titles[i];
                newButton.GetComponent<Button>().onClick.AddListener(eventTrigger.GetComponent<ChoiseEvent>().events[i]);
            }
        }
    }

    private void ClearChoiseList()
    {
        if(transform.GetChild(2).childCount > 0)
        {
            int x = transform.GetChild(2).childCount;
            for (int i = 0; i < x; i++)
            {
                DestroyImmediate(transform.GetChild(2).GetChild(0).gameObject);
            }
        }
    }

    public void DeactivateInfoBox()
    {
        player = GameObject.Find("Player").GetComponent<Controls>();

        player.immobal = false;
        ClearChoiseList();
        transform.GetChild(2).gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
