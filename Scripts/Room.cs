using UnityEngine;
using System.Collections;

public class Room {

    public Party party; //What party is this Room part of?
    public int starRating; //How difficult is this Room? 1-5 Stars
    public bool cleared; //Has this Room been Cleared?

    public string name; //Randomly Generated Room Name
    public int xPos; //Where is this room in the Party's Room Grid?
    public int yPos; // Same
    public int entranceDistance = -1; //Distance from the entrance of the Party. Assigned via the flood fill algorithm. Starts at -1 to find stragglers

    public bool punchBowl; // Is there a Punch Bowl in this Room?
    public bool hostHere; // Is the Host/Hostess in this Room?
    public bool entrance; // Is this Room the Entrance to the Party?
    public bool playerHere; //Is the Player in this Room?
    public bool playerAdjacent; //Is the Player Next to this room?

    public Room(Party p, int x, int y)
    {
        party = p;
        xPos = x;
        yPos = y;
        name = GenerateName();
    }

    public Room(Party p, int x, int y, int star, bool hH, bool e)
    {
        party = p;
        xPos = x;
        yPos = y;
        starRating = star;
        hostHere = hH;
        entrance = e;
        name = GenerateName();

        cleared = false;
        if (!entrance)
        {
            int punchBowlRandom = Random.Range(0, 6);
            if (punchBowlRandom == 5)
            {
                punchBowl = true;
            } else
            {
                punchBowl = false;
            }
        }
    }

    string GenerateName()
    {
        if (entrance)
        {
            return "Entrance";
        } else
        {
            string adjective;
            string noun;
            int adjectiveInt = Random.Range(0, GameData.roomAdjectiveList.Count);
            int nounInt = Random.Range(0, GameData.roomNounList.Count);
            adjective = GameData.roomAdjectiveList[adjectiveInt];
            noun = GameData.roomNounList[nounInt];
            return "The " + adjective + " " + noun;
        }
    }

    public int MoveThroughChance()
    {
        if (!cleared)
        {
            return 100 - ((starRating * 10) + 10);
        } else
        {
            return 90;
        }
    }

    void WorkTheRoom()
    {

    }

    void MoveThrough()
    {
        if (true)
        {
            Ambush();
        }
    }

    void Ambush()
    {

    }
}
