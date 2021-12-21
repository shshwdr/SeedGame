using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersManager : Singleton<TriggersManager>
{
    Dictionary<string, GameTrigger> triggerDict = new Dictionary<string, GameTrigger>();
    public Dictionary<string, bool> isTriggered = new Dictionary<string, bool>();
    
    public void addTrigger(GameTrigger trigger)
    {
        if (triggerDict.ContainsKey(trigger.name))
        {
            Debug.LogError("trigger name duplicated " + trigger.name);
        }
        triggerDict[trigger.name] = trigger;
        isTriggered[trigger.name] = false;
    }

    public void trigger(GameTrigger trigger)
    {
        isTriggered[trigger.name] = true;
    }

    public bool IsTriggered(GameTrigger trigger)
    {
        return isTriggered[trigger.name];
    }
    public override void Save(SerializedGame save)
    {
        base.Save(save);
        save.isTriggered = isTriggered;
    }
    public override void Load(SerializedGame save)
    {
        base.Load(save);
        Dictionary<string, bool> temp  = save.isTriggered;

        foreach(var trigger in triggerDict.Keys)
        {
            if (temp.ContainsKey(trigger) && temp[trigger])
            {
                triggerDict[trigger].trigger();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
