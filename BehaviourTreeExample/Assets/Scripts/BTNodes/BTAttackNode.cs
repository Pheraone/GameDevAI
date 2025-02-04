﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTAttackNode : BTBaseNode
{
    private BTBlackBoard blackboard;
    private int damage;
    //private Player player;
    
    public BTAttackNode(BTBlackBoard _bb, int _damage)
    {
        blackboard = _bb;
        damage = _damage;
        //player = _player;
    }

    public override TaskStatus Evaluate()
    {
        Transform target = blackboard.GetData<Transform>("playerInstance");
        //collider check
        //enemy do damage


        blackboard.GetData<GameObject>("text").GetComponent<TextMesh>().text = "Attacking" + target.transform.position;
        NavMeshAgent attackerAgent = blackboard.GetData<NavMeshAgent>("navMeshAgent");
        GameObject attacker = attackerAgent.gameObject;
        //Als enemy de speler heeft geraakt ->player is dood check? wait node?
        //Als de speler dood is -> home
        //Als de speler uit range is -> back to patrol
        Player player = target.gameObject.GetComponent<Player>();
        player.TakeDamage(attacker, damage);
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
