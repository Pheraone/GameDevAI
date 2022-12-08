using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class BTConditionNode : BTBaseNode
{
    private BTBlackBoard blackboard;
    private bool condition;
    private string conditionName;

    public BTConditionNode(BTBlackBoard _bb, string _conditionName)
    {
        blackboard = _bb;
        conditionName = _conditionName;
    }

    public override TaskStatus Evaluate()
    {
        blackboard.GetData<GameObject>("text").GetComponent<TextMesh>().text = "checking condition " + conditionName +" = " + condition;
        condition = blackboard.GetData<bool>(conditionName);
        Debug.Log(conditionName + condition);
        if (condition == true)
        {

            blackboard.SetData(conditionName, condition);
            
            state = TaskStatus.Success;

        }
        else
        {
            state = TaskStatus.Failed;
        }
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
