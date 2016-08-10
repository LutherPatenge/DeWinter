using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour {

    public Text titleText;
    public Text descriptionText;

    void Start()
    {
        Debug.Log("Current Scene is " + SceneManager.GetActiveScene().name);
        //This thing needs to run at the start of the Game but not kick off an Event. It checks to see if this scene is the Event Scene.
        if (SceneManager.GetActiveScene().name == "Game_Event")
        {
            EventStart();
        }
    }
    
    public void ChangeReputation(int repChange)
    {
        GameData.reputationCount += repChange;
    }
    
    void EventStart()
    {
        //Select the Event
        WeightedSelection(GameData.nextEventTime);
        //Reset the Event to its beginning
        GameData.selectedEvent.currentStage = 0;
        //Text and Title
        titleText.text = GameData.selectedEvent.eventTitle;
        descriptionText.text = GameData.selectedEvent.eventStages[0].description;
    }

    //TO DO: Fix the Weighted Selection System, this fucking thing
    public void WeightedSelection(string eventTime)
    {
        int randomSelection = 0;
        if (eventTime == "party")
        {
            randomSelection = Random.Range(0, EventInventory.eventInventories["party"].Count); // Doesn't need a +1, as Random.Range with ints isn't maximally inclusive

        }
        else if (eventTime == "night")
        {
            randomSelection = Random.Range(0, EventInventory.eventInventories["night"].Count); // Doesn't need a +1, as Random.Range with ints isn't maximally inclusive
        }
        GameData.selectedEvent = EventInventory.eventInventories[eventTime][randomSelection];
        /*
        int randomSelection = 0;
        if (eventTime == "party")
        {
            randomSelection = Random.Range(0, GameData.totalPartyEventWeight); // Doesn't need a +1, as Random.Range with ints isn't maximally inclusive

        }
        else if(eventTime == "night")
        {
            randomSelection = Random.Range(0, GameData.totalNightEventWeight); // Doesn't need a +1, as Random.Range with ints isn't maximally inclusive
        }
        int counter = 0;
        int i = 0;
        while (counter < randomSelection)
        {
            counter += EventInventory.eventInventories[eventTime][i].eventWeight;
            if (counter > randomSelection)
            {
                GameData.selectedEvent = EventInventory.eventInventories[eventTime][i];
            }
            else
            {
                i++;
            }
        }
        */
    }

    public void EventOptionSelect(int option)
    {
        int nextStage = GameData.selectedEvent.eventStages[GameData.selectedEvent.currentStage].stageEventOptions[option].ChooseNextStage();
        //Step 0: Did this just complete the event? If so then switch levels
        if (nextStage == -1)
        {
            //Switch Levels Here
            EventLoadLevel();
        }
        
        //Step 1: Which stage do I advance to?
        GameData.selectedEvent.currentStage = nextStage;
        Debug.Log("This stage is now " + nextStage);
        
        //Step 2: What's the Description Text say now? The Event Option Buttons should update on their own
        descriptionText.text = GameData.selectedEvent.eventStages[GameData.selectedEvent.currentStage].description;

        //Step 3: Get the Money, Change the Rep
        GameData.selectedEvent.EventStageRewards();
    }


    //These methods being here is SUPER fucking hax and needs to be cleaned up at the first opportunity
    void LoadLevel(string sceneName)
    {
        //TimeCheck(sceneName); <- The Time Check has been moved to whenever the a scene starts, might improve reliability
        Debug.Log("New Level load: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    void EventLoadLevel()
    {
        if (GameData.nextEventTime == "night")
        {
            LoadLevel("Game_Estate");
        }
        else if (GameData.nextEventTime == "party")
        {
            LoadLevel("Game_AfterPartyReport");
        }
    }
}
