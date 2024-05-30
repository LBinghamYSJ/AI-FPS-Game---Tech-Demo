using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class GoToPlayerNode : Node
{
    private Transform transform;
    private NavMeshAgent agent;

    public GoToPlayerNode(Transform transform)
    {
        this.transform = transform;
        this.agent = transform.GetComponent<NavMeshAgent>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (Vector3.Distance(transform.position, target.position) > 0.01f)
        {
            agent.destination = target.position;
            transform.LookAt(target.position);
        }

        State = NodeState.RUNNING;
        return State;
    }
}