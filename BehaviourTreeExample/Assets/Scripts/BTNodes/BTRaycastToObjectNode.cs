using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BTRaycastToObjectNode : BTBaseNode
{
    private BTBlackBoard blackboard;
    private string tObject;
    private string tThisObject;
    private Transform target;
    private NavMeshAgent myPosNav;
    private int layerNumber; // = 9
    private string boolean;
    private bool isHit;

    public BTRaycastToObjectNode(BTBlackBoard _bb, string _object, string _thisObject, int _layer, string _boolean)
    {
        blackboard = _bb;
        tObject = _object;
        tThisObject = _thisObject;
        layerNumber = _layer;
        boolean = _boolean;
        //player = _player;
    }

    public override TaskStatus Evaluate()
    {
        target = blackboard.GetData<Transform>(tObject);

        myPosNav = blackboard.GetData<NavMeshAgent>(tThisObject);
        Vector3 myPos = myPosNav.transform.position;
        Vector3 pos = target.transform.position - myPos;
        //collider check
        //enemy do damage
        isHit = blackboard.GetData<bool>(boolean);

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(myPos, pos, out hit, Mathf.Infinity, layerNumber))
        {

            Debug.DrawRay(myPos, pos * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            isHit = false;
            blackboard.SetData<bool>(boolean, isHit);
            
        }
        else
        {
            Debug.DrawRay(myPos, pos * 1000, Color.white);
            Debug.Log("Did not Hit");
            isHit = true;
            blackboard.SetData<bool>(boolean, isHit);
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
