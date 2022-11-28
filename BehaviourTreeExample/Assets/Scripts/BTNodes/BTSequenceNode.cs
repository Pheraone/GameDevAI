using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequenceNode : BTBaseNode
{
    public BTSequenceNode() : base() { }

    public BTSequenceNode(List<BTBaseNode> _children) : base(_children) { }

    public override TaskStatus Evaluate()
    {
        bool anyChildIsRunning = false;

        foreach(BTBaseNode _node in children)
        {
            switch (_node.Evaluate())
            {
                case TaskStatus.Failed:
                    return state;
                case TaskStatus.Success:
                    continue;
                case TaskStatus.Running:
                    anyChildIsRunning = true;
                    continue;
                default:
                    state = TaskStatus.Success;
                    return state;


            }
        }
        state = anyChildIsRunning ? TaskStatus.Running : TaskStatus.Success;
        return state;
    }

}

public class BTDebugNode : BTBaseNode
{
    private string debugMessage;
    public BTDebugNode(string _debugMessage)
    {
        debugMessage = _debugMessage;
    }

    public override TaskStatus Evaluate()
    {
        Debug.Log(debugMessage);
        return TaskStatus.Success;
    }
}