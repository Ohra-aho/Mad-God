using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] string name;
    [SerializeField] string description;
    private void Awake()
    {
        Action action = GetComponent<Action>();
        action.name = "Attack";
        action.description = "Deals damage";
        action.action = AttackAction;
        action.Initiate();
    }

    public void AttackAction()
    {
        Debug.Log("Attacking");
    }
}
