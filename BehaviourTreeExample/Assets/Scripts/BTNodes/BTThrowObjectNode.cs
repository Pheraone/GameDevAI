using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjectNode : BTBaseNode
{
    private BTBlackBoard blackboard;
    private string destination;
    public ThrowObjectNode(BTBlackBoard _bb, string _destination)
    {
        blackboard = _bb;
        destination = _destination;
    }
    public override TaskStatus Evaluate()
    {
        //blackboard.getData<Transform>("");
        return base.Evaluate();
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
