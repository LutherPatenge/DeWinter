using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AfterPartyReportTextController : MonoBehaviour {

    Text myText;

    // Use this for initialization
    void Start () {
        GameObject textObject = GameObject.Find("Text");
        myText = textObject.GetComponent<Text>();
        DisplayAfterPartyReportText(GameData.tonightsParty.ReportInfo());
    }
	
	public void DisplayAfterPartyReportText(List<int> reportInfo)
    {
        myText.text = "Temporary Text about the Party Goes Here";
        /*
        //These have to be declared here because the system gets grumpy about variables declared in If statements 
        string line0; string line1; string line2; string line3; string line4; string line5;
        int totalReputationChange = reportInfo[0];
        int outfitReputationChange = reportInfo[1];
        int rsvpReputationChange = reportInfo[2];

        //int totalReputationChange = GameData.tonightsParty.PartyResolution(GameData.partyOutfitID);
        // ---- Who Hosted the Party? ----
        line0 = "The " + GameData.tonightsParty.SizeString() + " Party was hosted by the " + GameData.tonightsParty.faction + ".";

        // ---- Total Reputation Change ----
        line1 = "\nYour Total Reputation change was " + totalReputationChange + ".";

        // ---- Factional Change ----
        line2 = "\n" + FactionText(GameData.tonightsParty.faction, totalReputationChange, GameData.tonightsParty.partySize);

        // ---- Outfit Stuff ----
        if ((totalReputationChange - ((GameData.tonightsParty.playerRSVPDistance - 2) * 20)) >= 0 )
        {
            if(OutfitInventory.outfitInventories["personal"][GameData.partyOutfitID].style == GameData.currentStyle)
            {
                line3 = "\nYour " + OutfitInventory.outfitInventories["personal"][GameData.partyOutfitID].style +
                " Outfit was recieved well and was in Style, improving your Reputation by " + outfitReputationChange + ".";
            } else
            {
                line3 = "\nYour " + OutfitInventory.outfitInventories["personal"][GameData.partyOutfitID].style +
                                " Outfit was recieved well, despite being out of Style, improving your Reputation by " + outfitReputationChange + ".";
            }    
        } else
        {
            if (OutfitInventory.outfitInventories["personal"][GameData.partyOutfitID].style == GameData.currentStyle)
            {
                line3 = "\nYour " + OutfitInventory.outfitInventories["personal"][GameData.partyOutfitID].style +
                " Outfit was recieved poorly, despite being in Style, damaging your Reputation by " + outfitReputationChange + ".";
            }
            else
            {
                line3 = "\nYour " + OutfitInventory.outfitInventories["personal"][GameData.partyOutfitID].style +
                                " Outfit was recieved poorly and was out of Style, damaging your Reputation by " + (totalReputationChange - ((GameData.tonightsParty.playerRSVPDistance - 2) * 20)) + ".";
            }
        }

        // ---- RSVP Bonuses ----
        line4 = "\nYou recieved " + ((GameData.tonightsParty.playerRSVPDistance - 2) * 20) + " Reputation for your early RSVP.";
        
        // ---- Novelty decay on your Outfit ----
        if (GameData.woreSameOutfitTwice == false)
        {
            line5 = "\nYour Outfit lost " + GameData.noveltyDamage + " Novelty.";
        }
        else
        {
            line5 = "\nYour Outfit lost " + GameData.noveltyDamage*2 + " Novelty. Try to avoid wearing the same Outfit twice in a row.";
        }
        Debug.Log("Party Outfit ID = " + GameData.partyOutfitID + ", Last Party Outfit ID = " + GameData.lastPartyOutfitID);

        // ---- Put it all together ----
        myText.text = line0 + line1 + line2 + line3 + line4 + line5;
        */
    }

    string FactionText(string faction, int reputationChange, int partySize)
    {
        if (faction == "Crown")
        {
            return "Crown Reputation Change: " + (reputationChange / partySize) + "\nRevolution Reputation Change: -" + (reputationChange / (partySize * 2));
        }
        else if (faction == "Church")
        {
            return "Church Reputation Change: " + (reputationChange / partySize);
        }
        else if (faction == "Military")
        {
            return "Military Reputation Change: " + (reputationChange / partySize);
        }
        else if (faction == "Bourgeoisie")
        {
            return "Bourgeoisie Reputation Change: " + (reputationChange / partySize);
        }
        else if (GameData.tonightsParty.faction == "Revolution")
        {
            return "Revolution Reputation Change: " + (reputationChange / partySize) + "\nCrown Reputation Change: -" + (reputationChange / (partySize * 2));
        } else
        {
            return "<The wrong faction got inputted, this is bad>";
        }
    }
}
