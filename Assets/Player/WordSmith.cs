using UnityEngine;

public class WordSmith : MonoBehaviour {
	//Letters
	public GUIText letterDisplay;
	private LetterCollection letters;

	//Words
	public GUIText wordDisplay;
	private WordCollection myWords;
	private WordCreator wordCreator;

	//Battle
	public GUIText scoreDisplay;
	private BattleScore battleScore;
	private WordBattle wordBattle;

	void Start() {
		letters = new LetterCollection (letterDisplay);

		myWords = new WordCollection(wordDisplay);
		myWords.addWord(new WordData("CAT"));
		wordCreator = new WordCreator (letters, myWords);

		battleScore = new BattleScore (scoreDisplay);
		wordBattle = new WordBattle (letters, myWords, battleScore);

		for (int i = 0; i < 100; i++) {
			WordCollection.generateWord ();
		}
		for (int i = 0; i < 400; i++) {
			letters.generateLetter();
		}
	}

	public void encounter(Word word) {
		if (myWords.isEmpty ()) {
			return;
		}

		//move the word to the battle
		wordBattle.startBattle(gameObject.transform.position, word.getData());
		Destroy (word.transform.gameObject);
	}
	
	void OnGUI() {
		if (wordBattle.display ()) {
			return;
		}
		if (Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Return) {
			wordCreator.toggle();
		} else {
			wordCreator.display();
		}
		if (Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Space) {
			wordBattle.startBattle (gameObject.transform.position);
		}

	}
	
}
