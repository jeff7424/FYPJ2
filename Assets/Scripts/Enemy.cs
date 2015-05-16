using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private int health;
	private float speed;

	// Use this for initialization
	void Start () {
		health = 100;
		speed = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
	}

	public float getSpeed(){
		return speed;
	}
}
