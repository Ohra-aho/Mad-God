using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Action : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void ThisAction();
    public ThisAction action;
    public Sprite sprite;
    [HideInInspector] public string name;
    [HideInInspector] public string description;

    public void Initiate()
    {
        if(sprite != null) GetComponent<Image>().sprite = sprite;
        
        if(GetComponent<Attack>())
        {
            GetComponent<Attack>().Initiate();
            Button btn = GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            if (action != null)
            {
                btn.onClick.AddListener(() => action.Invoke());
            }
        }
        else
        {
            Debug.LogError("Not an attack");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Transform info_panel = transform.parent.parent.parent.GetChild(1);
        info_panel.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
        info_panel.GetChild(1).GetComponent<TextMeshProUGUI>().text = description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Transform info_panel = transform.parent.parent.parent.GetChild(1);
        info_panel.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        info_panel.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
    }
}
