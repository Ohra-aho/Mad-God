using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public int damage;

    private void Start()
    {
        GetComponent<EnemyAction>().sub_actions.Add(Damage);
    }

    public void Damage()
    {
        Debug.Log("Deal " + damage +" damage");
    }
}
