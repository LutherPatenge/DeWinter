using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyTracker : MonoBehaviour {
    private Text myText;
    public GameObject screenFader;

    // Use this for initialization
    void Start()
    {
        myText = GetComponent<Text>();
        //Is it Pay Day?
        if ((GameData.currentDay+1) % 7 == 0) //Servants get paid every 7 days. The +1 is to throw off the fact that days are zero-indexed
        {
            //Pop Up Window
            //Can only pass an object, so make it an Array! Hax?            
            object[] stringStorage = new object[3];
            if (GameData.seamstressHired)
            {
                stringStorage[0] = "the Seamstress and the Handmaiden";
                stringStorage[1] = (GameData.seamstressWage + GameData.handmaidenWage).ToString();
                stringStorage[2] = (GameData.moneyCount - (GameData.seamstressWage+ GameData.handmaidenWage)).ToString();
            } else
            {
                stringStorage[0] = "the Handmaiden";
                stringStorage[1] = GameData.handmaidenWage.ToString();
                stringStorage[2] = (GameData.moneyCount - GameData.handmaidenWage).ToString();
            }
            
            screenFader.gameObject.SendMessage("CreatePayDayPopUp", stringStorage);
            //The Actual Transaction
            if (GameData.seamstressHired)
            {
                GameData.moneyCount -= (GameData.seamstressWage + GameData.handmaidenWage);
            } else
            {
                GameData.moneyCount -= GameData.handmaidenWage;
            }
        }
    }

    void Update()
    {
        updateMoney();
    }

    public void updateMoney()
    {
        myText.text = GameData.moneyCount.ToString("£" + "#,##0");
        VictoryOrDefeatCheck();
    }

    void VictoryOrDefeatCheck()
    {
        //If your Money drops to 0 or below then you lose (for now)
        if (GameData.moneyCount < 0)
        {
            LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            man.LoadLevel("Lose Screen");
        }
    }
}
