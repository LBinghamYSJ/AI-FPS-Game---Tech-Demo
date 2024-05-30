using System.Collections.Generic;

namespace BehaviourTree
{
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> Children) : base(Children) { }
        public override NodeState Evaluate()
        {
            bool isAChildRunning = false;

            foreach (Node node in Children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        State = NodeState.FAILURE;
                        return State;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        isAChildRunning = true;
                        continue;
                    default:
                        State = NodeState.SUCCESS;
                        return State;
                }
            }

            State = isAChildRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return State;
        }
    }
}
