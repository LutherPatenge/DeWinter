using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CalendarButton : MonoBehaviour {

    public int rowID; //Button Row
    public int columnID; // Button Column?
    public GameObject screenFader; // It's for the RSVP pop-up

    private Text myText;
    private Text myDate;
    private Image myBlockImage;
    private Image myXImage;
    private Image myCircleImage;
    private Image mySlashImage;
    private Outline myOutline;
    private Color defaultColor;
    private Day buttonDay;

    int startPositionPlusDays;

    void Start()
    {
        myText = this.transform.Find("Text").GetComponent<Text>();
        myDate = this.transform.Find("DateText").GetComponent<Text>();
        myBlockImage = this.GetComponent<Image>();
        myXImage = this.transform.Find("XImage").GetComponent<Image>();
        myCircleImage = this.transform.Find("CircleImage").GetComponent<Image>();
        mySlashImage = this.transform.Find("SlashImage").GetComponent<Image>();
        myOutline = this.GetComponent<Outline>();
        defaultColor = myBlockImage.color;
    }

    void Update ()
    {
        updateButtonDisplay();
    }

    void updateButtonDisplay()
    {
        SetButtonDay();
        if (buttonDay != null)
        {
            //Is there a party? If so, then display it. If not, then blank text
            if (buttonDay.party1.faction != null)
            {
                myDate.text = buttonDay.displayDay.ToString();
                myText.text = buttonDay.party1.SizeString() + " " + buttonDay.party1.faction + " Party";
            } else
            {
                myDate.text = buttonDay.displayDay.ToString();
                myText.text = "";
            }
            // Is this Day in the display month? If not, then gray it out
            if (buttonDay.month == GameData.displayMonthInt)
            {
                myBlockImage.color = defaultColor;
            } else
            {
                myBlockImage.color = Color.gray;
            }
            // Is this day today? If so, then Outline it
            if (GameData.currentMonth == buttonDay.month && GameData.currentDay == buttonDay.day)
            {
                myOutline.effectColor = Color.black;
                myXImage.color = Color.clear;
            } else if (GameData.currentMonth > buttonDay.month || (GameData.currentMonth == buttonDay.month && GameData.currentDay > buttonDay.day))
            {
                myOutline.effectColor = Color.clear;
                myXImage.color = Color.white;
            }  else
            {
                myOutline.effectColor = Color.clear;
                myXImage.color = Color.clear;
            }
            //What's the state of the Party RSVP?
            if(buttonDay.party1.RSVP == 1)
            {
                myCircleImage.color = Color.white;
                mySlashImage.color = Color.clear;
            } else if (buttonDay.party1.RSVP == -1)
            {
                myCircleImage.color = Color.clear;
                mySlashImage.color = Color.white;
            } else
            {
                myCircleImage.color = Color.clear;
                mySlashImage.color = Color.clear;
            }
        } else
        {
            myDate.text = "000";
            myText.text = "Error: Null Day";
        }

    }

    public void RSVP()
    {
        //Pop Up Window
        //Can only pass an object, so make it an Array! Hax?            
        if (GameData.calendar.monthList[GameData.displayMonthInt].dayList[rowID, columnID] != null)
        {
            if(buttonDay.party1.RSVP == 1)
            {
                object[] objectStorage = new object[3];
                objectStorage[0] = buttonDay;
                objectStorage[1] = rowID;
                objectStorage[2] = columnID;
                screenFader.gameObject.SendMessage("CreateCancellationPopUp", objectStorage);
            } else
            {
                object[] objectStorage = new object[3];
                objectStorage[0] = buttonDay;
                objectStorage[1] = rowID;
                objectStorage[2] = columnID;
                screenFader.gameObject.SendMessage("CreateRSVPPopUp", objectStorage);
            } 
        }
    }

    //To Do: Make the month of April/March work properly, why does March display its own days as coming after itself?
    void SetButtonDay()
    {
        if (GameData.calendar.monthList[GameData.displayMonthInt].dayList[rowID, columnID] != null)
        {
            buttonDay = GameData.calendar.monthList[GameData.displayMonthInt].dayList[rowID, columnID];
        }
        else if (rowID < 2)
        {
            buttonDay = GameData.calendar.monthList[GameData.displayMonthInt - 1].dayList[(rowID + GameData.calendar.monthList[GameData.displayMonthInt - 1].weeks), columnID];
        }
        else
        {
            //Something is going wrong here with April, hand math it out?
            buttonDay = GameData.calendar.monthList[GameData.displayMonthInt + 1].dayList[(rowID - GameData.calendar.monthList[GameData.displayMonthInt + 1].weeks), columnID];
        }
    }
}
