using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoiseStatDisplay : MonoBehaviour
{
    public int damage;
    public int defence;
    public int spent_focus;
    public void ResetStats()
    {
        damage = 0;
        defence = 0;
        DisplayStats();
    }

    public void DisplayStats()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Damage: " + damage;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Defence: " + defence;
    }

    public void AddToStats(int add_damage, int add_defence, int focus_cost)
    {
        damage += add_damage;
        defence += add_defence;
        spent_focus += focus_cost;
        DisplayStats();
    }

    public void Clear()
    {
        ResetStats();
        GameObject.Find("Player").GetComponent<Player>().focus += spent_focus;
        spent_focus = 0;
        transform.parent.parent.GetComponent<BattleMenu>().SetPlayerInfo();
    }

    public void Confirm()
    {
        Debug.Log("Confirmed");
    }
}
