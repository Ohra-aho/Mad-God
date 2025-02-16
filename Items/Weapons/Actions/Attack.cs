using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Attack : MonoBehaviour
{
    [HideInInspector] public float damage_modifier = 0;
    [HideInInspector] public float defence_modifier = 0;
    [HideInInspector] public int focus_cost = 0;
    [HideInInspector] public int focus_modifier = 0;

    public int attack_id = 0;

    public void Initiate()
    {
        Action action = GetComponent<Action>();
        ChooseAttack(action);
    }

    private string BaseAttackDescription(int base_damage)
    {
        int x = 0;
            if(damage_modifier != 0) x = (int)Math.Round(base_damage * damage_modifier);
            else x = base_damage;
        return "FC: " + focus_cost + "\nDeals " + x + " damage";
    }

    private string BaseBlockDescription(int base_block)
    {
        int x = 0;
        if (defence_modifier != 0) x = (int)Math.Round(base_block * defence_modifier);
        else x = base_block;
        return "FC: " + focus_cost + "\nBlocks " + x + " damage";
    }

    private void ChooseAttack(Action action) {
        switch(attack_id)
        {
            //Something wrong with this!! action is null for some reason
            case 0:
                action.action = Strike;
                focus_cost = 2 + focus_modifier;
                action.name = "Strike";
                action.description = BaseAttackDescription(100);
                break;
            case 1:
                action.action = Block;
                focus_cost = 2 + focus_modifier;
                action.name = "Block";
                action.description = BaseBlockDescription(100);
                break;
        }
    }


    // id: 0
    public void Strike()
    {
        Debug.Log("Attacking");
    }

    public void Block()
    {
        Debug.Log("Blocking");
    }
}
