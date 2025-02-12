using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLayer : MonoBehaviour
{
    //Add to each layer to be used in layermenu
    public void ChangeLayer(int index)
    {
        LayeredMenu LM = transform.parent.gameObject.GetComponent<LayeredMenu>();
        Destroy(LM.currentLayer);
        LM.currentLayer = Instantiate(LM.layers[index], this.transform.parent);
    }
}

