﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartPartyButtonTextController : MonoBehaviour {

    private Text myText;
    private Outline myOutline; // This is for highlighting buttons

    // Use this for initialization
    void Start () {
        myText = this.GetComponentInChildren<Text>();
        myOutline = this.GetComponent<Outline>();
    }
	
	// Update is called once per frame
	void Update () {
        if (GameData.partyOutfitID == -1)
        {
            myOutline.effectColor = Color.clear;
            myText.text = "Select Your Outfit";
        }
        else
        {
            myOutline.effectColor = Color.yellow;
            myText.text = "Start the Party!";
        }
    }
}
