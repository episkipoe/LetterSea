using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 *  A collection of WordData 
*/
public class WordCollection {
	private readonly GUIText guiText;

	/**
	 * The words in the collection - will be sorted by strength, descending
	*/
	private List<WordData> words = new List<WordData>();

	public WordCollection() { }
	public WordCollection(GUIText guiText) {
		this.guiText = guiText;
		display ();
	}

	public void addWord(WordData word) { 
		words.Add (word);
		words.Sort(SortByStrength);
		display ();
	}

	public void addWords (List<WordData> list) {
		words.AddRange(list);
		words.Sort(SortByStrength);
		display ();
	}

	private int SortByStrength(WordData a, WordData b) {
		return b.getStrength ().CompareTo (a.getStrength ());
	}

	/**
	 * @return the most powerful word or null if there are no words
	*/
	public WordData getMostPowerfulWord() {
		if(words.Count > 0) return words[0];
		return null;
	}

	public WordData popRandomWord() {
		if (words.Count > 0) {
			int index = Random.Range(0, words.Count-1);
			WordData w = words[index];
			words.RemoveAt(index);
			return w;
		}
		return null;
	}

	public string getWords(string joinWith) {
		List<string> list = new List<string> ();
		foreach (WordData w in words) {
			list.Add (w.ToString ());
		}
		return string.Join (joinWith, list.ToArray ());
	}
	
	private void display() {
		if (guiText != null) {
			guiText.text = getWords (", ");
		}
	}

	public void removeWord (WordData w) {
		words.Remove (w);
		display ();
	}
	
	public bool isEmpty() {
		return words.Count == 0;
	}

	public static void generateWord() {
		Word newWord = (Word) GameObject.Instantiate (Resources.Load ("Word", typeof(Word)), World.getRandomPoint (), Quaternion.identity);
	}
}
