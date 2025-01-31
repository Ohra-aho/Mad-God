using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Wall : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //If player is "above" sprite, show player through wall
        if(GetComponent<SpriteRenderer>())
        {
            if (player.transform.position.y < transform.position.y)
                GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
            else
                GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        } else if(GetComponent<TilemapRenderer>())
        {
            if (player.transform.position.y < transform.position.y)
                GetComponent<TilemapRenderer>().maskInteraction = SpriteMaskInteraction.None;
            else
                GetComponent<TilemapRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            
        }
    }
}
