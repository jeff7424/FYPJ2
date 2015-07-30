using UnityEngine;
using System.Collections;

public class MainMenuRobots : MonoBehaviour {

	int dir;
	int speed;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(Random.Range (-1, 1) * 7, transform.position.y, transform.position.z);
		speed = Random.Range (2, 5);
		dir = Random.Range (0, 10);
		if (dir < 5)
			dir = -1;
		else
			dir = 1;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate ( new Vector3( speed * dir * Time.deltaTime, 0, 0));
		if (transform.position.y < -5) {
			int temp = Random.Range (0, 10);
			if (temp < 5)
				temp = -1;
			else
				temp = 1;
			transform.position = new Vector3(temp * 10, 7, transform.position.z);
			if (transform.position.x < 0) {
				dir = 1;
			} else if (transform.position.x > 0) {
				dir = -1;
			} else if (transform.position.x == 0) {
				dir = Random.Range (0, 1);
				if (dir == 0)
					dir = -1;
			}
		}
		transform.localScale = new Vector3(dir, 1, 1);
	}
}
