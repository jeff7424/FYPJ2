using UnityEngine;
using System.Collections;

public class Defense : MonoBehaviour {
	//declaration
	private int damage;
	private int cost;
	private float firerate;
	private int level;
	private Vector2 direction;
	public GameObject enemy;
	public GameObject bulletPrefab;
	public Bullets bullet;
	public bool inRange;

	// Use this for initialization
	void Start () {
		damage = 10;
		level = 1;
		cost = 300;
		firerate = 3;
		inRange = false;
		direction = new Vector2 (0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		direction = enemy.transform.position - transform.position;
		if (firerate > 0) {
			firerate -= Time.deltaTime;
		} 
		else {
			Instantiate (bulletPrefab, transform.position, Quaternion.identity);//create new object
			firerate = 3;
			//GameObject g = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.identity);
		}
	}
	
	public Vector2 GetDirection() {
		return direction;
	}

	public int GetDamage() {
		return damage;
	}

	void OnTriggerEnter(Collider co)
	{
		if (co.GetComponent<Enemy> ()) {
			inRange = true;
			//g.GetComponent<Bullets>().target = co.transform;
		} 
		else {
			inRange = false;
		}
	}
}
