using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCheckPlayerNode : BTBaseNode
{
    private BTBlackBoard blackboard;
    private int damage;

    public BTCheckPlayerNode(BTBlackBoard _bb)
    {
        blackboard = _bb;
        //player = _player;
    }

    public override TaskStatus Evaluate()
    {
        Transform target = blackboard.GetData<Transform>("playerInstance");
        //collider check
        //enemy do damage

        blackboard.GetData<GameObject>("text").GetComponent<TextMesh>().text = "Checking" + target.transform.position;

        Player player = target.gameObject.GetComponent<Player>();
        if(player.isSeen == true)
        {
            Debug.Log("playerSeenBool" + player.isSeen);
            blackboard.SetData<bool>("isPlayerSeen", player.isSeen);
        } else
        {
            blackboard.SetData<bool>("isPlayerSeen", player.isSeen);
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
