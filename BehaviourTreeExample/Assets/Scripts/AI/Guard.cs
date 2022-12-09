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

    private int waypointIndex = 0;

    private bool isInRange = false;
    private bool canPickUp = false;
    private bool isInAttackRange = false;

    [SerializeField] public List<Transform> waypoints = new List<Transform>();
    [SerializeField] private Transform playerInstance;
    [SerializeField] private Transform weapon;
    [SerializeField] private GameObject text;
    [SerializeField] private int range;
    [SerializeField] private int attackRange;
    [SerializeField] private int pickUpRange;
    [SerializeField]private bool hasWeapon = false;
    [SerializeField] private int damage = 10;
    private bool isSmoked = false;
    

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
        bb.SetData<bool>("isInAttackRange", isInAttackRange);
        bb.SetData<int>("range", range);
        bb.SetData<int>("pickUpRange", pickUpRange);
        bb.SetData<Transform>("weapon", weapon);
        bb.SetData<bool>("canPickUp", canPickUp);
        bb.SetData<bool>("isSmoked", isSmoked);
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

        BTBaseNode smoke =
            new BTSequenceNode(
                new BTConditionNode(bb, "isSmoked"),
            new BTInvertNode(
                new BTWaitNode(10f))
                );

        BTBaseNode attack =
            new BTParallelNode(
                new BTInvertNode(
               new BTRangeToObjectNode(bb, "playerInstance", range, "isInRange")),
            new BTSequenceNode(
                    new BTConditionNode(bb, "isInRange"),
                new BTSequenceNode(

                    new BTFlipBoolNode(bb),
                    new BTConditionNode(bb, "hasWeapon") ,
                new BTMoveTowardsNode(bb, "playerInstance"),
                new BTInvertNode(
                new BTRangeToObjectNode(bb, "playerInstance", attackRange, "isInAttackRange")),
                new BTConditionNode(bb, "isInAttackRange"),
                new BTAttackNode(bb, damage)
                )
                ));


        BTBaseNode getWeapon =
            new BTSequenceNode(
                //new BTParallelNode(
                new BTInvertNode(
                new BTRangeToObjectNode(bb, "playerInstance", range, "isInRange")),
                new BTSequenceNode(
                    new BTConditionNode(bb, "isInRange"),
                    new BTSequenceNode(
                    new BTFlipBoolNode(bb),
                        new BTInvertNode(
                            new BTConditionNode(bb, "hasWeapon")
                        ),
                        new BTMoveTowardsNode(bb, "weapon"),
                        new BTInvertNode(
                            new BTRangeToObjectNode(bb, "weapon", pickUpRange, "canPickUp")),
                        new BTPickUpNode(bb, "weapon")
                )));

        BTBaseNode conditionalNodeTest =
                //new BTSequenceNode(
                new BTInvertNode(
                    new BTConditionNode(bb, "hasWeapon")
                    );


        tree = new BTSelectorNode
            (
            smoke,
            getWeapon,
             attack,
            new BTParallelNode(
                new BTRangeToObjectNode(bb, "playerInstance", range, "isInRange"),
                new BTSequenceNode(
                new BTInvertNode(

                new BTConditionNode(bb, "isInRange")),
                new BTFlipBoolNode(bb),
                patrol
                //new BTWaitNode(5f)
                ))
            );
    }

    private void FixedUpdate()
    {
        tree?.Evaluate();
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "smoke")
        {

            isSmoked = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "smoke")
        {
            isSmoked = false;
        }
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
