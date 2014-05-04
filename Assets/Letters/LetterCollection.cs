using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LetterCollection {
	private readonly Dictionary<char,int> letters = new Dictionary<char,int>();
	private readonly GUIText guiText;

	public LetterCollection(GUIText guiText) {
		this.guiText = guiText;
		resetLetterCount ();
	}

	void resetLetterCount() {
		for (char l = 'A'; l <= 'Z'; l++) {
			letters [l] = 0;
		}
		display ();
	}

	private void display() {
		string displayText = "";
		string vowels = "AEIOU";
		string consts = "BCDFGHJKLMNPQRSTVWXYZ";
		for (int i = 0 ; i < vowels.Length ; i++) {
			char l = vowels[i]; 
			if(letters[l] == 1) {
				displayText += l + " ";
			} else if(letters[l] > 1) {
				displayText += l + "x" + letters[l].ToString() + " ";
			}
		}
		displayText += "\n";
		for (int i = 0 ; i < consts.Length ; i++) {
			char l = consts[i]; 
			if(letters[l] == 1) {
				displayText += l + " ";
			} else if(letters[l] > 1) {
				displayText += l + "x" + letters[l].ToString() + " ";
			}
		}
		guiText.text = displayText;
	}

	public void pickUpLetter(char l) {
		l = char.ToUpper (l);
		if(letters.ContainsKey(l)) {
			letters[l]++;
		} else {
			letters[l]=1;
		}
		display ();
	}
	
	public void pickUpWord(string w) {
		for (int i = 0; i < w.Length; i++)  pickUpLetter(w [i]);
	}

	public void removeLetters(string w) {
		for (int i = 0; i < w.Length; i++) {
			char l = w[i];
			if(letters.ContainsKey(l) && letters[l]>0) {
				letters[l]--;
			}
		}
		display();
	}

	public Dictionary<char,int> getLetterCount() {
		return new Dictionary<char, int>(letters);
	}

	public void generateLetter() {
		Letter newLetter = (Letter) GameObject.Instantiate (Resources.Load ("Letter", typeof(Letter)), World.getRandomPoint(), Quaternion.identity);
		newLetter.setCollection (this);
		var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		newLetter.setValue(alphabet[(int)Mathf.Floor(Random.Range(0,alphabet.Length-1))]);
	}
	
	public void createLetter(Vector3 near, char l) {
		Letter newLetter = (Letter)GameObject.Instantiate (Resources.Load ("Letter", typeof(Letter)), Vector3.MoveTowards (near, World.getRandomPoint (), 5.0f), Quaternion.identity);
		newLetter.setCollection (this);
		newLetter.setValue (l);
	}

}
