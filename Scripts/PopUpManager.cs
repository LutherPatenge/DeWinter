using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour {

    public GameObject messageModal;
    public GameObject rSVPModal;
    public GameObject cancellationModal;
    public GameObject buyOrSellModal;
    public GameObject confidenceTallyModal;
    public GameObject roomChoiceModal;

    void CreateFashionPopUp(object[] stringStorage)
    { 
        string oldStyle = stringStorage[0] as string;
        string newStyle = stringStorage[1] as string;
        //Make the Pop Up
        GameObject popUp = Instantiate(messageModal) as GameObject;
        popUp.transform.SetParent(gameObject.transform, false);
        //Title Text
        Text titleText = popUp.transform.Find("TitleText").GetComponent<Text>();
        titleText.text = "Style Change!";
        //Body Text
        Text bodyText = popUp.transform.Find("BodyText").GetComponent<Text>();
        bodyText.text = "The " + oldStyle + " Style is now out of fashion and " +
            "\nthe " + newStyle + " Style is now in fashion!" +
            "\nAdjust your wardrobe accordingly!";
        //Modal Background Shift
        BroadcastMessage("ActiveModal");
    }

    void CreatePayDayPopUp(object[] stringStorage)
    {
        string servantsHired = stringStorage[0] as string;
        string totalWages = stringStorage[1] as string;
        string moneyAfterPay = stringStorage[2] as string;
        //Make the Pop up
        GameObject popUp = Instantiate(messageModal) as GameObject;
        popUp.transform.SetParent(gameObject.transform, false);
        //Title Text
        Text titleText = popUp.transform.Find("TitleText").GetComponent<Text>();
        titleText.text = "It's Payday";
        //Body Text (Update with more Servants)
        Text bodyText = popUp.transform.Find("BodyText").GetComponent<Text>();
        bodyText.text = "It's time to pay the help. You're currently employing the " + servantsHired + "." +
            "\nTheir cost of employment this week is " + totalWages + "." +
            "\nThis leaves you with " + moneyAfterPay;
        //Modal Background Shift
        BroadcastMessage("ActiveModal");
    }

    void CreateRSVPPopUp(object[] objectStorage)
    {
        Day partyDay = objectStorage[0] as Day;
        GameData.partyRowID = (int)objectStorage[1];
        GameData.partyColumnID = (int)objectStorage[2];
        string partyFaction = partyDay.party1.faction;
        string partySize = partyDay.party1.SizeString();
        //Make the Pop up
        GameObject popUp = Instantiate(rSVPModal) as GameObject;
        popUp.transform.SetParent(gameObject.transform, false);
        //Title Text
        Text titleText = popUp.transform.Find("TitleText").GetComponent<Text>();
        titleText.text = "RSVP";
        //Body Text (Update with Spymaster)
        Text bodyText = popUp.transform.Find("BodyText").GetComponent<Text>();
        bodyText.text = "You've been invited to a " + partySize + " Party being held by the " + partyFaction + "." +
            "\nWould you like to attend?";
        //Modal Background Shift
        BroadcastMessage("ActiveModal");
    }

    void CreateCancellationPopUp(object[] objectStorage)
    {
        Day partyDay = objectStorage[0] as Day;
        GameData.partyRowID = (int)objectStorage[1];
        GameData.partyColumnID = (int)objectStorage[2];
        string partyFaction = partyDay.party1.faction;
        string partySize = partyDay.party1.SizeString();
        //Make the Pop up
        GameObject popUp = Instantiate(cancellationModal) as GameObject;
        popUp.transform.SetParent(gameObject.transform, false);
        //Title Text
        Text titleText = popUp.transform.Find("TitleText").GetComponent<Text>();
        titleText.text = "Cancel Your RSVP?";
        //Body Text
        Text bodyText = popUp.transform.Find("BodyText").GetComponent<Text>();
        bodyText.text = "You've already agreed to attend the " + partySize + " Party being held by the " + partyFaction + "." +
            "\nCancelling will harm your reputation." +
            "\nWould you like cancel?";
        //Modal Background Shift
        BroadcastMessage("ActiveModal");
    }

    void CreateBuyOrSellModal(object[] objectStorage)
    {
        //Info Is Parsed Out Here
        string inventoryType = objectStorage[0] as string;
        int inventoryNumber = (int)objectStorage[1];
        int outfitPrice;

        //Make the Pop up
        GameObject popUp = Instantiate(buyOrSellModal) as GameObject;
        popUp.transform.SetParent(gameObject.transform, false);
        Text bodyText = popUp.transform.Find("BodyText").GetComponent<Text>();
        Text titleText = popUp.transform.Find("TitleText").GetComponent<Text>();

        //Set the Pop Up Values
        BuyAndSellPopUpController controller = popUp.GetComponent<BuyAndSellPopUpController>();
        controller.inventoryType = inventoryType;
        controller.inventoryNumber = inventoryNumber;

        //Fill in the Text
        if (inventoryType == "personal")
        {
            titleText.text = "Sell This?";
            outfitPrice = (int)(OutfitInventory.outfitInventories[inventoryType][inventoryNumber].price * 0.5); //Items are at Half Price from the Player Inventory to the Merchant
            bodyText.text = "Are you sure you want to sell this " + OutfitInventory.outfitInventories[inventoryType][inventoryNumber].Name() + " for " + outfitPrice.ToString("£" + "#,##0") + "?";
        } else
        {
            titleText.text = "Buy This?";
            outfitPrice = OutfitInventory.outfitInventories[inventoryType][inventoryNumber].price;
            if (GameData.seamstressHired) // Seamstress gives a Discount on stuff in the Merchant Shop
            {
                outfitPrice = (int)(outfitPrice * GameData.seamstressDiscount);
            }
            bodyText.text = "Are you sure you want to buy this " + OutfitInventory.outfitInventories[inventoryType][inventoryNumber].Name() + " for " + outfitPrice.ToString("£" + "#,##0") + "?";
        }
        //Modal Background Shift
        BroadcastMessage("ActiveModal");
    }

    void CreateCantAffordModal(object[] objectStorage)
    {
        int inventoryNumber = (int)objectStorage[0];

        //Make the Pop Up
        GameObject popUp = Instantiate(messageModal) as GameObject;
        popUp.transform.SetParent(gameObject.transform, false);
        //Title Text
        Text titleText = popUp.transform.Find("TitleText").GetComponent<Text>();
        titleText.text = "Oh No!";
        //Body Text
        Text bodyText = popUp.transform.Find("BodyText").GetComponent<Text>();
        bodyText.text = "I'm sorry Madamme, but you do not have enough Livres to afford this " + OutfitInventory.outfitInventories["merchant"][inventoryNumber].Name() + ". " +
            "\nPerhaps you could sell some of your existing wardrobe?";
        //Modal Background Shift
        BroadcastMessage("ActiveModal");
    }

    void CreateCantFitModal(object[] objectStorage)
    {
        int merchantInventoryNumber = (int)objectStorage[0];
        int maxInventoryNumber = (int)objectStorage[1];

        //Make the Pop Up
        GameObject popUp = Instantiate(messageModal) as GameObject;
        popUp.transform.SetParent(gameObject.transform, false);
        //Title Text
        Text titleText = popUp.transform.Find("TitleText").GetComponent<Text>();
        titleText.text = "Oh No!";
        //Body Text
        Text bodyText = popUp.transform.Find("BodyText").GetComponent<Text>();
        bodyText.text = "I'm sorry Madamme, but you do not have enough space in your wardobe to fit this " + OutfitInventory.outfitInventories["merchant"][merchantInventoryNumber].Name() + ". " +
            "\nAt this time your Wardrobe can only hold " + maxInventoryNumber + " Outfits. Perhaps there is some way to expand your closet space?";
        //Modal Background Shift
        BroadcastMessage("ActiveModal");
    }

    void CreateConfidenceTallyModal(object[] objectStorage)
    {
        int partyOutfitID = (int)objectStorage[0];
        string partyFaction = objectStorage[1].ToString();
        int outfitReaction = (int)objectStorage[2];
        int styleReaction = (int)objectStorage[3];
        int factionReaction = (int)objectStorage[4];
        int generalRepReaction = (int)objectStorage[5];
        int maxConfidence = (int)objectStorage[6];
        int currentConfidence = (int)objectStorage[7];

        //Make the Pop Up
        GameObject popUp = Instantiate(confidenceTallyModal) as GameObject;
        popUp.transform.SetParent(gameObject.transform, false);
        //Title Text
        Text titleText = popUp.transform.Find("TitleText").GetComponent<Text>();
        titleText.text = "Welcome to The Party!";
        //Body Text
        Text bodyText = popUp.transform.Find("BodyText").GetComponent<Text>();
        string line1;
        string line2;
        string line3;
        string line4;
        string line5;
        string line6;
        line1 = "You wore your " + OutfitInventory.personalInventory[partyOutfitID].Name() + " to the event, hosted by the " + partyFaction + ".";
        if (partyFaction != "military")
        {
            if (outfitReaction > 10) //If it's a positive reaction
            {
                line2 = "\nEveryone seems to like your choice of Outfit. (+" + outfitReaction + " Max Confidence)";
            } else if (outfitReaction < 10) //If it's a negative reaction
            {
                line2 = "\nOh no! Nobody seems to like your Outfit. This is bad. (+" + outfitReaction + " Max Confidence)";
            }
            else //If it's just in the middle
            {
                line2 = "\nNobody seems to notice your Outfit. It hasn't really made an impression. (+" + outfitReaction + " Max Confidence)";
            }
        } else
        {
            line2 = "\nHowever, the Military doesn't know fashion so they give you a pass. (+" + outfitReaction + " Max Confidence)";
        }
        if (styleReaction > 0)
        {
            line3 = "\nYour Outfit's in style with the latest in " + GameData.currentStyle + " fashion! (+" + styleReaction + " Max Confidence)"; 
        } else
        {
            line3 = "\nOh no! Your Outfit is in the " + OutfitInventory.personalInventory[partyOutfitID].style + "Style and it appears that " + GameData.currentStyle + " is in vogue at the moment. (+" + styleReaction + " Max Confidence)";
        }
        line4 = "\nThe " + partyFaction + ", of course have their opinion on you... (+" + factionReaction + " Max Confidence)";
        line5 = "\nSociety as a whole also has their opinions. (+" + generalRepReaction + " Max Confidence)";
        line6 = "\nOverall your Maximum Confidence is at " + maxConfidence + " and your Current Confidence is " + currentConfidence;
        bodyText.text = line1 + line2 + line3 + line4 + line5 + line6;
    //Modal Background Shift
    BroadcastMessage("ActiveModal");
    }

    void CreateRoomChoiceModal(int[] intStorage)
    {
        int xPos = intStorage[0];
        int yPos = intStorage[1];

        //Make the Pop Up
        GameObject popUp = Instantiate(roomChoiceModal) as GameObject;
        popUp.transform.SetParent(gameObject.transform, false);
        //Title Text
        Text titleText = popUp.transform.Find("TitleText").GetComponent<Text>();
        titleText.text = GameData.tonightsParty.Name();
        //Body Text Stays the Same (for now)
        Text bodyText = popUp.transform.Find("BodyText").GetComponent<Text>();

        Text moveThroughText = popUp.transform.Find("MoveThroughButton").Find("Text").GetComponent<Text>();
        moveThroughText.text = "Move Through (" + GameData.tonightsParty.roomGrid[xPos, yPos].MoveThroughChance().ToString() + "%)";

        //Modal Background Shift
        BroadcastMessage("ActiveModal");
    }
}
