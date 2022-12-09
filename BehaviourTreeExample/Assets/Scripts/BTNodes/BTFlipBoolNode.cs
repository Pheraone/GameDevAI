using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTFlipBoolNode : BTBaseNode
{
    private BTBlackBoard blackboard;
    private bool isInRange;
    public BTFlipBoolNode(BTBlackBoard _bb)
    {
        blackboard = _bb;
        //player = _player;
    }

    public override TaskStatus Evaluate()
    {
        Transform target = blackboard.GetData<Transform>("playerInstance");
        //als in range true is flip
        isInRange = blackboard.GetData<bool>("isInRange");

        Player player = target.gameObject.GetComponent<Player>();
        if(isInRange == true)
        {
            player.isSeen = true;
        } else
        {
            player.isSeen = false;
        }
        return TaskStatus.Success;
        //collider check
        //enemy do damage

        //blackboard.GetData<GameObject>("text").GetComponent<TextMesh>().text = "Checking" + target.transform.position;
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
