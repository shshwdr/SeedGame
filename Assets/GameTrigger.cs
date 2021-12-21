using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameTrigger:MonoBehaviour
{
    public string triggerName;


    private void Awake()
    {
        if(triggerName == "")
        {
            triggerName = name;
        }
        TriggersManager.Instance.addTrigger(this);
    }

    public virtual void trigger()
    {
        Debug.Log("trigger " + triggerName);
    }
}