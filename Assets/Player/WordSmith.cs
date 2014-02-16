using UnityEngine;

public class WordSmith : MonoBehaviour {
	public GUIText letterDisplay;
	public GUIText wordDisplay;
	public GUIText scoreDisplay;
	
	private WordCollection myWords;
	private LetterCollection letters;
	private WordCreator wordCreator;
	private WordBattle wordBattle;
	private BattleScore battleScore;

	void Start() {
		myWords = new WordCollection(wordDisplay);
		myWords.addWord(new WordData("CAT"));

		letters = new LetterCollection (letterDisplay);

		wordCreator = new WordCreator (letters, myWords);
		battleScore = new BattleScore (scoreDisplay);
		wordBattle = new WordBattle (letters, myWords, battleScore);

		for (int i = 0; i < 100; i++) {
			generateWord();
			letters.generateLetter();
		}
	}

	private void generateWord() {
		GameObject.Instantiate (Resources.Load ("Word", typeof(Word)), World.getRandomPoint(), Quaternion.identity);
	}
	
	public void encounter(Word word) {
		if (myWords.isEmpty ()) {
			return;
		}
		//move the word to the battle; if it wins it will be recreated
		wordBattle.startBattle(word.getData(), gameObject.transform.position);
		Destroy (word.transform.gameObject);
	}

	void Update() { }

	void OnGUI() {
		if (wordBattle.display ()) {
			return;
		}
		if (Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Return) {
			wordCreator.toggle();
		} else {
			wordCreator.display();
		}
	}



}
