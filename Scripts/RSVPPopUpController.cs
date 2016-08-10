using UnityEngine;
using System.Collections;

public class RSVPPopUpController : MonoBehaviour {

    public void RSVPAction(int decision)
    {
        //0 means no RSVP yet, 1 means Attending and -1 means Decline
        GameData.calendar.monthList[GameData.displayMonthInt].dayList[GameData.partyRowID, GameData.partyColumnID].party1.RSVP = decision;
    }

    public void CancellationAction()
    {
        string faction = GameData.calendar.monthList[GameData.displayMonthInt].dayList[GameData.partyRowID, GameData.partyColumnID].party1.faction;
        GameData.calendar.monthList[GameData.displayMonthInt].dayList[GameData.partyRowID, GameData.partyColumnID].party1.RSVP = -1;
        GameData.factionList[faction].playerReputation -= 20;
        GameData.reputationCount -= 10;
    }
}
