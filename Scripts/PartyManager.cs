using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PartyManager : MonoBehaviour {
    public GameObject screenFader; // It's for the Confidence Tally pop-up
    public RoomManager roomManager;

    void Start()
    {
        ConfidenceTally();
        EngageParty();
        GameData.tonightsParty.roomGrid[GameData.tonightsParty.entranceRoom.xPos, GameData.tonightsParty.entranceRoom.yPos].playerHere = true;
    }

    void Update() {


    }

    //To Do: Update this to handle the Party Map and Such
    void EngageParty()
    {
        //Instantiate a the room Holder Parent Object
        roomManager.SetUpMap(GameData.tonightsParty);
        OutfitDegradation();
    }

    void ConfidenceTally()
    {
        int maxConfidence = 0;
        int currentConfidence = 0;
        //Calculate Confidence Values Here------------
        //Faction Outfit Likes (Military doesn't know anything)
        int outfitReaction;
        if (GameData.tonightsParty.faction != "military")
        {
            int modestyLike = GameData.factionList[GameData.tonightsParty.faction].modestyLike;
            int luxuryLike = GameData.factionList[GameData.tonightsParty.faction].luxuryLike;
            int outfitModesty = OutfitInventory.personalInventory[GameData.partyOutfitID].modesty;
            int outfitLuxury = OutfitInventory.personalInventory[GameData.partyOutfitID].luxury;
            //Fix this formula
            outfitReaction = (modestyLike - outfitModesty) + (luxuryLike - outfitLuxury);
            maxConfidence += outfitReaction;
        } else {
            outfitReaction = 50;
            maxConfidence += outfitReaction;
        }
        //Is it in Style?
        int styleReaction;
        if (GameData.currentStyle == OutfitInventory.personalInventory[GameData.partyOutfitID].style)
        {
            styleReaction = 25;
        } else
        {
            styleReaction = 0;
        }
        maxConfidence += styleReaction;
        //Faction Rep
        int factionReaction = GameData.factionList[GameData.tonightsParty.faction].playerReputation / 10;
        maxConfidence += factionReaction;
        //General Rep
        int generalRepReaction = GameData.reputationCount / 20;
        maxConfidence += generalRepReaction;
        currentConfidence = maxConfidence;
        //Put Results in the Pop-Up Here
        object[] objectStorage = new object[8];
        objectStorage[0] = GameData.partyOutfitID;
        objectStorage[1] = GameData.tonightsParty.faction;
        objectStorage[2] = outfitReaction;
        objectStorage[3] = styleReaction;
        objectStorage[4] = factionReaction;
        objectStorage[5] = generalRepReaction;
        objectStorage[6] = maxConfidence;
        objectStorage[7] = currentConfidence;
        screenFader.gameObject.SendMessage("CreateConfidenceTallyModal", objectStorage);
    }

    //To Do: Make it Tally Up the Results Properly
    public void FinishTheParty()
    {
        GameData.reputationCount += 100;
        GameData.factionList[GameData.tonightsParty.faction].playerReputation += 50;
    }

    void OutfitDegradation()
    {
        //Reduce Novelty of Outfit. If Outfit has been used twice in a row then it's lowered double.
        if (GameData.partyOutfitID != GameData.lastPartyOutfitID)
        {
            OutfitInventory.outfitInventories["personal"][GameData.partyOutfitID].novelty += GameData.noveltyDamage;
            GameData.woreSameOutfitTwice = false;
        }
        else
        {
            OutfitInventory.outfitInventories["personal"][GameData.partyOutfitID].novelty += GameData.noveltyDamage * 2;
            GameData.woreSameOutfitTwice = true;
        }
        //Now that the calculations are finished, the outfit now becomes the last used outfit.
        GameData.lastPartyOutfitID = GameData.partyOutfitID;
    }
}
