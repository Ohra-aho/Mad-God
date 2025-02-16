using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiseBlock : MonoBehaviour
{
    [SerializeField] GameObject action;
    public void DisplayActions(List<GameObject> actions, GameObject action_container)
    {
        if(action_container.GetComponent<Weapon>())
        {
            //Each attack id creates a one attack action
            for (int i = 0; i < action_container.GetComponent<Weapon>().attack_ids.Count; i++)
            {
                GameObject new_action = Instantiate(action_container.GetComponent<Weapon>().attack_prefab, transform);
                new_action.GetComponent<Attack>().attack_id = action_container.GetComponent<Weapon>().attack_ids[i];
                new_action.GetComponent<Action>().Initiate();
            }
        }
        
    }

}
