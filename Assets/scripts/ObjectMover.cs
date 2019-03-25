using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    Rigidbody2D rb2d;
    public Transform[] wayPointsArray;
    Transform WayPoint;
    public float speed = 5f;
    int currentWayPoint = 0;
    public float arrivalDistance = 0.1f;
    float lastDistanceToTarget = 0f;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //Get first waypoint
        WayPoint = wayPointsArray[currentWayPoint];
        lastDistanceToTarget = Vector3.Distance(transform.position, WayPoint.position);
    }

    void FixedUpdate()
    {
        walk();
    }

    void walk()
    {
        //If close to target, or overshot it get next waypoint
        float distanceToTarget = Vector3.Distance(transform.position, WayPoint.position);
        if ((distanceToTarget < arrivalDistance) || (distanceToTarget > lastDistanceToTarget))
        {
            currentWayPoint++;
            if (currentWayPoint >= wayPointsArray.Length)
                currentWayPoint = 0;
            WayPoint = wayPointsArray[currentWayPoint];
            lastDistanceToTarget = Vector3.Distance(transform.position, WayPoint.position);
            Debug.Log("Added the next waypoint(" + currentWayPoint + "). Object: " + gameObject.name);
        }
        else
        {
            lastDistanceToTarget = distanceToTarget;
        }

        //Get direction to the waypoint.
        //Normalize so it doesn't change with distance.
        Vector3 dir = (WayPoint.position - transform.position).normalized;

        //(speed * Time.fixedDeltaTime) makes the object move by 'speed' units per second, framerate independent
        rb2d.MovePosition(transform.position + dir * (speed * Time.fixedDeltaTime));

    }
}

