using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMenu : MonoBehaviour
{
    [SerializeField] GameObject button;
    Player player;
    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
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
}
