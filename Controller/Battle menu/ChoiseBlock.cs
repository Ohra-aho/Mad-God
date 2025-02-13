using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiseBlock : MonoBehaviour
{
    [SerializeField] GameObject action;
    public void DisplayActions(List<GameObject> actions)
    {
        for(int i = 0; i < actions.Count; i++)
        {
            GameObject new_action = Instantiate(action, transform);
            new_action.GetComponent<Action>().Initiate();
        }
    }

}
