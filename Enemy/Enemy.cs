using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool on_cooldown = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() && !on_cooldown)
        {
            on_cooldown = true;
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            DisplayBattleMenu();
        }
    }

    public void DisplayBattleMenu()
    {
        GameObject.Find("Controller").GetComponent<Controller>().stop = true;
        GameObject battle_menu = GameObject.Find("Battle Menu");
        battle_menu.transform.GetChild(0).gameObject.SetActive(true);
    }
}
