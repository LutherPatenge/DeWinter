using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CalendarManager : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Game_Estate")
        {
            PrepareTonightsParty();
        }
    }

    void PrepareTonightsParty()
    {
        Debug.Log("Today is Day: " + GameData.currentDay + " Month: " + GameData.currentMonth);
        GameData.tonightsParty = GameData.calendar.today().party1;
        Debug.Log("Tonights Party is: a " + GameData.tonightsParty.partySize + " " + GameData.tonightsParty.faction + " Party");
    }
}
