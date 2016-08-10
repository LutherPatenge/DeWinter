using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReputationTracker : MonoBehaviour {
    public Text numberText;
    public Text toolTipText;
    
    // Use this for initialization
    void Start()
    {
        UpdateReputation();
        VictoryOrDefeatCheck();
    }

    void Update()
    {
        UpdateReputation();
    }

    public void UpdateReputation()
    {
        numberText.text = GameData.reputationCount.ToString("#,##0");                     
    }

    void VictoryOrDefeatCheck()
    {
        //If your Reputation drops to 0 or below then you lose (for now)
        if (GameData.reputationCount <= 0)
        {
            LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            man.LoadLevel("Lose Screen");
        }
        //If your Reputation reaches 2,000 or higher then you win (for now)
        if (GameData.reputationCount >= 2000)
        {
            LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            man.LoadLevel("Win Screen");
        }
    }
}
