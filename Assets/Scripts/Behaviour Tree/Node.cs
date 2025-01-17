using System.Collections;
using System.Collections.Generic;

namespace BehaviourTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
    public class Node
    {
        protected NodeState State;

        public Node Parent;
        protected List<Node> Children = new List<Node>();

        private Dictionary<string, object> dataContext = new Dictionary<string, object>();

        public Node()
        {
            Parent = null;
        }
        public Node(List<Node> Children)
        {
            foreach (Node child in Children)
            {
                Attach(child);
            }
        }

        private void Attach(Node node)
        {
            node.Parent = this;
            Children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, object value)
        {
            dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if(dataContext.TryGetValue(key, out value))
            {
                return value;
            }

            Node node = Parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                {
                    return value;
                }
                node = node.Parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            if (dataContext.ContainsKey(key))
            {
                dataContext.Remove(key);
                return true;
            }

            Node node = Parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                {
                    return true;
                }
                node = node.Parent;
            }
            return false;
        }

    }
}
