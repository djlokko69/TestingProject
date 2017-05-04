using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class AIControl : MonoBehaviour
{
    //Array is []
    public Transform[] waypoints;
    public bool crouch = false;
    public bool jump = false;
    public float stoppingDistance = 5f;

    private Player character;
    private int currentPoint = 0;
    private float distance = 0;


    // Use this for initialization
    void Start()
    {
        character = GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        MoveToWaypoint();
    }

    void MoveToWaypoint()
    {
        //Get our movement direction
        float playerPosX = transform.position.x;
        float waypointPosX = GetWaypointPos().x;
        float move = 0;
        if(playerPosX > waypointPosX)
        {
            move = -1;
        }
        else
        {
            move = 1;
        }
        character.Move(move, crouch, jump);
        jump = false;
    }

    //Void doesnt return
    Vector3 GetWaypointPos()
    {
        //Get distance from possittion to waypoint
        Vector3 waypointPos = waypoints[currentPoint].position;
        distance = Vector3.Distance(transform.position, waypointPos);
        // Check if i'm close to stoppingdistance
        if(distance <= stoppingDistance)
        {
            // Go to next waypoint
            currentPoint++;
        }
        //Check if currentPoint is outside waypoints length
        if(currentPoint >= waypoints.Length)
        {
            //Reset currentPoint
            currentPoint = 0;
        }
        // Return the waypoint
        return waypoints[currentPoint].position;
    }
}
