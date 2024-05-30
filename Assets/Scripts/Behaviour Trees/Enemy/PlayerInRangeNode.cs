using BehaviourTree;
using UnityEngine;

public class PlayerInRangeNode : Node
{
    private static int PlayerLayerMask = 1 << 6;
    private Transform transform;

    public PlayerInRangeNode(Transform transform)
    {
        this.transform = transform;
    }

    public override NodeState Evaluate()
    {
        object target = GetData("target");
        if (target == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, EnemyBT.FOV, PlayerLayerMask);
            if (colliders.Length > 0)
            {
                Parent.Parent.SetData("target", colliders[0].transform);

                State = NodeState.SUCCESS;
                return State;
            }
            State = NodeState.FAILURE;
            return State;
        }

        State = NodeState.SUCCESS;
        return State;
    }
}