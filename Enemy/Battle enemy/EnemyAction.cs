using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAction : MonoBehaviour
{
    public int damage;
    public int block;
    public List<UnityAction> sub_actions = new List<UnityAction>();

    public void MakeAction()
    {
        for(int i = 0; i < sub_actions.Count; i++)
        {
            sub_actions[i].Invoke();
        }
    }

    public void DealDamage()
    {
        Debug.Log("Deal " + damage + " damage");
    }

    public void BlockDamage()
    {
        Debug.Log("Block " + block + " damage");
    }
}
