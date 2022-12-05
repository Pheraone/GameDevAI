﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelectorNode : BTBaseNode
{
    private int index = 0;
    private BTBaseNode[] nodes;

    public BTSelectorNode(params BTBaseNode[] _nodes)
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
                    continue;
                case TaskStatus.Success:
                    state = TaskStatus.Success;
                    return state;
               case TaskStatus.Running:
                    state = TaskStatus.Running;
                    return state;
               default:
                    continue;        


            }
        }
        
        index = 0;
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


