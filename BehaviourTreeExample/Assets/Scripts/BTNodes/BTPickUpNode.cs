using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTPickUpNode : BTBaseNode
{
    private BTBlackBoard blackboard;
    private string objectName;
    private Transform weapon;
    private bool hasWeapon;
    public BTPickUpNode(BTBlackBoard _bb, string _object)
    {
        blackboard = _bb;
        objectName = _object;

    }

    public override TaskStatus Evaluate()
    {
        bool isInRange = blackboard.GetData<bool>("canPickUp");
        if(isInRange == true)
        {
            hasWeapon = blackboard.GetData<bool>("hasWeapon");
            weapon = blackboard.GetData<Transform>(objectName);
            NavMeshAgent myPos = blackboard.GetData<NavMeshAgent>("navMeshAgent");
            Vector3 myPosition = myPos.transform.position;
            int pickUpRange = blackboard.GetData<int>("pickUpRange");
            
            //Collider[] collider = Physics.OverlapSphere(myPosition, pickUpRange);
            //foreach(Collider _collider in collider)
            //{
                hasWeapon = true;
                weapon.gameObject.SetActive(false);
                Debug.Log("pickup object " + hasWeapon);
                blackboard.SetData<bool>("hasWeapon", hasWeapon);
                return TaskStatus.Success;

            //}

            
        }
        return TaskStatus.Running;
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
