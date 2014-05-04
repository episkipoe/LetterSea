﻿using UnityEngine;
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
		BoxCollider collider = gameObject.GetComponent<BoxCollider>();

		//collider.bounds = newWord.GetComponent<TextMesh> ().renderer.bounds;
		//collider.size.z	 = 5;

		collider.size = new Vector3 (renderer.bounds.size.x * 4, renderer.bounds.size.y * 3, 10.0f);
		collider.center = new Vector3(-1.0f,0.5f,0.0f);

	}

	public WordData getData() {
		return data;
	}
	
	void OnTriggerEnter (Collider other) {
		WordSmith wordSmith = other.GetComponent<WordSmith>();
		if(wordSmith != null) wordSmith.encounter(this);
	}
}
