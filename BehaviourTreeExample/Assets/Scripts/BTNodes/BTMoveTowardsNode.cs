using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BTMoveTowardsNode : BTBaseNode
{
    private BTBlackBoard blackboard;
    private string destination;

    public BTMoveTowardsNode(BTBlackBoard _bb, string _destination)
    {
        blackboard = _bb;
        destination = _destination;
    }

    public override TaskStatus Evaluate()
    {

        //blackboard.SetData<Transform>("destination", transform);
        Transform targetPosition = blackboard.GetData<Transform>(destination);
        NavMeshAgent agent = blackboard.GetData<NavMeshAgent>("navMeshAgent");
        blackboard.GetData<GameObject>("text").GetComponent<TextMesh>().text = "Walking Towards" + targetPosition;

        Vector3 target = targetPosition.position;//.x, targetPosition.position.y, targetPosition.position.z);
        agent.destination = target;
        //return TaskStatus.Failed;

        if (agent.pathPending)
        {
            return TaskStatus.Running;
        }

        if (agent.isPathStale)
        {
            return TaskStatus.Failed;
        }

        switch (agent.pathStatus)
        {
            case NavMeshPathStatus.PathComplete:
                return TaskStatus.Success;
            case NavMeshPathStatus.PathPartial:
                return TaskStatus.Failed;
            case NavMeshPathStatus.PathInvalid:
                return TaskStatus.Failed;
        }
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
