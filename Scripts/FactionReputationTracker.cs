﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FactionReputationTracker : MonoBehaviour {
    public string factionTracked;
    Text myText;

	void Start () {
        myText = this.GetComponent<Text>();
	}

	void Update () {
	    myText.text = "- Your Reputation with The " + factionTracked + " is " + GameData.factionList[factionTracked].playerReputation.ToString();
	}
}
