using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Assertions.Must;

public enum TaskStatus { Success, Failed, Running }
public abstract class BTBaseNode
{
    //public abstract TaskStatus Run();
    protected TaskStatus state;

    //public BTBaseNode parent;
    //protected List<BTBaseNode> children = new List<BTBaseNode>();
    //public BTBaseNode()
    //{
    //    parent = null;
    //}

    //public BTBaseNode(List<BTBaseNode> children)
    //{
    //    foreach (BTBaseNode child in children)
    //    {
    //        Attach(child);
    //    }
    //}

    //private void Attach(BTBaseNode _node)
    //{
    //    _node.parent = this;
    //    children.Add(_node);
    //}

    public abstract void OnEnter();
    public abstract void OnExit();

    public virtual TaskStatus Evaluate() => TaskStatus.Failed;

    //private Dictionary<string, object> dataContext = new Dictionary<string, object>();

    //public void SetData(string _key, object _value)
    //{
    //    dataContext[_key] = _value;
    //}

    //public object GetData(string _key)
    //{
    //    object value = null;
    //    if (dataContext.TryGetValue(_key, out value)) 
    //    { 
    //        return value;
    //    }
           

    //    BTBaseNode node = parent;
    //    while(node != null)
    //    {
    //        value = node.GetData(_key);
    //        if(value != null)
    //        {
    //            return value;
    //        }
    //        node = node.parent;
    //    }

    //    return null;
    //}

    //public bool ClearData(string _key)
    //{
    //    if (dataContext.ContainsKey(_key))
    //    {
    //        dataContext.Remove(_key);
    //        return true;
    //    }

    //    BTBaseNode node = parent;

    //    while (node != null)
    //    {
    //        bool cleared = node.ClearData(_key);
    //        if (cleared)
    //        {
    //            return true;
    //        }
    //        node = node.parent;
    //    }
    //    return false;
    //}
}
