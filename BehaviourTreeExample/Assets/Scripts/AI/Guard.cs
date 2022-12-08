using BTExample;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    private BTBaseNode tree;
    
    private NavMeshAgent agent;
    private Animator animator;
    private BTBlackBoard bb;
    [SerializeField] public List<Transform> waypoints = new List<Transform>();
    [SerializeField] private Transform playerInstance;
    [SerializeField] private GameObject text;
    [SerializeField] private int range;
    [SerializeField] private int attackRange;

    private int waypointIndex = 0;
    [SerializeField]private bool hasWeapon = false;
    private bool isInRange = false;
    [SerializeField] private Transform weapon;
    

    private void Awake()
    {

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        bb = new BTBlackBoard();
        bb.SetData<NavMeshAgent>("navMeshAgent", agent);
        bb.SetData<Transform>("playerInstance", playerInstance);
        bb.SetData<GameObject>("text", text);
        bb.SetData<List<Transform>>("waypoints", waypoints);
        bb.SetData<int>("waypointIndex", waypointIndex);
        bb.SetData<bool>("hasWeapon", hasWeapon);
        bb.SetData<bool>("isInRange", isInRange);
        bb.SetData<int>("range", range);
        bb.SetData<int>("attackRange", attackRange);
        bb.SetData<Transform>("weapon", weapon);
    }

    private void Start()
    {

        BTBaseNode patrol =
           new BTSequenceNode(
               new BTCycleWaypointsNode(bb),
               new BTMoveTowardsNode(bb, "destination"),
               new BTDebugNode("Hihi"),
               new BTWaitNode(5f)
             );

        BTBaseNode attack =
            new BTParallelNode(
                new BTInvertNode(
               new BTRangeToObjectNode(bb, "playerInstance", range, "isInRange")),
            new BTSequenceNode(
               
                    new BTConditionNode(bb, "isInRange"),
                new BTSequenceNode(
                     
                        new BTConditionNode(bb, "hasWeapon") ,
                    
                new BTMoveTowardsNode(bb, "playerInstance"),
               
                new BTAttackNode(bb)
                )
                ));


        BTBaseNode getWeapon =
            new BTSequenceNode(
                new BTParallelNode(
                new BTInvertNode(
               new BTRangeToObjectNode(bb, "playerInstance", range, "isInRange")),
                new BTSequenceNode(

                new BTConditionNode(bb, "isInRange"),
                new BTInvertNode(
                new BTConditionNode(bb, "hasWeapon")
                ),
                new BTRangeToObjectNode(bb, "weapon", range, "isInRange"),
                //get closest object (weapon)
                new BTMoveTowardsNode(bb, "weapon")
                //new BTPickUpNode(bb, "weapon")
                // pick up weapon
                )));

        BTBaseNode conditionalNodeTest =
                //new BTSequenceNode(
                new BTInvertNode(
                    new BTConditionNode(bb, "hasWeapon")
                    );

        //new BTDebugNode("conditional code reached")
        // );

        tree = new BTSelectorNode
            (
            getWeapon,
             attack,
            new BTParallelNode(
                new BTRangeToObjectNode(bb, "playerInstance", range, "isInRange"),
                new BTSequenceNode(
                new BTInvertNode(

                new BTConditionNode(bb, "isInRange")),
                patrol
                //new BTWaitNode(5f)
                ))
            );
    }

    private void FixedUpdate()
    {
        tree?.Evaluate();
    }

  
    //protected override BTBaseNode SetUpTree()
    //{
    //    BTBaseNode Patrol =
    //        new BTSequenceNode(

    //          );

    //    BTBaseNode root = new BTSelectorNode(new List<BTBaseNode>
    //    {
    //        Patrol,
    //        new BTInvertNode(),
    //        new BTDebugNode("It work"),
    //        new BTWaitNode(4f),
    //        new BTDebugNode("Then This Message"),
    //        new BTDebugNode("It's amazing")
    //    });
    //    return root;
    //}

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
