using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTCalcClosestObjectNode : BTBaseNode
{
    private BTBlackBoard blackboard;
    private bool firstTime = true;
    int index = 0;
    int smallestIndex;
    float smallestDistance;
    public BTCalcClosestObjectNode(BTBlackBoard _bb)
    {
        blackboard = _bb;
    }

    public override TaskStatus Evaluate()
    {

        List<Transform> hidingSpots = blackboard.GetData<List<Transform>>("hidingSpots");
        NavMeshAgent myNavMesh = blackboard.GetData<NavMeshAgent>("navMeshAgent");
        Vector3 myPos = myNavMesh.transform.position;
        //int waypointIndex = blackboard.GetData<int>("waypointIndex");
        Transform target;
        //if (firstTime == true)
        //{
        //    firstTime = false;
        //    target = waypoints[waypointIndex];
        //    blackboard.SetData<Transform>("destination", target);
        //    //Debug.Log(waypointIndex);
        //    return TaskStatus.Success;
        //}


        //waypointIndex = waypointIndex + 1;
        //if (waypointIndex == waypoints.Count)
        //{
        //    waypointIndex = 0;
        //}
        

        for (; index <= hidingSpots.Count; index++)
        {
            
            smallestDistance = Mathf.Infinity;
            float tempValue = Vector3.Distance(myPos, hidingSpots[index].transform.position);
            if (tempValue < smallestDistance)
            {
                smallestDistance = tempValue;
                smallestIndex = index;
            }
        }

        target = hidingSpots[smallestIndex];

        blackboard.SetData<Transform>("destination", target);
        index = 0;
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


