using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCycleWaypointsNode : BTBaseNode
{
    private BTBlackBoard blackboard;
    private bool firstTime = true;
    public BTCycleWaypointsNode(BTBlackBoard _bb)
    {
        blackboard = _bb;
    }

    public override TaskStatus Evaluate()
    {
      
        List<Transform> waypoints = blackboard.GetData<List<Transform>>("waypoints");
        int waypointIndex = blackboard.GetData<int>("waypointIndex");
        Transform target;
        if (firstTime == true)
        {
            firstTime = false;
            target = waypoints[waypointIndex];
            blackboard.SetData<Transform>("destination", target);
            Debug.Log(waypointIndex);
            return TaskStatus.Success;
        }


        waypointIndex = waypointIndex + 1;
        if (waypointIndex == waypoints.Count)
        {
            waypointIndex = 0;
        } 

        blackboard.SetData<int>("waypointIndex", waypointIndex);
        target = waypoints[waypointIndex];

        blackboard.SetData<Transform>("destination", target);

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
