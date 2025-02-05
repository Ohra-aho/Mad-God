using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    public bool active = true;

    int delay_frames = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(delay_frames > -3) delay_frames--;
        if (delay_frames == 0)
        {
            if(active) Spawn();
        }
    }

    private void Spawn()
    {
        GameObject enemy1 = Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
