using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequenceNode : BTBaseNode 
{
    private int index = 0;
    private BTBaseNode[] nodes;

    public BTSequenceNode(params BTBaseNode[] _nodes) {
        nodes = _nodes;
    }

    public override TaskStatus Evaluate()
    {
        for(; index < nodes.Length; index++)
        {
            switch (nodes[index].Evaluate())
            {
                case TaskStatus.Failed:
                    return state;
                case TaskStatus.Success:
                    continue;
                case TaskStatus.Running:
                    //anyChildIsRunning = true;
                    return TaskStatus.Running;
                default:
                    state = TaskStatus.Success;
                    return state;


            }
        }
        //state = anyChildIsRunning ? TaskStatus.Running : TaskStatus.Success;
        index = 0;
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

    public override void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }
}