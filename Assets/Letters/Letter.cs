using UnityEngine;
using System.Collections;

/**
 *   An independent, collectable letter
*/
public class Letter : MonoBehaviour {
	private LetterCollection letters;
	private char value;

	public void setCollection(LetterCollection letters) {
		this.letters = letters;
	}

	public void setValue(char value) { 
		this.value = value; 
		gameObject.GetComponent<TextMesh> ().text = value.ToString();
	}

	void OnTriggerEnter (Collider other) {
		letters.pickUpLetter(value);
		Destroy(gameObject, 0.4f);
	}
}

