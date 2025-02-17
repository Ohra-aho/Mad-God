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
    ChoiseStatDisplay csd;
    Player player;
    public int attack_id = 0;

    public void Initiate()
    {
        Action action = GetComponent<Action>();
        csd = GameObject.Find("Chosen stat display").GetComponent<ChoiseStatDisplay>();
        player = GameObject.Find("Player").GetComponent<Player>();
        ChooseAttack(action);
    }

    private int CalculateDamage(int base_damage)
    {
        int x = 0;
        if (damage_modifier != 0) x = (int)Math.Round(base_damage * damage_modifier);
        else x = base_damage;
        return x;
    }

    private int CalculateDefence(int base_defence)
    {
        int x = 0;
        if (defence_modifier != 0) x = (int)Math.Round(base_defence * defence_modifier);
        else x = base_defence;
        return x;
    }

    private string BaseAttackDescription(int base_damage)
    {
        int x = CalculateDamage(base_damage);
        return "FC: " + focus_cost + "\nDeals around" + x + " damage";
    }

    private string BaseBlockDescription(int base_block)
    {
        int x = CalculateDefence(base_block);
        return "FC: " + focus_cost + "\nBlocks around" + x + " damage";
    }

    private void ChooseAttack(Action action) {
        switch(attack_id)
        {
            //Something wrong with this!! action is null for some reason
            case 0:
                focus_cost = 2 + focus_modifier;
                action.action = () => { 
                    Strike(100, 0);
                };
                action.name = "Strike";
                action.description = BaseAttackDescription(100);
                break;
            case 1:
                focus_cost = 2 + focus_modifier;
                action.action = () => { 
                    Block(0, 100);
                };
                action.name = "Block";
                action.description = BaseBlockDescription(100);
                break;
        }
    }

    //Returns false if not enough focus to use
    private bool SubtractFocus()
    {
        if(player.focus >= focus_cost)
        {
            player.focus -= focus_cost;
            transform.parent.parent.parent.parent.GetComponent<BattleMenu>().SetPlayerInfo();
        }
        else
        {
            return false;
        }
        return true;
    }

    // id: 0
    public void Strike(int base_damage, int base_defence)
    {
        if (SubtractFocus())
        {
            csd.AddToStats(
            CalculateDamage(base_damage),
            CalculateDefence(base_defence),
            focus_cost
            );
        }
        

    }

    // id: 1
    public void Block(int base_damage, int base_defence)
    {
        if (SubtractFocus())
        {
            csd.AddToStats(
            CalculateDamage(base_damage),
            CalculateDefence(base_defence),
            focus_cost
            );
        }
    }
}
