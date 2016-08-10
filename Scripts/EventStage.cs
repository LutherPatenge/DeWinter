using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventStage : MonoBehaviour {

    public string description;
    public List<EventOption> stageEventOptions = new List<EventOption>();
    //This is the Rep and Money Change that occurs upon this Stage BEGINNING
    public int stageRepChange;
    public int stageMoneyChange;

    //1 Option Constructor
    public EventStage(string desc, int sRepChng, int sMonChng, EventOption op0)
    {
        description = desc;
        stageRepChange = sRepChng;
        stageMoneyChange = sMonChng;
        stageEventOptions.Add(op0);
        stageEventOptions.Add(new EventOption());
        stageEventOptions.Add(new EventOption());
        stageEventOptions.Add(new EventOption());
    }
    
    //2 Option Constructor
    public EventStage(string desc, int sRepChng, int sMonChng, EventOption op0, EventOption op1)
    {
        description = desc;
        stageRepChange = sRepChng;
        stageMoneyChange = sMonChng;
        stageEventOptions.Add(op0);
        stageEventOptions.Add(op1);
        stageEventOptions.Add(new EventOption());
        stageEventOptions.Add(new EventOption());
    }
    
    //3 Option Constructor
    public EventStage(string desc, int sRepChng, int sMonChng, EventOption op0, EventOption op1, EventOption op2)
    {
        description = desc;
        stageRepChange = sRepChng;
        stageMoneyChange = sMonChng;
        stageEventOptions.Add(op0);
        stageEventOptions.Add(op1);
        stageEventOptions.Add(op2);
        stageEventOptions.Add(new EventOption());
    }

    //4 Option Constructor
    public EventStage(string desc, int sRepChng, int sMonChng, EventOption op0, EventOption op1, EventOption op2, EventOption op3)
    {
        description = desc;
        stageRepChange = sRepChng;
        stageMoneyChange = sMonChng;
        stageEventOptions.Add(op0);
        stageEventOptions.Add(op1);
        stageEventOptions.Add(op2);
        stageEventOptions.Add(op3);
    }
}
