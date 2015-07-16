using UnityEngine;
using System.Collections;

public class Bullets : MonoBehaviour {

	private float speed = 20.0f;

	public float damage;
	public Vector3 direction;
	public bool slow = false;
	public bool fire = false;
	public float buff_duration = 0.0f;
	public float buff_value = 0.0f;
	public Defense owner;
	
	// Use this for initialization
	void Start () {
		Debug.Log (this.gameObject.name);
		if (this.gameObject.name == "Slow bullet") {
			slow = true;
			buff_duration = 3.0f;
			buff_value = 0.5f;
		}
		else if (this.gameObject.name == "Flamethrower bullet") {
			fire = true;
			buff_duration = 3.0f;
			buff_value = damage / 2;
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Move the bullet towards the direction of enemy
		transform.Translate (Vector3.right * Time.deltaTime * speed);
	}

	void OnTriggerEnter2D(Collider2D other) {
		// Check if collides with enemy
		if (other.gameObject.tag == "Enemy" || other.GetComponent<Enemy>()) {
			// Deal damage to enemy (Not working yet)
			other.GetComponent<Enemy>().health -= damage;
			if (slow == true) {
				other.GetComponent<Enemy>().slow_duration = buff_duration;
				if (other.GetComponent<Enemy>().getSpeed () == other.GetComponent<Enemy>().getOriginalSpeed()) {
					//other.GetComponent<Enemy>().setSpeed (other.GetComponent<Enemy>().getSpeed () * buff_value);
					other.GetComponent<Enemy>().setEffectValue(buff_value);
				}
			}
			else if (fire == true) {
				other.GetComponent<Enemy>().fire_duration = buff_duration;
				other.GetComponent<Enemy>().setBurnDamage(buff_value);
			}
			// Destroy bullet once hit enemy
			//Debug.Log (damage);
			Destroy (gameObject);
		} 
	}
}
