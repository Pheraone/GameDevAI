using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTree : MonoBehaviour
{
    private BTBaseNode root = null;

    protected void Start()
    {
        root = SetUpTree();
    }

    private void Update()
    {
        if(root != null)
        {
            root.Evaluate();
        }
    }

    protected abstract BTBaseNode SetUpTree();
}
