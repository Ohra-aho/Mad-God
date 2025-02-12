using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItem : MonoBehaviour
{
    GameObject info_box;

    private void Awake()
    {
        info_box = transform.parent.parent.GetChild(1).gameObject;
    }

}
