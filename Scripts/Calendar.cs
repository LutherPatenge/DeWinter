using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Calendar {

    public List<Month> monthList = new List<Month>();

    public Calendar()
    {
        stockWithMonths();
        Debug.Log("Created Calendar!");
    }

    //To Do: Make the Starting Day Pos thing less janky
    void stockWithMonths()
    {
        monthList.Add(new Month("Janvier", 31, 0, 0, 4));
        monthList.Add(new Month("Fevrier", 28, 1, startingDayPos(31, monthList[0].startingDayPosition), 4));
        monthList.Add(new Month("Mars", 31, 2, startingDayPos(28, monthList[1].startingDayPosition), 4));
        monthList.Add(new Month("Avril", 30, 3, startingDayPos(31, monthList[2].startingDayPosition), 6));
        monthList.Add(new Month("Mai", 31, 4, startingDayPos(30, monthList[3].startingDayPosition), 5));
        monthList.Add(new Month("Juin", 30, 5, startingDayPos(31, monthList[4].startingDayPosition), 4));
        monthList.Add(new Month("Juillet", 31, 6, startingDayPos(30, monthList[5].startingDayPosition), 4));
        monthList.Add(new Month("Aout", 31, 7, startingDayPos(31, monthList[6].startingDayPosition), 4));
        monthList.Add(new Month("Septembre", 30, 8, startingDayPos(31, monthList[7].startingDayPosition), 4));
        monthList.Add(new Month("Octobre", 31, 9, startingDayPos(30, monthList[8].startingDayPosition), 4));
        monthList.Add(new Month("Novembre", 31, 10, startingDayPos(31, monthList[9].startingDayPosition), 4));
        monthList.Add(new Month("Decembre", 31, 11, startingDayPos(31, monthList[10].startingDayPosition), 4));
    }

    //To Do: Make this accept negative numbers to check the past (do I need that?)
    //To Do: Make this able to check multiple months in the future (do I need THAT?)
    public Day daysFromNow(int dayNumber)
    {
        if(GameData.currentDay + dayNumber <= monthList[GameData.currentMonth].days) //If the upcoming day is in the current Month
        {
            return monthList[GameData.currentMonth].SelectDayByInt(GameData.currentDay + dayNumber);
        } else //If the upcoming day is in a future Month
        {
            int day = (monthList[GameData.currentMonth].days - GameData.currentDay) + dayNumber;
            return monthList[GameData.currentMonth + 1].SelectDayByInt(day);
        }
    }

    public Day today()
    {
        return monthList[GameData.currentMonth].SelectDayByInt(GameData.currentDay);
    }

    //To Do: How to get the Previous Months starting position?
    int startingDayPos(int prevMonthLength, int prevMonthStartPos)
    {
        return (prevMonthLength + prevMonthStartPos) % 7;
    }
}
