using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class InfoBox : MonoBehaviour
{
    //public InputAction space;
    private bool displayInProcess;
    IEnumerator textDisplay;
    string title;
    string description;

    Controls player;

    /*public void OnEnable()
    {
        space.Enable();
        space.performed += OnSpacePressed;
    }

    public void OnDisable()
    {
        space.Disable();
        space.performed -= OnSpacePressed;
    }*/
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
        displayInProcess = false;
    }

    // Optional: Allows changing the string dynamically
    public void SetText(string displayTitle, string displayDescription)
    {

        title = displayTitle;
        description = displayDescription;

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";

        if(textDisplay != null) StopCoroutine(textDisplay);
        textDisplay = DisplayText(displayTitle, displayDescription);
        StartCoroutine(textDisplay);
    }

    public void StopTextDisplay()
    {
        if(displayInProcess)
        {
            StopCoroutine(textDisplay);
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = description;
            displayInProcess = false;
        } else
        {
            player = GameObject.Find("Player").GetComponent<Controls>();
            player.immobal = false;
            this.gameObject.SetActive(false);
        }
    }
}
