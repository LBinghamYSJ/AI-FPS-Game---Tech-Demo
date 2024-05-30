using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class PatrolTask : Node
{
    private Transform transform;
    private Transform[] Waypoints;

    private int currentWaypointIndex = 0;

    private float waitTime = 1f;
    private float waitCounter = 0f;
    private bool waiting = false;
    
    public PatrolTask(Transform transform, Transform[] Waypoints)
    {
        this.transform = transform;
        this.Waypoints = Waypoints;
    }

    public override NodeState Evaluate()
    {
        if (waiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
            {
                waiting = false;
            }
        }
        else
        {
            Transform wp = Waypoints[currentWaypointIndex];
            if (Vector3.Distance(transform.position, wp.position) < 0.1f)
            {
                transform.position = wp.position;
                waitCounter = 0f;
                waiting = true;

                currentWaypointIndex = (currentWaypointIndex + 1) % Waypoints.Length;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, wp.position, EnemyBT.speed * Time.deltaTime);
                transform.LookAt(wp.position);
            }
        }

        State = NodeState.RUNNING;
        return State;
    }
}
