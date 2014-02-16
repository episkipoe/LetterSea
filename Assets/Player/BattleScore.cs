using UnityEngine;
using System.Collections;

public class BattleScore {
	private readonly GUIText guiText;
	private int nBattles, nVictories;

	public BattleScore(GUIText guiText) {
		this.guiText = guiText;
		guiText.text = "No encounters";
		nBattles = nVictories = 0;
	}

	public void addBattle(bool won) {
		nBattles++;
		if (won) nVictories++;
		guiText.text = nBattles + " battles, " + nVictories + " won";
	}
}
