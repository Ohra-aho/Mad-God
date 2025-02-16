using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour
{
    public void Inisiate()
    {
        Item item = GetComponent<Item>();
        item.GiveData(
            "Club",
            "Crude weapon, but sufficient to bash someones head in with.",
            null, null
           );
    }
}
