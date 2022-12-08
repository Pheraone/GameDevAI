using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAttackNode : BTBaseNode
{
    private BTBlackBoard blackboard;
    //private Player player;
    
    public BTAttackNode(BTBlackBoard _bb)
    {
        blackboard = _bb;
        //player = _player;
    }

    public override TaskStatus Evaluate()
    {
        Transform target = blackboard.GetData<Transform>("playerInstance");
        //collider check
        //enemy do damage


        blackboard.GetData<GameObject>("text").GetComponent<TextMesh>().text = "Attacking" + target.transform.position;
        //Als enemy de speler heeft geraakt ->player is dood check? wait node?
        //Als de speler dood is -> home
        //Als de speler uit range is -> back to patrol
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
