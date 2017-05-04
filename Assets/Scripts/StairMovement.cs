using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairMovement : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float movementSpeed = 20f;

    private int currentPoint = 0;
    private Rigidbody2D rigid;


    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        // Check if we have reached our waypoint
        if (transform.position == patrolPoints[currentPoint].position)
        {
            // Increment currentpoint by 1
            currentPoint++;
        }

        // Check if current is outside range of array
        if (currentPoint >= patrolPoints.Length)
        {
            // Reset currentPoint to 0
            currentPoint = 0;
        }

        //Move between waypoints
        Vector3 movePos = Vector3.MoveTowards(transform.position,
            patrolPoints[currentPoint].position,
            movementSpeed * Time.deltaTime);
        transform.position = movePos;
    }
}
