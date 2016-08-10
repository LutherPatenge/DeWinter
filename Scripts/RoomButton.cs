using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoomButton : MonoBehaviour {

    public Room myRoom;
    public RoomManager roomManager;
    Text myDescriptionText;
    Outline myOutline; // Indicates that the Player is there
    Image myPunchBowlImage;

	// Use this for initialization
	void Start () {
        myDescriptionText = this.transform.Find("DescriptionText").GetComponent<Text>();
        myOutline = this.GetComponent<Outline>();
        myPunchBowlImage = this.transform.Find("PunchBowlImage").GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!myRoom.entrance)
        {
            if (!myRoom.hostHere)
            {
                //myDescriptionText.text = myRoom.name + "\n(" + myRoom.entranceDistance.ToString() + " Distance)";
                myDescriptionText.text = myRoom.name + "\n(" + myRoom.starRating.ToString() + " Stars)";
            } else
            {
                myDescriptionText.text = myRoom.name + "\n(Host Here)";
            }
        } else
        {
            myDescriptionText.text = myRoom.name;
        }
        //Is the player here? If so, Outline
        if (myRoom.playerHere)
        {
            myOutline.effectColor = Color.black;
        } else if (myRoom.playerAdjacent)
        {
            myOutline.effectColor = Color.yellow;
        } else
        {
            myOutline.effectColor = Color.clear;
        }
        //Is there a Punch Bowl? If so, display the Icon
        if (myRoom.punchBowl)
        {
            myPunchBowlImage.color = Color.white;
        }
        else
        {
            myPunchBowlImage.color = Color.clear;
        }
    }

    public void Move()
    {
        if (myRoom.playerAdjacent && myRoom.party.turnsLeft > 0)
        {
            roomManager.PlayerMovement(myRoom.xPos, myRoom.yPos);
            //Tell Room Manager to set the other Player here to False
            roomManager.ChoiceModal(myRoom.xPos, myRoom.yPos);
        } else
        {
            Debug.Log("Can't move there, sorry :(");
        }

    }
}
