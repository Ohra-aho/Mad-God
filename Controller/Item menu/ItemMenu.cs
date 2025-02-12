using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenu : MonoBehaviour
{
    Player player;
    [SerializeField] GameObject item;

    public void Display()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        GameObject scroll = transform.GetChild(0).gameObject;
        scroll.GetComponent<ScrollView>().DisplayItems(player.items, item);
    }
}
