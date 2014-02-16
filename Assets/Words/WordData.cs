using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WordData {
	public readonly string value;
	public readonly int popularity = 1;

	public WordData(string value) {
		this.value = value;
		this.popularity = 1;
	}

	override public string ToString() { 
		return value; 
	}

	public float getStrength() { 
		return value.Length * popularity;
	}

	public bool defeats(WordData other) {
		float difference = getStrength () - other.getStrength ();
		if (difference > 0.9) {
			return true;
		} else if (difference < -0.9) {
			return false;
		}
		return Random.Range(0,100) % 2 == 0;
	}

	public List<WordData> removeLetter(LetterCollection letters, Vector3 near) {
		int indexToRemove = Random.Range (0, value.Length);
		List<WordData> subWords = new List<WordData> ();

		//create a word or letter from the first part
		if (indexToRemove > 1) {
			subWords.Add (new WordData (value.Substring (0, indexToRemove)));
		} else if (indexToRemove == 1) {
			letters.createLetter(near, value[0]);
		}

		//create a word or letter from the last part
		int lastLetter = value.Length - 1;
		int lettersAtEnd = lastLetter - indexToRemove;
		if (lettersAtEnd > 1) {
			subWords.Add (new WordData(value.Substring(indexToRemove+1)));
		} else if (lettersAtEnd == 1) {
			letters.createLetter(near, value[lastLetter]);
		}

		return subWords;
	}

}
