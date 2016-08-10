using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour {

    public Party party; //What Party are we managing?
    public Room[, ] roomButtonGrid; // Holds the Rooms
    public Room entranceRoom; //Whichever Room is the Entrance
    public Room curentPlayerRoom;
    public GameObject roomButtonPrefab;
    public Canvas canvas;
    bool mapSetUp = false;
    public GameObject screenFader;

    void Start()
    {
        mapSetUp = false;
    }

    void Update()
    {
        if (mapSetUp) {
            ScanForPlayerAndAdjacents();
        }
    }

    public void SetUpMap(Party p)
    {
        party = p;
        GameObject roomHolder = new GameObject();
        roomHolder.transform.SetParent(canvas.transform, false);
        roomHolder.transform.localPosition = new Vector3(0, -175, 0);
        roomHolder.transform.SetAsFirstSibling();
        //Make the Room Buttons ----------------------
        //Positioning (Set Up)
        int buttonwidth = (int)roomButtonPrefab.GetComponent<RectTransform>().rect.width;
        int padding = 5; // Space between Rooms  
        int offsetFromCenterX = (GameData.tonightsParty.roomGrid.GetLength(0) * buttonwidth) / 2;
        for (int i = 0; i < GameData.tonightsParty.roomGrid.GetLength(0); i++)
        {
            for (int j = 0; j < GameData.tonightsParty.roomGrid.GetLength(1); j++)
            {
                if (GameData.tonightsParty.roomGrid[i, j] != null)
                {
                    //Parenting
                    GameObject mapButton = Instantiate(roomButtonPrefab, roomButtonPrefab.transform.position, roomButtonPrefab.transform.rotation) as GameObject;
                    mapButton.transform.SetParent(roomHolder.transform, false);
                    RoomButton roomButton = mapButton.GetComponent<RoomButton>();
                    //Positioning (Actual)
                    mapButton.transform.localPosition = new Vector3(((i * buttonwidth) + padding) - offsetFromCenterX, (j * buttonwidth) + padding, 0);
                    //Set the Room that this button represents
                    roomButton.myRoom = GameData.tonightsParty.roomGrid[i, j];
                    roomButton.roomManager = this;
                    Debug.Log("Room Created at Grid Square: " + i.ToString() + "," + j.ToString());
                }
            }
        }
        mapSetUp = true;
    }

    public void PlayerMovement(int xPos, int yPos)
    {
        if (party.turnsLeft > 0)
        {
            for (int i = 0; i < party.roomGrid.GetLength(0); i++) // Search via the X Axis
            {
                for (int j = 0; j < party.roomGrid.GetLength(1); j++) //Search via the Y Axis
                {
                    if (party.roomGrid[i, j] != null) //If this room is not null
                    {
                        party.roomGrid[i, j].playerHere = false;
                        party.roomGrid[i, j].playerAdjacent = false;
                    }
                }
            }
            party.roomGrid[xPos, yPos].playerHere = true;
            party.turnsLeft--;
        } else
        {
            Debug.Log("Out of turns. Go home!");
        }
       
    }

    void ScanForPlayerAndAdjacents()
    {
        for (int i = 0; i < party.roomGrid.GetLength(0); i++) // Search via the X Axis
        {
            for (int j = 0; j < party.roomGrid.GetLength(1); j++) //Search via the Y Axis
            {
                if (party.roomGrid[i, j] != null) //If this room is not null
                {
                    if (party.roomGrid[i, j].playerHere) { //Is the Player is in this Room
                        //North
                        if ((j + 1) < party.roomGrid.GetLength(1)) //Is the North Room on the Map?
                        {
                            Room northRoom = party.roomGrid[i, j + 1];
                            if (northRoom != null)
                            {
                                party.roomGrid[northRoom.xPos, northRoom.yPos].playerAdjacent = true;
                            }
                        }
                        //South
                        if ((j - 1) >= 0) //Is the South Room on the Map?
                        {
                            Room southRoom = party.roomGrid[i, j - 1];
                            if (southRoom != null)
                            {
                                party.roomGrid[southRoom.xPos, southRoom.yPos].playerAdjacent = true;
                            }
                        }
                        //East
                        if ((i + 1) < party.roomGrid.GetLength(0)) //Is the North Room on the Map?
                        {
                            Room eastRoom = party.roomGrid[i + 1, j];
                            if (eastRoom != null)
                            {
                                party.roomGrid[eastRoom.xPos, eastRoom.yPos].playerAdjacent = true;
                            }
                        }
                        //West
                        if ((i - 1) >= 0) //Is the North Room on the Map?
                        {
                            Room westRoom = party.roomGrid[i - 1, j];
                            if (westRoom != null)
                            {
                                party.roomGrid[westRoom.xPos, westRoom.yPos].playerAdjacent = true;
                            }
                        }
                    }
                }
            }
        }
    }

    public void ChoiceModal(int xPos, int yPos)
    {
        //Work the Room or Move Through
        int[] intStorage = new int[2];
        intStorage[0] = xPos;
        intStorage[1] = yPos;
        screenFader.gameObject.SendMessage("CreateRoomChoiceModal", intStorage);
    }
}
