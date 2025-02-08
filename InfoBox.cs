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

    public List<Message> incomming_messages = new List<Message>();

    //Displays info box and eventuallu shows event description. All further functionalities start from here
    public void SetText(GameObject trigger)
    {
        title = incomming_messages[0].Title;
        description = incomming_messages[0].Content;
        if (eventTrigger != trigger) eventTrigger = trigger;
        incomming_messages.RemoveAt(0);

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";

        ClearChoiseList();
        transform.GetChild(2).gameObject.SetActive(false);

        if (textDisplay != null) StopCoroutine(textDisplay);
        textDisplay = DisplayText(title, description);
        StartCoroutine(textDisplay);
    }

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
            //If all messages are displayed, close message window and call InitialInteraction
            if(incomming_messages.Count == 0)
            {
                player = GameObject.Find("Player").GetComponent<Controls>();
                player.immobal = false;
                ClearChoiseList();

                // At the end of first interaction, invoke initial interaction
                if (eventTrigger.GetComponent<Interactable>())
                    if (eventTrigger.GetComponent<Interactable>().initial_interaction != null)
                        eventTrigger.GetComponent<Interactable>().InitialInteraction();

                transform.GetChild(2).gameObject.SetActive(false);
                this.gameObject.SetActive(false);
            } else
            {
                //If more messages, set another one
                SetText(eventTrigger);
            }
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

    public void AddMessage(string title, string content)
    {
        incomming_messages.Add(
            new Message() { Title = title, Content = content }
        );
    }

    public class Message
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
