using UnityEngine;
using System.Collections;

public class Day {

    public int month; // Which number month is this?
    public int day; // Which day in the month is this? (zero indexed)
    public int displayDay; //Which day is this? Publicly shown
    public int linearDayValue; //Is this the 99th day? The 100th?
    public Party party1;
    public Party party2;

       // This is the constructor that's actually used for usable days
    public Day(int dDay, int dMonth) 
    {
        day = dDay;
        displayDay = day + 1;
        month = dMonth;
        party1 = new Party();
    }

}
