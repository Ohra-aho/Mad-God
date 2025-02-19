using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleMenu : MonoBehaviour
{
    [SerializeField] GameObject button;
    Player player;
    public List<GameObject> enemies;

    public GameObject enemy_holder;

    public bool player_turn = true;
    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Update()
    {
        if(player_turn)
        {
            //Nothing really
        } else
        {
            //Some enemy behaviour shit
            for(int i = 0; i < enemy_holder.transform.childCount; i++)
            {
                if(enemy_holder.transform.GetChild(i).childCount > 0)
                {
                    enemy_holder.transform.GetChild(i).GetChild(0).GetComponent<BattleEnemy>().TakeATurn();
                }
            }
            player_turn = true;
        }
    }

    public void Initiate()
    {
        SetActions();
        SetPlayerInfo();
        DisplayEnemies();
    }

    public void SetActions()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        Transform main_choise_panel = transform.GetChild(0).GetChild(0);

        if (player.base_actions != null) {
            player.base_actions.GetComponent<Weapon>().Initiate();
            main_choise_panel.GetChild(0).gameObject.GetComponent<ChoiseBlock>().DisplayActions(
                player.base_actions.GetComponent<Weapon>().actions,
                player.base_actions
            ); 
        }
        if (player.right_hand != null)
        {
            player.right_hand.GetComponent<Weapon>().Initiate();
            main_choise_panel.GetChild(1).gameObject.GetComponent<ChoiseBlock>().DisplayActions(
                player.right_hand.GetComponent<Weapon>().actions,
                 player.right_hand
            );
        }
        //Make something better for this
        //if (player.left_hand != null) main_choise_panel.GetChild(2).gameObject.GetComponent<ChoiseBlock>().DisplayActions(player.left_hand.GetComponent<Weapon>().actions);
        //if (player.right_leg != null) main_choise_panel.GetChild(3).gameObject.GetComponent<ChoiseBlock>().DisplayActions(player.right_leg.GetComponent<Weapon>().actions);
        //if (player.left_leg != null) main_choise_panel.GetChild(4).gameObject.GetComponent<ChoiseBlock>().DisplayActions(player.left_leg.GetComponent<Weapon>().actions);

    }

    public void SetPlayerInfo()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        Transform character = transform.GetChild(0).GetChild(3);
        character.GetChild(0).GetComponent<Image>().sprite = player.battle_sprite;
        character.GetChild(1).GetComponent<TextMeshProUGUI>().text = "HP: " + player.HP;
        character.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Focus: " + player.focus;
    }

    public void DisplayEnemies()
    {
        Transform enemy_holder = transform.GetChild(0).GetChild(4);
        for (int i = 0; i < enemies.Count; i++)
        {
            GameObject new_enemy = Instantiate(enemies[i], enemy_holder.GetChild(i));
        }
    }
}
