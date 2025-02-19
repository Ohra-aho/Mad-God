using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleEnemy : MonoBehaviour
{
    [HideInInspector] public ChoiseStatDisplay csd;
    // Start is called before the first frame update
    private void Awake()
    {
        csd = transform.parent.parent.parent.GetChild(2).GetComponent<ChoiseStatDisplay>();
        if(transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                //transform.GetChild(i).GetComponent<Button>().onClick.AddListener(ConfirmAttack);
            }
        }
    }

    public void TakeATurn()
    {
        //Could add some scripted behaviour
        Debug.Log("Enemy turn");
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Limb>().TakeAnAction(null);
            }
        }
    }

    public void ConfirmPlayerAttack()
    {
        csd.Confirm();
    }
}
