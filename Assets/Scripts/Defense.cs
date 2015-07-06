using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(ParticleSystem))]
public class Defense : MonoBehaviour {
	//declaration
	public enum defenseType {
		DEF_CANNON 	= 1,	// 1 target, 1 shot, ground only
		DEF_TURRET 	= 2,	// 1 target, 3 shots, ground only
		DEF_SLOW 	= 3,	// 1 target, 1 shot, ground and air
		DEF_ANTIAIR = 4		// 1 target, 3 shots, air only
	}

	public defenseType selection;
	public int damage;
	private int cost;
	private int level;
	private int health;
	private int bulletcount = 0;
	private float bulletcounttimer = 0.1f;
	private float firerate;
	private float fireratecounter;
	private float aimposition;
	private float weaponRotationSpeed = 20.0f;
	private Quaternion weaponRotation = Quaternion.identity;
	public ParticleSystem flare;
	
	public Transform target;
	public Bullets bullet;
	public Bullets slowbullet;
	public Rect healthbar;
	public Tile tile;

	private GameObject weapon;
	public GameObject weaponObject;
	public Sprite cannon;	
	public Sprite turret;
	public Sprite slow;
	public Sprite antiair;
	
	// Use this for initialization
	void Start () {
		SetType (selection);
	}

	// Update is called once per frame
	void Update () {
		// If target is available (not null)
		if (target) {
			CalculateAim (target);
			// If fire rate is not 0 then countdown else fire bullet
			if (fireratecounter > 0.0f) {
				fireratecounter -= Time.deltaTime;
			} else {
				if (this.gameObject.name == "Turret") {
					TurretFire ();
				} 
				else {
					ShootBullet ();
				}
			}
		}
		if (health <= 0) {
			Destroy (gameObject);
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
		if (this.gameObject.name == "Slow") {
			Instantiate (slowbullet, weapon.transform.position, weapon.transform.rotation);
			slowbullet.name = this.gameObject.name + " bullet";
			slowbullet.damage = damage;
		} else {
			Instantiate (bullet, weapon.transform.position, weapon.transform.rotation);
			bullet.name = this.gameObject.name + " bullet";
			bullet.damage = damage;
		}
		//FireFlare ();
		this.fireratecounter = this.firerate;
	}

	void TurretFire() {
		if (bulletcounttimer > 0.0f) {
			bulletcounttimer -= Time.deltaTime;
		} else {
			Instantiate (bullet, weapon.transform.position, weapon.transform.rotation);
			bullet.name = this.gameObject.name + " bullet";
			bullet.damage = damage;
			bulletcount += 1;
			bulletcounttimer = 0.15f;
			FireFlare ();
		}

		if (bulletcount >= 3) {
			bulletcount = 0;
			this.fireratecounter = this.firerate;
		}
	}

	void FireFlare() {
		ParticleSystem fireFlare = Instantiate (flare, weapon.transform.position, weapon.transform.rotation) as ParticleSystem;
		Destroy (fireFlare.gameObject, fireFlare.startLifetime);
	}

	void CalculateAim(Transform target) {
		// Calculate the direction of the target from the defense
		Vector2 aim = new Vector2 (target.position.x - transform.position.x, target.position.y - transform.position.y);
		// Do a atan2 and convert to degree
		aimposition = Mathf.Atan2 (aim.y, aim.x) * Mathf.Rad2Deg;
		// Apply rotation to the child
		Vector3 angles = weapon.transform.eulerAngles;
		angles.z = Mathf.LerpAngle (weapon.transform.eulerAngles.z, aimposition, weaponRotationSpeed * Time.deltaTime);
		weapon.transform.eulerAngles = angles;
	}

	public void SetSelection (int selection) {
		this.selection = (defenseType)selection;
	}

	public int GetSelection () {
		return (int)selection;
	}

	public void SetType(defenseType type) {
		this.weapon = Instantiate (weaponObject, transform.position, Quaternion.identity) as GameObject;
		this.weapon.transform.parent = transform;
		switch (selection) {
		case defenseType.DEF_CANNON:
			{
				this.damage = 8;
				this.cost = 300;
				this.firerate = 1.0f;
				this.weapon.GetComponent<SpriteRenderer>().sprite = cannon;
				this.GetComponent<CircleCollider2D>().radius = 3;
				this.gameObject.name = "Cannon";
				break;
			}
			case defenseType.DEF_TURRET:
			{
				this.damage = 2;
				this.cost = 300;
				this.firerate = 1.0f;
				this.weapon.GetComponent<SpriteRenderer>().sprite = turret;
				this.GetComponent<CircleCollider2D>().radius = 2;
				this.gameObject.name = "Turret";
				break;
			}
			case defenseType.DEF_SLOW:
			{
				this.damage = 5;
				this.cost = 300;
				this.firerate = 3.0f;
				this.weapon.GetComponent<SpriteRenderer>().sprite = slow;
				this.GetComponent<CircleCollider2D>().radius = 5;
				this.gameObject.name = "Slow";
				break;
			}
			case defenseType.DEF_ANTIAIR:
			{
				this.damage = 3;
				this.cost = 300;
				this.firerate = 3.0f;
				this.weapon.GetComponent<SpriteRenderer>().sprite = cannon;
				this.gameObject.name = "Anti-Air";
				break;
			}
		}
		this.fireratecounter = this.firerate;
		this.health = 10;
		this.level = 1;
		this.gameObject.tag = "Defense";

		//healthbar = new Rect (transform.position.x - health / 2, transform.position.y - 10, 10, 2);
		healthbar = new Rect (0, 0, 10, 3);
	}

	public int GetCost(defenseType type) {
		return cost;
	}
}
