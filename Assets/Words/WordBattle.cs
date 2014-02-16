using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WordBattle {
	private bool enabled = false;
	private int counter;
	private const int framesPerTurn = 300;

	private readonly LetterCollection myLetters;
	private readonly WordCollection myWords;
	private readonly WordCollection opponent;
	private readonly BattleScore score;

	private Vector3 location;

	private int contentWidth;
	private int contentHeight;

	public WordBattle(LetterCollection myLetters, WordCollection myWords, BattleScore score) {
		this.myLetters = myLetters;
		this.myWords = myWords;
		this.score = score;
		opponent = new WordCollection ();
	}

	public void startBattle(WordData word, Vector3 location) {
		opponent.addWord(word);
		this.location = location;
		if (!enabled) {
			enabled = true;
			counter = framesPerTurn;
		}
	}

	public void endBattle(bool won) {
		enabled = false;
		score.addBattle (won);
	}

	private void processTurn() {
		counter = framesPerTurn;

		WordData lhs = myWords.popRandomWord();
		WordData rhs = opponent.popRandomWord();

		myWords.addWords(lhs.removeLetter(myLetters, location));
		opponent.addWords(rhs.removeLetter (myLetters, location));

		if (opponent.isEmpty()) {
			endBattle(true);
		} else if (myWords.isEmpty()) {
			endBattle(false);
		}
	}

	/**
	 * @return true if enabled and displaying something
	*/
	public bool display() {
		if (!enabled) return false;

		counter--;
		if (counter <= 0) {
			processTurn();
		}

		contentWidth = (int)(Screen.width * 0.3);
		contentHeight = Screen.height;

		Rect windowRect = new Rect ((Screen.width / 2) - (contentWidth / 2), (Screen.height / 2) - (contentHeight / 2), contentWidth, contentHeight);
		GUI.Window (0, windowRect, DrawWindow, "Word Battle");
		return true;
	}

	void DrawWindow(int id) {
		int width = (int)(Screen.width * 0.1);
		int height = (int)(Screen.height * 0.75);

		GUIStyle leftStyle =  new GUIStyle(GUI.skin.label);
		leftStyle.alignment = TextAnchor.UpperLeft;
		GUI.Label (new Rect (10, 20, width, height), myWords.getWords("\n"), leftStyle);

		GUIStyle rightStyle = new GUIStyle(GUI.skin.label);
		rightStyle.alignment = TextAnchor.UpperRight;
		int x = (int)(contentWidth * 0.6);
		GUI.Label (new Rect (x, 20, width, height), opponent.getWords ("\n"), rightStyle);
	}
}
