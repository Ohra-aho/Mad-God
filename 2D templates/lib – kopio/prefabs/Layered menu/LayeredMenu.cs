using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayeredMenu : MonoBehaviour
{
    //Keeps track of layers in the menu
    public List<GameObject> layers;
    [HideInInspector] public GameObject currentLayer;

    void Start()
    {
        currentLayer = Instantiate(layers[0], this.transform);
    }
}
