using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public abstract class Tree : MonoBehaviour
    {
        private Node Root = null;

        protected void Start()
        {
            Root = SetupTree();
        }

        private void Update()
        {
            if (Root != null)
            {
                Root.Evaluate();
            }
        }

        protected abstract Node SetupTree();
    }
}
