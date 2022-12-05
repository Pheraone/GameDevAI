using BTExample;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private GameObject playerInstance;
    [SerializeField] private GameObject text;
    private int waypointIndex = 0;
    //private Transform destination;

    private void Awake()
    {

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        bb = new BTBlackBoard();
        bb.SetData<NavMeshAgent>("navMeshAgent", agent);
        bb.SetData<GameObject>("playerInstance", playerInstance);
        bb.SetData<GameObject>("text", text);
        bb.SetData<List<Transform>>("waypoints", waypoints);
        bb.SetData<int>("waypointIndex", waypointIndex);
        //bb.SetData<Transform>("destination", destination);
    }

    private void Start()
    {

        BTBaseNode patrol =
           new BTSequenceNode(
               new BTCycleWaypointsNode(bb),
               new BTMoveTowardsNode(bb),
               new BTWaitNode(5f),
               new BTDebugNode("Hihi")
             );
        tree = new BTSelectorNode
            (
               //parallel
               //condition ziet speler?
               patrol,
               new BTSequenceNode
               (
                   //new BTInvertNode(
                    new BTDebugNode("This work?"),
                    //),
                    new BTWaitNode(5f),
                    new BTDebugNode("AAAAA"),
                    new BTMoveTowardsNode(bb)
                ),
                new BTSequenceNode
                (
                    new BTDebugNode("If Invert this message")
                )
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
