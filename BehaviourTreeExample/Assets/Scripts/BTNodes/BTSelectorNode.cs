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
                    break;
                case TaskStatus.Success:
                    index = 0;
                    state = TaskStatus.Success;
                    return state;
               case TaskStatus.Running:
                    return TaskStatus.Running;
               //default:
                   // continue;        


            }
        }
        
        index = 0;
        return TaskStatus.Failed;
        
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


