using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nexus : BaseObject {

    protected void Awake () {
	    switch (team)
        {
            case Teams.red:
                GlobalRefs.redNexus = this;
                break;
            case Teams.blue:
                GlobalRefs.blueNexus = this;
                print("added myself: "+GlobalRefs.blueNexus.gameObject.name);
                break;
            default:
                Debug.LogError("Nexus "+gameObject.name+" Team not assigned.");
                break;
        }
	}

    public override void ApplyDamage(int damage = 0)
    {
        CurrentHP -= damage;

        if (CurrentHP <= 0 && GlobalRefs.gameState != GameState.gameover)
        {
            GlobalRefs.gameState = GameState.gameover;
            GameOver();
        }
    }

    private void GameOver()
    {
        string winningTeam;
        switch (team)
        {
            case Teams.red:
                winningTeam = "Red";
                break;
            case Teams.blue:
                winningTeam = "Red";
                break;
            default:
                winningTeam = "?";
                break;
        }
        foreach (Text text in GlobalRefs.PlayerViewText)
        {
            text.text = "Team " + winningTeam + " won!";
        }
    }
}
