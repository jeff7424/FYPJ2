using UnityEngine;
using System.Collections;

public class Defense : MonoBehaviour {
	//declaration
	private int damage;
	private int cost;
	private float firerate;
	private int level;
	private float weaponRotation;

	public Vector2 direction;
	public Transform target;
	public GameObject bullet;

	private GameObject weapon;
	public GameObject cannon;
	
	// Use this for initialization
	void Start () {
		damage = 10;
		level = 1;
		cost = 300;
		firerate = 1;
		direction = new Vector2 (0, 0);
		weapon = Instantiate (cannon, transform.position, Quaternion.identity) as GameObject;
		weapon.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {
		//direction = target.position - transform.position;
		// If target is available (not null)
		if (target) {
			CalculateAim (target);
			//weapon.rotation = Quaternion.Lerp (weapon.rotation, weaponRotation, Time.deltaTime);
			// If fire rate is not 0 then countdown else fire bullet
			if (firerate > 0) {
				firerate -= Time.deltaTime;
			} else {
				ShootBullet ();
			}
		}
	}
	
	public int GetDamage() {
		return damage;
	}
	
	void OnTriggerEnter2D(Collider2D co)
	{
		// Check if defense has no target then assign new one
		if (target == null) {
			if (co.gameObject.tag == "Enemy" || co.GetComponent<Enemy>()) {
				target = co.gameObject.transform;
			} 
		}
	}

	void OnTriggerStay2D(Collider2D co) {
		if (target == null) {
			if (co.gameObject.tag == "Enemy" || co.GetComponent<Enemy>()) {
				target = co.gameObject.transform;
			} 
		}
	}

	void OnTriggerExit2D(Collider2D co) {
		// If target exits the defense range then set target to null
		if (target) {
			if (co.gameObject.tag == "Enemy" || co.GetComponent<Enemy>()) {
				target = null;
			}
		}
	}
	
	void ShootBullet() {
		// Instantiation of bullet object
		GameObject b = (GameObject)Instantiate (bullet, transform.position, Quaternion.identity);
		b.GetComponent<Bullets>().target = target;
		b.GetComponent<Bullets> ().damage = damage;
		firerate = 1;
	}

	void CalculateAim(Transform target) {
		// Calculate the direction of the target from the defense
		Vector2 aim = new Vector2 (target.position.x - transform.position.x, target.position.y - transform.position.y);
		// Do a atan2 and convert to degree
		weaponRotation = Mathf.Atan2 (aim.y, aim.x) * Mathf.Rad2Deg;
		// Apply rotation to the child
		weapon.transform.rotation = Quaternion.Euler (0, 0, weaponRotation);
	}
}
