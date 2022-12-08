using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTParallelNode : BTBaseNode
{
    private int index = 0;
    private BTBaseNode[] nodes;

    public BTParallelNode(params BTBaseNode[] _nodes)
    {
        nodes = _nodes;
    }

    public override TaskStatus Evaluate()
    {

        for (; index < nodes.Length; index++)
        {

            switch (nodes[index].Evaluate())
            {
                case TaskStatus.Failed:
                    index = nodes.Length;
                    state = TaskStatus.Failed;
                    break;
                    //return TaskStatus.Failed;
                case TaskStatus.Success:
                    state = TaskStatus.Success;
                    break;
                    //return TaskStatus.Success;
                case TaskStatus.Running:
                    state = TaskStatus.Running;
                    break;
                   // return TaskStatus.Running;
            }
        }
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

    //state = anyChildIsRunning ? TaskStatus.Running : TaskStatus.Success;
   
        //return state;
}


