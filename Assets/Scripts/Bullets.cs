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
		if (slow == true) {
			buff_duration = 3.0f;
			buff_value = 0.5f;
		}
		else if (fire == true) {
			buff_duration = 3.0f;
			buff_value = damage / 2;
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Move the bullet towards the direction of enemy
		transform.Translate (Vector3.right * Time.deltaTime * speed);
		// Destroy object if it doesn't hit anything after 2 seconds
		Destroy (gameObject, 2.0f);
	}

	void OnTriggerEnter2D(Collider2D other) {
		// Check if collides with enemy
		if (other.gameObject.tag == "Enemy" || other.GetComponent<Enemy>()) {
			// Deal damage to enemy (Not working yet)
			other.GetComponent<Enemy>().health -= damage;
			if (slow == true && other.GetComponent<Enemy>().slowByBuff == false) {
				other.GetComponent<Enemy>().slow_duration = buff_duration;
				// Check if their speed is equivilent to their original speed, if it is then slow them (needs optimisation)
				if (other.GetComponent<Enemy>().getSpeed () == other.GetComponent<Enemy>().getOriginalSpeed()) {
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
