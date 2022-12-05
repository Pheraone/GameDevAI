using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTWaitNode : BTBaseNode
{
    
        private float waitTime;
        private float currentTime;
        public BTWaitNode(float _waitTime)
        {
            waitTime = _waitTime;
        }

        public override TaskStatus Evaluate()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= waitTime)
            {
                currentTime = 0;
                return TaskStatus.Success;
            }
            return TaskStatus.Running;

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
