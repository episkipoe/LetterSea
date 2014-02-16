using UnityEngine;
using System.Collections.Generic;
using System;

public class WordCreator {
	private bool enabled = false;
	private Dictionary<char,int> lettersRemaining = new Dictionary<char,int>();
	private readonly LetterCollection letters;
	private readonly WordCollection words;

	private string stringToEdit = "";

	public WordCreator(LetterCollection letters, WordCollection words) {
		this.letters = letters;
		this.words = words;
	}

	private bool wordComplete() {
		return stringToEdit.Length > 2;
	}

	public void toggle() { 
		if (enabled) { 
			if(wordComplete()) {
				letters.removeLetters (stringToEdit);
				words.addWord (new WordData (stringToEdit));
			}
			enabled = false;
		} else {
			enabled = true; 
			stringToEdit = "";
			lettersRemaining = letters.getLetterCount ();
		}
	}

	private bool validCharacter(string key) {
		return (key.Length == 1 && lettersRemaining.ContainsKey (key [0]) && lettersRemaining [key [0]] > 0);
	}

	public void display() {
		if (!enabled) return;

		if (Event.current.type == EventType.KeyUp) {
			KeyCode keyCode = Event.current.keyCode;
			if (validCharacter (keyCode.ToString ())) {
				char l = keyCode.ToString () [0];
				stringToEdit += l;
				lettersRemaining [l]--;
			} else if (keyCode == KeyCode.Backspace) {
				int lastPosition = stringToEdit.Length-1;
				lettersRemaining[stringToEdit[lastPosition]]++;
				stringToEdit = stringToEdit.Substring(0,lastPosition);
			} 
		}

		GUI.Window (0, new Rect (20, 40, 400, 400), DrawWindow, "Create Word");
	}

	void DrawWindow(int id) {
		string remaining = "";
		foreach (char k in lettersRemaining.Keys) {
			if(lettersRemaining[k]>0) {
				remaining+=k;
			}
		}
		GUI.Label (new Rect (20, 10, 300, 40), remaining);
		GUI.Label (new Rect (20, 30, 300, 40), stringToEdit);
	}
}
