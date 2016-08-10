using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Party {

    public string faction;
    public int partySize;
    public int RSVP = 0; //0 means no RSVP yet, 1 means Attending and -1 means Decline
    public int playerRSVPDistance = -1;
    public int modestyPreference;
    public int luxuryPreference;

    public string description; // Randomly Generated Flavor Description

    public Room[,] roomGrid;
    public Room entranceRoom;
    public int turns;
    public int turnsLeft;

    // Default Constructor
    public Party()
    {
        int partyFaction = Random.Range(0, 7);
        if (partyFaction == 0)
        {
            faction = "Crown";
            modestyPreference = GameData.factionList[faction].modestyLike;
            luxuryPreference = GameData.factionList[faction].luxuryLike;
        }
        if (partyFaction == 1)
        {
            faction = "Church";
            modestyPreference = GameData.factionList[faction].modestyLike;
            luxuryPreference = GameData.factionList[faction].luxuryLike;
        }
        if (partyFaction == 2)
        {
            faction =  "Military";
            modestyPreference = GameData.factionList[faction].modestyLike;
            luxuryPreference = GameData.factionList[faction].luxuryLike;
        }
        if (partyFaction == 3)
        {
            faction = "Bourgeoisie";
            modestyPreference = GameData.factionList[faction].modestyLike;
            luxuryPreference = GameData.factionList[faction].luxuryLike;
        }
        if (partyFaction == 4)
        {
            faction = "Revolution";
            modestyPreference = GameData.factionList[faction].modestyLike;
            luxuryPreference = GameData.factionList[faction].luxuryLike;
        } else if (partyFaction == 5 || partyFaction == 6) // Doubles the odds of a blank day
        {
            faction = null;
        }
        partySize = Random.Range(1, 5);
        GenerateRandomDescription();
        GenerateRooms();
        turns = partySize * 2;
        turnsLeft = turns;
    }

    void GenerateRandomDescription()
    {
        description = "This party is being hosted by some dude or dudette. This segment will later have randomly generated Text describing the party. It should be pretty damn funny.";
    }

    void GenerateRooms()
    {
        //Set the Party Size
        roomGrid = new Room [partySize + 1, partySize + 1];
        //Fill the Grid with Randomized Rooms
        for(int i = 0; i < partySize+1; i++)
        {
            for (int j = 0; j < partySize+1; j++)
            {
                roomGrid[i, j] = new Room(this, i, j);
            }
        }
        //Delete a few Random Rooms (Party Size amount) (Increase this later)
        for (int i = 0; i < partySize; i++)
        {
                roomGrid[Random.Range(0,partySize+1), Random.Range(0, partySize + 1)] = null;
        }
        //Set the Entrance (Random, Southern-most Room)
        Room selectedRoom = null;
        while (selectedRoom == null){ //Just in case it selects a null cell
            selectedRoom = roomGrid[Random.Range(0, partySize + 1), 0];
        }
        roomGrid[selectedRoom.xPos, selectedRoom.yPos].entrance = true;
        roomGrid[selectedRoom.xPos, selectedRoom.yPos].entranceDistance = 0;

        roomGrid[selectedRoom.xPos, selectedRoom.yPos].name = "The Entrance";
        entranceRoom = roomGrid[selectedRoom.xPos, selectedRoom.yPos]; // This is for the Party Manager Later
        //Flood fill to set Room Entrance Distance 
        floodFill(roomGrid, selectedRoom.xPos, selectedRoom.yPos, 0);

        //Delete any stragglers (Rooms for which Entrance Distance equals -1, as they weren't touched by flood fill)
        //Also Set the Host (the furthest point from the Entrance)
        //TO DO: Set Star Ratings
        //Also Punch Bowls randomly throughout
        Room furthestRoom = selectedRoom; // Lowest possible distance Room, the entrance
        for (int i = 0; i < partySize + 1; i++)
        {
            for (int j = 0; j < partySize + 1; j++)
            {
                if (roomGrid[i, j] != null) // Safety Measure
                {
                    if (roomGrid[i, j].entranceDistance == -1) //Is it untouched by Flood Fill?
                    {
                        roomGrid[i, j] = null; // Kill the bastard Room
                    } else
                    {
                        if (roomGrid[i,j].entranceDistance > furthestRoom.entranceDistance)
                        {
                            furthestRoom = roomGrid[i, j];
                        }
                        if (Random.Range(1, 5) == 1 && !roomGrid[i,j].entrance) //Chance of Random Punch Bowl, none in the Entrance
                        {
                            roomGrid[i, j].punchBowl = true;
                        }
                        if (!roomGrid[i, j].entrance && !roomGrid[i, j].hostHere)
                        {
                            roomGrid[i, j].starRating = Random.Range(1, 6);
                        }
                    }
                }
            }
        }
        roomGrid[furthestRoom.xPos, furthestRoom.yPos].hostHere = true; //The Furthest Room is the Host

    }

    void floodFill(Room [,] rg, int xPosInt, int yPosInt, int currentDistance)
    {
        currentDistance++;
        roomGrid[xPosInt, yPosInt].entranceDistance = currentDistance;
        if ((yPosInt + 1) < rg.GetLength(1))
        {
            Room northRoom = roomGrid[xPosInt, yPosInt + 1];
            if (northRoom != null)
            {
                if (rg[northRoom.xPos, northRoom.yPos].entranceDistance == -1)
                {

                    floodFill(roomGrid, northRoom.xPos, northRoom.yPos, currentDistance);
                }
            }    
        }
        if ((yPosInt - 1) >= 0)
        {
            Room southRoom = roomGrid[xPosInt, yPosInt - 1];
            if (southRoom != null)
            {
                if (rg[southRoom.xPos, southRoom.yPos].entranceDistance == -1)
                {
                    floodFill(roomGrid, southRoom.xPos, southRoom.yPos, currentDistance);
                }
            }  
        }
        if ((xPosInt + 1) < rg.GetLength(0))
        {
            Room eastRoom = roomGrid[xPosInt + 1, yPosInt];
            if (eastRoom != null)
            {
                if (rg[eastRoom.xPos, eastRoom.yPos].entranceDistance == -1)
                {
                    floodFill(roomGrid, eastRoom.xPos, eastRoom.yPos, currentDistance);
                }
            } 
        }
        if ((xPosInt - 1) >= 0)
        {
            Room westRoom = roomGrid[xPosInt - 1, yPosInt];
            if (westRoom != null)
            {
                if (rg[westRoom.xPos, westRoom.yPos].entranceDistance == -1)
                {
                    floodFill(roomGrid, westRoom.xPos, westRoom.yPos, currentDistance);
                }
            }   
        }
    }

    //To Do: Obsolete this --------------
    public int PartyResolution(int outfitID)
    {
        return 50;
    }

    public string SizeString()
    {
        if (partySize == 1)
        {
            return "Tiny";
        } else if (partySize == 2)
        {
            return "Small";
        } else if (partySize == 3)
        {
            return "Medium"; 
        } else if (partySize == 4)
        {
            return "Large";
        } else
        {
            return "Nothing";
        }
    }

    //To Do: Obsolete this --------------
    public List<int> ReportInfo()
    {
        List<int> info = new List<int>();
        //info[0] = Total Reputation Change
        info.Add(PartyResolution(GameData.partyOutfitID));
        //info[1] = Outfit Reputation Change
        info.Add(PartyResolution(GameData.partyOutfitID) - ((playerRSVPDistance - 2) * 20));
        //info[2] = RSVP Reptutation Change
        info.Add((playerRSVPDistance - 2) * 20);
        return info;
    }

    public string Name()
    {
        string name;
        name = SizeString() + " " + faction + " Party";
        return name;
    }

    public string Description()
    {
        return description;
    }

    public string Objective1()
    {
        return "- Charm the Host";
    }

    public string Objective2()
    {
        return "- Eat ALL the Hors D'oeuvres";
    }

    public string Objective3()
    {
        return "- Don't get too trashed";
    }

    public string Guest1()
    {
        return "- Vis-Prince Christophe Sagnier";
    }

    public string Guest2()
    {
        return "- Prince Emile Fauconier";
    }

    public string Guest3()
    {
        return "- Lady Volteza ";
    }
}
