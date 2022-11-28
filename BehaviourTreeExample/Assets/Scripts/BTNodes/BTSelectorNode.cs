using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelectorNode : BTBaseNode
{
        public BTSelectorNode() : base() { }

        public BTSelectorNode(List<BTBaseNode> _children) : base(_children) { }

        public override TaskStatus Evaluate()
        {
            foreach (BTBaseNode _node in children)
            {
                switch (_node.Evaluate())
                {
                    case TaskStatus.Failed:
                        continue;
                    case TaskStatus.Success:
                        state = TaskStatus.Success;
                        return state;
                    case TaskStatus.Running:
                        state = TaskStatus.Running;
                        return state;
                    default:
                    continue;


                }
            }
            state = TaskStatus.Failed;
            return state;
        }

    }


