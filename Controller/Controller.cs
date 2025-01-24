using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public List<GameObject> entities;

    public void CollectEntities()
    {
        entities.Clear();

        // Find all GameObjects in the scene
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if(obj.GetComponent<RenderingOrder>())
            {
                if (obj.GetComponent<RenderingOrder>().entity)
                {
                    entities.Add(obj);
                    //Debug.Log(obj.name);
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        CollectEntities();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
