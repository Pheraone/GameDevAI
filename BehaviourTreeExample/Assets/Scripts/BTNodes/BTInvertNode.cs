using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTInvertNode : BTBaseNode
{

    //public BTInvertNode() : base() { }

    //public BTInvertNode(List<BTBaseNode> _children) : base(_children) { }
    private BTBaseNode node;

    public BTInvertNode(BTBaseNode _node)
    {
        node = _node;
    }

    public override TaskStatus Evaluate()
    {
       
            switch (node.Evaluate())
            {
                case TaskStatus.Failed:
                    state = TaskStatus.Success;
                    return state;
                case TaskStatus.Success:
                    state = TaskStatus.Failed;
                    return state;
                case TaskStatus.Running:
                    state = TaskStatus.Running;
                    return state;
            }
        
        state = TaskStatus.Failed;
        return state;
    }

    public override void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }
}
