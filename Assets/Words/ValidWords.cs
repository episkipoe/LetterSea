using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ValidWords {
	private static List<string> words;

	private static List<string> getWords() {
		if (words == null) {
			string fileName = Application.dataPath+"/file.txt";
			Debug.Log ("file: " + fileName);

			words = new List<string> ();
			words.Add ("FOOBAR");
			words.Add ("WORD");
			words.Add ("CAT");
			words.Add ("DOG");
		}
		return words;
	}
	
	public static string getWord() {
		int max = getWords ().Count;
		return words [Random.Range (0, max)];
	}

	public static bool isValidWord(string word) {
		return getWords ().Contains(word);
	}
}
