using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    GameObject entity;

    [SerializeField] float viewRange;
    [SerializeField] int field_of_view;
    [SerializeField] LayerMask nonLOS_blocker;
    [SerializeField] LayerMask obstacles;

    bool playerDetected = false;

    [HideInInspector] public GameObject player;


    private void Awake()
    {
        entity = transform.parent.parent.gameObject;
    }
    private void Update()
    {
    }

    private void FixedUpdate()
    {
        MakeFieldOfVIew(nonLOS_blocker, true);

    }

    private void MakeFieldOfVIew(LayerMask ignore, bool LOS)
    {

        float angleBetweenRays = 5;
        bool detected = false;

        //parents angel to direction
        float angle = transform.parent.eulerAngles.z + 90; // Reverse the -90 offset
        Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

        for(int i = 0; i < field_of_view; i++)
        {
            
            float angleOffset = angleBetweenRays * i;

            // Rotate the direction vector for sight1 (clockwise)
            Vector2 rotatedDirection1 = RotateVector(direction, angleOffset);

            // Rotate the direction vector for sight2 (counterclockwise)
            Vector2 rotatedDirection2 = RotateVector(direction, -angleOffset);

            // Cast the rays
            RaycastHit2D sight1 = Physics2D.Raycast(transform.position, rotatedDirection1, viewRange, ignore);
            RaycastHit2D sight2 = Physics2D.Raycast(transform.position, rotatedDirection2, viewRange, ignore);

            if (sight1.collider != null)
            {
                if (sight1.collider.GetComponent<Player>() && LOS)
                {
                    player = sight1.collider.gameObject;
                    detected = true;
                }
            }

            if (sight2.collider != null)
            {
                if (sight2.collider.GetComponent<Player>() && LOS)
                {
                    player = sight2.collider.gameObject;
                    detected = true;
                }
            }

            // Draw the rays
            
            if(LOS)
            {
                Debug.DrawRay(transform.position, rotatedDirection1 * (sight1.collider != null ? sight1.distance : viewRange), Color.green);
                Debug.DrawRay(transform.position, rotatedDirection2 * (sight2.collider != null ? sight2.distance : viewRange), Color.green);
            }
            
        }
        if(detected)
        {
            entity.GetComponent<EnemyPathFinding>().Aggro();
        } else
        {
            entity.GetComponent<EnemyPathFinding>().DeAggro();
        }
    }

    //Selvitä miten toimii
    Vector2 RotateVector(Vector2 v, float angleDegrees)
    {
        float radians = angleDegrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       /* if (other.gameObject.GetComponent<Player>())
        {
            entity.GetComponent<EnemyPathFinding>().player = other.gameObject;
            entity.GetComponent<EnemyPathFinding>().Aggro();
        }*/
            
    }

    /*private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>())
            entity.GetComponent<EnemyPathFinding>().player = other.gameObject;
    }*/

    private void OnTriggerExit2D(Collider2D other)
    {
        /*if (other.gameObject.GetComponent<Player>())
        {
            entity.GetComponent<EnemyPathFinding>().DeAggro();
        }*/
    }
}
