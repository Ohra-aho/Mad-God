using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMenu : MonoBehaviour
{
    [SerializeField] GameObject button;
    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
