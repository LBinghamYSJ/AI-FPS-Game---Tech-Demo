using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class PlayerInShootingRangeNode : Node
{
    private static int PlayerLayerMask = 1 << 6;

    private Transform transform;

    public PlayerInShootingRangeNode(Transform transform)
    {
        this.transform = transform;
    }

    public override NodeState Evaluate()
    {
        object target = GetData("target");
        if (target == null)
        {
            State = NodeState.FAILURE;
            return State;
        }

        Transform _target = (Transform)target;
        if (Vector3.Distance(transform.position, _target.position) <= EnemyBT.ShootRange)
        {
            State = NodeState.SUCCESS;
            return State;
        }

        State = NodeState.FAILURE;
        return State;

    }
}
