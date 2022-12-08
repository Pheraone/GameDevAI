using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.AI;

public class BTThrowSmokeNode : BTBaseNode
{
    private BTBlackBoard blackboard;
    private string destination;

    private float waitTime = 1f;
    private float currentTime;
    private bool hasThrownSmoke;
    private string boolean;

    private GameObject clone;
    public BTThrowSmokeNode(BTBlackBoard _bb, string _destination, string _bool)
    {
        blackboard = _bb;
        destination = _destination;
        boolean = _bool;
    }
    public override TaskStatus Evaluate()
    {

        hasThrownSmoke = blackboard.GetData<bool>(boolean);

        if(hasThrownSmoke == false)
        {
            Transform enemyPosition = blackboard.GetData<Transform>(destination);
            GameObject cloud = blackboard.GetData<GameObject>("cloud");
           // NavMeshAgent parentNavMesh = blackboard.GetData<NavMeshAgent>("navMeshAgent");
            //GameObject parent = parentNavMesh.gameObject;
            clone = GameObject.Instantiate(cloud, enemyPosition.position, Quaternion.identity);

            hasThrownSmoke = !hasThrownSmoke;
            blackboard.SetData<bool>(boolean, hasThrownSmoke);

            
            
        }
        currentTime = currentTime + Time.deltaTime;

        if (currentTime >= waitTime)
        {
            currentTime = 0;
            clone.SetActive(false);
            hasThrownSmoke = !hasThrownSmoke;
            blackboard.SetData<bool>(boolean, hasThrownSmoke);
            return TaskStatus.Failed;
        }
        else
        {
            return TaskStatus.Success;
        }

        //return TaskStatus.Success;


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
