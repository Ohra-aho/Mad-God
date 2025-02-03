using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    [SerializeField] GameObject node;
    public int width;
    public int height;

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        int child_index = 0;
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                GameObject new_node = Instantiate(node, transform);
                new_node.transform.position = new Vector2(transform.position.x + i, transform.position.y + j);

                //Previous
                if(i != 0)
                {
                    new_node.GetComponent<PathNode>().connections.Add(
                            transform.GetChild(child_index - 1).GetComponent<PathNode>()
                        );
                    transform.GetChild(child_index - 1).GetComponent<PathNode>().connections.Add(new_node.GetComponent<PathNode>());
                }
                if(j != 0)
                {
                    //Straight down
                    new_node.GetComponent<PathNode>().connections.Add(
                            transform.GetChild(child_index - height).GetComponent<PathNode>()
                        );
                    transform.GetChild(child_index - height).GetComponent<PathNode>().connections.Add(new_node.GetComponent<PathNode>());

                    //Diagonal forward
                    /*if(i != width-1)
                    {
                        new_node.GetComponent<PathNode>().connections.Add(
                            transform.GetChild(child_index - height + 1).GetComponent<PathNode>()
                        );
                        transform.GetChild(child_index - height + 1).GetComponent<PathNode>().connections.Add(new_node.GetComponent<PathNode>());
                    }

                    if (i != 0)
                    {
                        //Diagonal down
                        new_node.GetComponent<PathNode>().connections.Add(
                                transform.GetChild(child_index - height - 1).GetComponent<PathNode>()
                            );
                        transform.GetChild(child_index - height - 1).GetComponent<PathNode>().connections.Add(new_node.GetComponent<PathNode>());
                    }*/
                }
                child_index++;
            }
        }
    }


}
