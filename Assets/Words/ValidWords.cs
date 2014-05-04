using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ValidWords {
	private static readonly List<string> words = new List<string>();

	private static List<string> getWords() {
		if (words.Count == 0) {
			WordLoader.loadWords();
		}
		return words;
	}

	public static void addWord(string word) {
		words.Add(word.Trim().ToUpper());
	}
	
	public static string getWord() {
		int max = getWords ().Count;
		return words [Random.Range (0, max)];
	}

	public static bool isValidWord(string word) {
		return getWords ().Contains(word);
	}
}
