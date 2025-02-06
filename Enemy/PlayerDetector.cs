using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] float viewRange;
    [SerializeField] int field_of_view;
    [SerializeField] LayerMask nonLOS_blocker;
    [SerializeField] LayerMask obstacles;

    public bool target_in_sight = false;

    [HideInInspector] public GameObject player;

    int delay = 0;


    private void Awake()
    {
    }
    private void Update()
    {
    }

    private void FixedUpdate()
    {
        MakeFieldOfVIew(nonLOS_blocker);
    }

    private void MakeFieldOfVIew(LayerMask ignore)
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
                if (sight1.collider.GetComponent<Player>())
                {
                    player = sight1.collider.gameObject;
                    detected = true;
                }
            }

            if (sight2.collider != null)
            {
                if (sight2.collider.GetComponent<Player>())
                {
                    player = sight2.collider.gameObject;
                    detected = true;
                }
            }

            // Draw the rays
            Debug.DrawRay(transform.position, rotatedDirection1 * (sight1.collider != null ? sight1.distance : viewRange), Color.green);
            Debug.DrawRay(transform.position, rotatedDirection2 * (sight2.collider != null ? sight2.distance : viewRange), Color.green);
        }

        //Prevents rapid changing from in line of sight to out of line of sight
        if(target_in_sight && !detected)
        {
            if(delay == 0) delay = 60;
            delay--;
        } else if(!target_in_sight && detected)
        {
            delay = 0;
        }

        if(delay == 0) target_in_sight = detected;
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
    }

    private void OnTriggerStay2D(Collider2D other)
    {
    }

    private void OnTriggerExit2D(Collider2D other)
    {
    }
}
