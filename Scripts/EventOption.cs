﻿using UnityEngine;
using System.Collections;

public class EventOption{

    public string optionButtonText;
    //Determines the next stage the Event will hop to. If it's -1 then the Event finishes
    public int nextStage1;
    int nextStage1Chance;
    public int nextStage2;
    int nextStage2Chance; 

    //Full Constructor, no chance based outcome
    public EventOption(string buttonText, int nxtStge1)
    {
        optionButtonText = buttonText;
        nextStage1 = nxtStge1;
        nextStage1Chance = 1;
        nextStage2 = 0;
        nextStage2Chance = 0;    
    }

    //Full Constructor, chance based outcome with two choices
    public EventOption(string buttonText, int nxtStge1, int nxtStge1Chance, int nxtStge2, int nxtStge2Chance)
    {
        optionButtonText = buttonText;
        nextStage1 = nxtStge1;
        nextStage1Chance = nxtStge1Chance;
        nextStage2 = nxtStge2;
        nextStage2Chance = nxtStge2Chance;
    }

    //Empty, Null, Dummy Constructor
    public EventOption()
    {
        optionButtonText = null;
        nextStage1 = -1;
    }

    public int ChooseNextStage()
    {
        int chosenStage = 0;
        int randomRangeMax = nextStage1Chance + nextStage2Chance; 
        if (Random.Range(1, randomRangeMax + 1) > nextStage2Chance) //+1 Because Random.Range with ints is NOT maximally inclusive
        {
            chosenStage = nextStage1;
        }
        else
        {
            chosenStage = nextStage2;
        }
        return chosenStage;
    }
}
