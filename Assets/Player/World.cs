using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {
	public static int MIN_X = -600;
	public static int MAX_X = 600;
	public static int MIN_Z = -600;
	public static int MAX_Z = 600;

	public static Vector3 getRandomPoint() {
		float x = Random.Range (MIN_X, MAX_X);
		float z = Random.Range (MIN_Z, MAX_Z);
		return new Vector3 (x, 1.2f, z);
	}
}
