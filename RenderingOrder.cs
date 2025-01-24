using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderingOrder : MonoBehaviour
{
    public bool entity;

    public int order;

    GameObject controller;

    public float colliderApexPosition;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Controller");
        CalculatePositionOfCollider();
    }

    // Update is called once per frame
    void Update()
    {
        //ChangeSortingOrder();
    }

    public void ChangeSortingOrder()
    {

        //Debug.Log(GetComponent<SpriteRenderer>().sortingLayerName);
        //Debug.Log(GetComponent<SpriteRenderer>().sortingOrder);
        List<GameObject> entities = controller.GetComponent<Controller>().entities;
        if(entity)
        {
            Debug.Log(name);
            CalculatePositionOfCollider();
        }
        for (int i = 0; i < entities.Count; i++)
        {
            if(entities[i] != this.gameObject)
            {
                if (colliderApexPosition < entities[i].GetComponent<RenderingOrder>().colliderApexPosition)
                {
                    GetComponent<SpriteRenderer>().sortingOrder = entities[i].GetComponent<SpriteRenderer>().sortingOrder + 1;
                } else if(colliderApexPosition > entities[i].GetComponent<RenderingOrder>().colliderApexPosition)
                {
                    GetComponent<SpriteRenderer>().sortingOrder = entities[i].GetComponent<SpriteRenderer>().sortingOrder - 1;
                }
            }
        }

    }

    //Could still be usefull
    private void CalculatePositionOfCollider()
    {
        colliderApexPosition = transform.position.y - transform.localScale.y / 2 + transform.localScale.y * GetComponent<BoxCollider2D>().size.y;
    }
}
