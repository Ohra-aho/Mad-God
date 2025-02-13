using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Action : MonoBehaviour
{
    public delegate void ThisAction();
    public ThisAction action;
    public Sprite sprite;
    public string name;
    public string description;

    public void Initiate()
    {
        if(sprite != null) GetComponent<Image>().sprite = sprite;
        GetComponent<Button>().onClick.AddListener(() => action.Invoke());
    }

}
