using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChoiseEvent : MonoBehaviour
{
    public List<UnityAction> events = new List<UnityAction>();
    public List<string> titles;

    public void InvokeEvent(int index)
    {
        events[index].Invoke();
    }

    public void AddEvents(List<UnityAction> actions)
    {
        events.AddRange(actions);
    }

    public void AddTitles(List<string> title)
    {
        titles.AddRange(title);
    }

    public void ClearEvents()
    {
        events.Clear();
    }

    public void ClearTitles()
    {
        titles.Clear();
    }
}
