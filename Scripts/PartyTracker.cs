using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PartyTracker : MonoBehaviour {
    private Text myText;

    void Start()
    {
        myText = GetComponent<Text>();
        //This is just to force an update at the beginning.
        updateParty();
    }

    void Update()
    {
        updateParty();
    }

    //TO DO: Account for Monthly Rollovers
    //This function controls the text that tells players what the next party is in the Wardrobe screen. Not a necessity but very helpful
    public void updateParty()
    {
        if (GameData.tonightsParty != null) { // Second layer of protection to prevent jams
            if (GameData.tonightsParty.faction != null) // Today's Party (if there is one)
            {
                myText.text = GameData.tonightsParty.SizeString() + " " + GameData.tonightsParty.faction + " Party";
            }
            else if (GameData.tonightsParty.faction == null && GameData.calendar.daysFromNow(1).party1.faction != null) //Tomorrow's Party (if there is one)
            {
                myText.text = GameData.calendar.daysFromNow(1).party1.SizeString() + " " + GameData.calendar.daysFromNow(1).party1.faction + " Party (Tomorrow)";
            }
            else //The Day After Tomorrow's Party (if there is one)
            {
                myText.text = GameData.calendar.daysFromNow(2).party1.SizeString() + " " + GameData.calendar.daysFromNow(2).party1.faction + " Party (The Day After Tomorrow)";
            }
        }
    }
}
