using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<int> attack_ids; //Determines specific attak actions
    public List<GameObject> actions; //Actions available in combat
    public GameObject attack_prefab;
    public float damage_modifier; //number damage of an action is multiplied by
    public float defence_modifier; //number defence is multiplied by
    public int focus_modifier; //increases focus cost or decreases it


    public void Initiate()
    {
        for (int i = 0; i < actions.Count; i++)
        {
            if (actions[i].GetComponent<Attack>())
            {
                actions[i].GetComponent<Attack>().damage_modifier = damage_modifier;
                actions[i].GetComponent<Attack>().defence_modifier = defence_modifier;
                actions[i].GetComponent<Attack>().focus_modifier = focus_modifier;
            }
        }
    }
}
