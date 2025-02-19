using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Limb : MonoBehaviour
{
    public int max_hp;
    public int current_hp;
    public int defence;
    public List<GameObject> actions;

    private void Awake()
    {
        current_hp = max_hp;
        defence = 0;
        GetComponent<Button>().onClick.AddListener(TargetThis);
    }

    public void TargetThis()
    {
        int damage = transform.parent.GetComponent<BattleEnemy>().csd.damage;
        TakeDamage(damage);
        transform.parent.GetComponent<BattleEnemy>().ConfirmPlayerAttack();
    }

    public void TakeDamage(int damage)
    {
        int true_damage = damage - defence;
        if(true_damage < 0)
        {
            true_damage = 0;
        }
        current_hp -= true_damage;
    }

    public void TakeAnAction(int? index)
    {
        //If no index given, choose random action
        Debug.Log("Limb action");
        actions[index ?? Random.Range(0, actions.Count)].GetComponent<EnemyAction>().MakeAction();
    }
}
