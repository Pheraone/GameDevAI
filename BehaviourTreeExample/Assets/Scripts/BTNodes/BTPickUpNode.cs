using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        hasWeapon = blackboard.GetData<bool>("hasWeapon");
        weapon = blackboard.GetData<Transform>(objectName);
        weapon.gameObject.SetActive(false);
        hasWeapon = true;
        blackboard.SetData<bool>("hasWeapon", hasWeapon);
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
