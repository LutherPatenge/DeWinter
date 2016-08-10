using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    void Start()
    {
        TimeCheck(SceneManager.GetActiveScene().name);
    }

    public void LoadLevel(string sceneName)
    {
        Debug.Log ("New Level load: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadLevelEventChance(string sceneName)
    {
        int randomRangeMax = 100;
        if (Random.Range(1, randomRangeMax+1) > GameData.eventChance) //Random.Range with ints is NOT maximally inclusive
        {
            LoadLevel(sceneName);
        }
        else
        {
            LoadLevel("Game_Event");
        }
    }

    public void EventLoadLevel()
    {
        if(GameData.nextEventTime == "night")
        {
            LoadLevel("Game_Estate");
        } else if (GameData.nextEventTime == "party")
        {
            LoadLevel("Game_AfterPartyReport");
        }
    }

    //New Version related to the Party System
    //TO DO: Fix Event System as it relates to Parties
    public void AdvanceTimeLoadLevel() 
    {
        TimeCheck("Game_Estate");
        if (GameData.tonightsParty.faction == null || GameData.tonightsParty.RSVP == -1 || GameData.tonightsParty.RSVP == 0)  // If there's no party tonight OR if it hasn't been RSVP'd to
        {
            AdvanceTime();
            LoadLevelEventChance("Game_Estate");
        }
        else
        {
            LoadLevel("Game_PartyLoadOut");
            AdvanceTime();
        }
    }

    public void StartPartyLevel()
    {
        if (GameData.partyOutfitID != -1)
        {
            LoadLevel("Game_Party");
        } else
        {
            Debug.Log("No Outfit selected :(");
        }
    }

    void TimeCheck(string sceneName)
    {
        if (GameData.tonightsParty != null) //Error Protection Layer
        {
            if (sceneName == "Game_AfterPartyReport")
            {
                GameData.nextEventTime = "night";

            }
            else if (sceneName == "Game_Estate" && (GameData.tonightsParty.RSVP == -1 || GameData.tonightsParty.RSVP == 0))
            {
                GameData.nextEventTime = "night";
            }
            else if (sceneName == "Game_Estate" && GameData.tonightsParty.RSVP == 1)
            {
                GameData.nextEventTime = "party";
            }
            Debug.Log("Next Event Time is:" + GameData.nextEventTime);
        }
    }

    //TO DO: Count for rolling over years
    void AdvanceTime()
    {
        GameData.currentDay += 1;
        if(GameData.currentDay > GameData.calendar.monthList[GameData.currentMonth].days)
        {
            GameData.currentDay = 1;
            GameData.currentMonth += 1;
        }
       
    }

    public void QuitRequest()
    {
        Debug.Log("Quit requested");
        Application.Quit();
    }
}
