using UnityEngine;
using System.Collections;

public class Word : MonoBehaviour {
	private WordData data;

	private Vector3 destination;

	private const float SPEED = 0.1f;
	
	void Start () { 
		setValue (ValidWords.getWord ());
		moveTowardsNewPoint();
	}
	
	void Update () { 
		gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, destination, SPEED);
		if(gameObject.transform.position.Equals(destination)) {
			moveTowardsNewPoint();
		}
	}

	void moveTowardsNewPoint() {
		destination = World.getRandomPoint();
		gameObject.transform.localEulerAngles = Quaternion.LookRotation(destination).eulerAngles;
	}

	public void setValue(string value) {
		this.data = new WordData(value);
		gameObject.GetComponent<TextMesh> ().text = value;
	}

	public WordData getData() {
		return data;
	}
	
	void OnTriggerEnter (Collider other) {
		WordSmith wordSmith = other.GetComponent<WordSmith>();
		if(wordSmith != null) wordSmith.encounter(this);
	}
}
