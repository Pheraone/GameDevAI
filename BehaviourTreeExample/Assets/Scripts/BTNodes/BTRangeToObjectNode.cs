using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTRangeToObjectNode : BTBaseNode
{
    private BTBlackBoard blackboard;
    private string objectName;
    private int range;
    private string boolean;

    
    public BTRangeToObjectNode(BTBlackBoard _bb, string _objectName, int _range, string _bool)
    {
        blackboard = _bb;
        range = _range;
        boolean = _bool;
        objectName = _objectName;
    }

    public override TaskStatus Evaluate()
    {
        blackboard.GetData<GameObject>("text").GetComponent<TextMesh>().text = "checking object " + objectName;
        
        bool isInRange = blackboard.GetData<bool>(boolean);
        Transform objectTransform = blackboard.GetData<Transform>(objectName);
        Vector3 objectPosition = objectTransform.transform.position;

        NavMeshAgent agent = blackboard.GetData<NavMeshAgent>("navMeshAgent");
        Vector3 myPos = agent.transform.position;

        if(objectTransform.gameObject.activeSelf == false)
        {
            isInRange = false;
            blackboard.SetData<bool>(boolean, isInRange);
            return TaskStatus.Success;
        }
       
       if(Vector3.Distance(myPos, objectPosition) <= range)
        {
            isInRange = true;
            blackboard.SetData<bool>(boolean, isInRange);
            return TaskStatus.Failed;
        } 
        else
        {
            isInRange = false;
            blackboard.SetData<bool>(boolean, isInRange);
            return TaskStatus.Success;
        }
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