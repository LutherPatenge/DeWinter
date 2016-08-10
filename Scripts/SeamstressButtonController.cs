using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SeamstressButtonController : MonoBehaviour
{
    private Text myText;

    void Start()
    {
        myText = transform.GetChild(0).GetComponent<Text>(); ;
    }

    void Update()
    {
        if (GameData.seamstressHired == false)
        {
            if(GameData.seamstressWage < GameData.moneyCount)
            {
                myText.color = Color.white;
            } else
            {
                myText.color = Color.red;
            }
            myText.text = "Hire Seamstress for " + GameData.seamstressWage;
        }
        else
        {
            myText.color = Color.white;
            myText.text = "Fire Seamstress";
        }
    }

    public void HireOrFire()
    {
        if (!GameData.seamstressHired && GameData.moneyCount >= GameData.seamstressWage) //If she is NOT hired and you CAN afford her
        {
            GameData.seamstressHired = true;
            GameData.moneyCount -= GameData.seamstressWage; //Everyone's gotta get paid!
            Debug.Log("Seamstress Hired!");
        } else if (!GameData.seamstressHired && GameData.moneyCount < GameData.seamstressWage) //If she is NOT hired and you CAN'T afford her
        {
            Debug.Log("Can't afford the Seamstress :(");
        } else // If she IS Hired and it doesn't reall matter whether or not you can afford her
        {
            GameData.seamstressHired = false;
            Debug.Log("Seamstress Fired!");
        }
    }
}