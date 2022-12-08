using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using UnityEditor.SceneManagement;

public class Rogue : MonoBehaviour
{

    private BTBaseNode tree;
    private NavMeshAgent agent;
    private Animator animator;
    private BTBlackBoard bb;
    
    private bool isPlayerSeen;
    [SerializeField] private int range;
    [SerializeField]private GameObject text;
    [SerializeField] private Transform enemyPosition;
    [SerializeField] private Transform playerInstance;
    [SerializeField] private GameObject cloudMesh;
    private bool isPlayerInRange = false;
    private bool hasThrownSmoke = false;
    [SerializeField] private List<Transform> hidingSpots;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        bb = new BTBlackBoard();
        bb.SetData<NavMeshAgent>("navMeshAgent", agent);
        bb.SetData<List<Transform>>("hidingSpots", hidingSpots);
        bb.SetData<Transform>("playerInstance", playerInstance);
        bb.SetData<GameObject>("text", text);
        bb.SetData<bool>("isPlayerInRange", isPlayerInRange);
        bb.SetData<Transform>("enemyPosition", enemyPosition);
        bb.SetData<GameObject>("cloud", cloudMesh);
        bb.SetData<bool>("hasThrownSmoke", hasThrownSmoke);
        //bb.SetData<bool>("isPlayerSeen",)

    }

    private void Start()
    {
        BTBaseNode hide =
            new BTSequenceNode(
                new BTParallelNode(
                new BTCheckPlayerNode(bb),
                new BTConditionNode(bb, "isPlayerSeen"),
                //new BTSequenceNode(
                //check when ally is hidden bool switch range to object node?
                //new BTConditionNode(bb, "isHidden"),
                new BTSequenceNode(
                    //new BTInvertNode(
                    //    new BTConditionNode(bb, "isHidden")
                    //),
                    //go to closest waypoint
                    new BTCalcClosestObjectNode(bb),
                    new BTMoveTowardsNode(bb, "destination")
                    )
                ));

        BTBaseNode throwBomb =
            new BTSequenceNode(
                
                new BTCheckPlayerNode(bb),
                new BTConditionNode(bb, "isPlayerSeen"),
                //new BTConditionNode(bb, "isHidden"), 
                //new BTInvertNode(
                new BTThrowSmokeNode(bb, "enemyPosition", "hasThrownSmoke")
                //new BTWaitNode(5f)
                );

        BTBaseNode stayCloseToPlayer =
            new BTParallelNode(
                new BTDebugNode("now here"),
                new BTSequenceNode(
                new BTRangeToObjectNode(bb, "playerInstance", range, "isPlayerInRange"),
                new BTInvertNode(
                new BTConditionNode(bb, "isPlayerInRange")
                ))
                //new BTInvertNode(
                //new BTRangeToObjectNode(bb, "playerInstance", range, "isPlayerInRange")
                //),
                ,
                new BTSequenceNode(
                    new BTMoveTowardsNode(bb, "playerInstance")
                    //new BTWaitNode(2f)
                    )
                );

        tree = new BTSelectorNode(
            new BTSequenceNode(

                stayCloseToPlayer,
                hide
                //throwBomb,
                )

            );
    }

    private void FixedUpdate()
    {
        tree?.Evaluate();
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Handles.color = Color.yellow;
    //    Vector3 endPointLeft = viewTransform.position + (Quaternion.Euler(0, -ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;
    //    Vector3 endPointRight = viewTransform.position + (Quaternion.Euler(0, ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;

    //    Handles.DrawWireArc(viewTransform.position, Vector3.up, Quaternion.Euler(0, -ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward, ViewAngleInDegrees.Value * 2, SightRange.Value);
    //    Gizmos.DrawLine(viewTransform.position, endPointLeft);
    //    Gizmos.DrawLine(viewTransform.position, endPointRight);

    //}
}
