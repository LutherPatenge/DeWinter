using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour {

    static GameData instance = null;
    
    public static int moneyCount;
    public static int currentDay;
    public static int currentMonth;
    public static int startMonthInt;

    //Reputation and Faction Stuff
    public static Dictionary<string, Faction> factionList = new Dictionary<string, Faction>();
    public static int reputationCount;

    //Party Stuff
    public static Party tonightsParty;
    public static int lastDay;
    public static int lastMonth;
    //For the RSVP Sysstem
    public static int partyRowID;
    public static int partyColumnID;
    //For the Rooms
    public static List<string> roomAdjectiveList = new List<string>();
    public static List<string> roomNounList = new List<string>();

    //Outfit Stuff. The Values start at -1 because I can't use null and I need an ID number that will never appear in the list.
    public static int partyOutfitID;
    //Used for seeing if the same Outfit was used twice in a row
    public static int lastPartyOutfitID;
    public static bool woreSameOutfitTwice; 
    public static int noveltyDamage;

    //Style Stuff
    public static string currentStyle;
    public static string nextStyle;
    public static int lastStyleSwitch;
    public static int nextStyleSwitch;
    public static double outOfStylePriceMultiplier; //Effect on Outfit price it gets for being out of Style

    //Event Stuff. Can be "party" or "night";
    public static int eventChance; // 25 = 25% Chance of an event, etc...
    public static string nextEventTime;
    public static Event selectedEvent;
    public static int totalPartyEventWeight;
    public static int totalNightEventWeight;

    //Servant Stuff
    public static bool seamstressHired;
    public static double seamstressDiscount; //Seamstress Price will be 80% of normal price.
    public static int seamstressWage;
    public static int handmaidenWage;
    public static bool bodyGuardHired;
    public static bool spyMasterHired;

    //UI Stuff
    public static int activeModals;
    public static int displayMonthInt;

    //Calendar Stuff
    public static Calendar calendar;
    public static int gameLengthMonths;
    public static int gameLengthDays;

    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            print("Duplicate Game Data container self-destructing!");
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
        // If we go to the main menu then Reset all the Game Data Values
        if (SceneManager.GetActiveScene().name == "Start Menu")
        {
            ResetValues();
        }
    }

    public void ResetValues()
    {
        moneyCount = 600;
        currentDay = 0;
        currentMonth = 0;
        startMonthInt = currentMonth;

    //Reputation and Faction Stuff
        reputationCount = 300;
        factionList.Add("Crown", new Faction("Crown", 100, 100));
        factionList.Add("Church", new Faction("Church", 100, -100));
        factionList.Add("Military", new Faction("Military", 0, 0));
        factionList.Add("Bourgeoisie", new Faction("Bourgeoisie", -100, 100));
        factionList.Add("Revolution", new Faction("Revolution", -100, -100));

    //Party Stuff
        tonightsParty = null;
        lastDay = 0;
        lastMonth = currentMonth;
        //For the Rooms
        roomAdjectiveList.Add("Lovely");
        roomAdjectiveList.Add("Shady");
        roomAdjectiveList.Add("Gaudy");
        roomAdjectiveList.Add("Tasteful");
        roomAdjectiveList.Add("Smokey");
        roomAdjectiveList.Add("Dim");
        roomAdjectiveList.Add("Tasteless");
        roomAdjectiveList.Add("Crowded");
        roomAdjectiveList.Add("Open");
        roomAdjectiveList.Add("Quiet");
        roomAdjectiveList.Add("Loud");
        roomAdjectiveList.Add("Drunken");
        roomAdjectiveList.Add("Riotous");
        roomAdjectiveList.Add("Peaceful");
        roomAdjectiveList.Add("Sedate");

        roomNounList.Add("Dining Room");
        roomNounList.Add("Ball Room");
        roomNounList.Add("Smoking Room");
        roomNounList.Add("Study");
        roomNounList.Add("Terrace");
        roomNounList.Add("Garden");
        roomNounList.Add("Living Room");
        roomNounList.Add("Gallery");
        roomNounList.Add("Banquet Hall");
        roomNounList.Add("Parlor");
        roomNounList.Add("Billiards Room");

        //Outfit Stuff. The Values start at -1 because I can't use null and I need an ID number that will never appear in the list.
        partyOutfitID = -1;
    //Used for seeing if the same Outfit was used twice in a row
        lastPartyOutfitID = -1;
        woreSameOutfitTwice = false;
        noveltyDamage = -20;

    //Style Stuff
        currentStyle = "Frankish";
        nextStyle = "Catalan";
        lastStyleSwitch = 0;
        nextStyleSwitch = Random.Range(6, 9);
        outOfStylePriceMultiplier = 0.75; //Effect on Outfit price it gets for being out of Style

    //Event Stuff. Can be "party" or "night";
        eventChance = 25; // 25 = 25% Chance of an event, etc...
        nextEventTime = "party";
        selectedEvent = null;

    //Servant Stuff
        handmaidenWage = 5;
        seamstressHired = false;
        seamstressDiscount = 0.8; //Seamstress Price will be 80% of normal price.
        seamstressWage = 25;
        bodyGuardHired = false;
        spyMasterHired = false;

    //UI Stuff
        activeModals = 0;
        displayMonthInt = currentMonth;

        //Calendar Stuff
        calendar = new Calendar();

        gameLengthMonths = 4;
        for (int i = 0; i < (startMonthInt + gameLengthMonths); i++)
        {
            gameLengthDays = gameLengthDays + calendar.monthList[GameData.startMonthInt + i].days;
        }
    }
}
