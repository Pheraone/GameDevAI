using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTBlackBoard
{
    private Dictionary<string, object> data = new Dictionary<string, object>();

    public T GetData<T>(string _name)
    {
        return data.ContainsKey(_name) ? (T)data[_name] : default(T);
    }

    public void SetData<T>(string _name, T _value)
    {
        if (data.ContainsKey(_name))
        {
            data[_name] = _value;
        }
        else
        {
            data.Add(_name, _value);
        }
    } 

}
