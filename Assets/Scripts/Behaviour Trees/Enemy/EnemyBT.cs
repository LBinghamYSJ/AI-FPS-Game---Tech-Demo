using System.Collections.Generic;
using BehaviourTree;

public class EnemyBT : Tree
{
    public UnityEngine.Transform[] Waypoints;

    public static float speed = 2f;
    public static float FOV = 12f;
    public static float ShootRange = 12f;
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new PlayerInShootingRangeNode(transform),
                new AttackTaskNode(transform),
            }),
            new Sequence(new List<Node>
            {
                new PlayerInRangeNode(transform),
                new GoToPlayerNode(transform),
            }),
            new PatrolTask(transform, Waypoints),
        });

        return root;
    }
}
