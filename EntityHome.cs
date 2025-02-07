using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHome : MonoBehaviour
{

    public List<PathNode> home_nodes = new List<PathNode>();

    int delay = 60;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Remove components whith are no longer needed
        if(delay > 0) delay--;
        if(delay == 0)
        {
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<Rigidbody2D>());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<PathNode>())
        {
            home_nodes.Add(collision.GetComponent<PathNode>());
        }
    }
}
