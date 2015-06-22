using UnityEngine;
using System.Collections;

public class Defense : MonoBehaviour {
	//declaration
	public enum defenseType {
		DEF_CANNON 	= 0,	// 1 target, 1 shot, ground only
		DEF_TURRET 	= 1,	// 1 target, 3 shots, ground only
		DEF_SLOW 	= 2,	// 1 target, 1 shot, ground and air
		DEF_ANTIAIR = 3		// 1 target, 3 shots, air only
	}

	public defenseType selection;
	public int damage;
	private int cost;
	private int level;
	private float firerate;
	private float fireratecounter;
	private float weaponRotation;
	private float aimposition;
	
	public Transform target;
	public Bullets bullet;

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
				ShootBullet ();
			}
		}
	}
	
	public int GetDamage() {
		return damage;
	}
	
	void OnTriggerEnter2D(Collider2D co)//range of the tower's target
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
		Instantiate (bullet, weapon.transform.position, weapon.transform.rotation);
		bullet.damage = damage;
		this.fireratecounter = this.firerate;
	}

	void CalculateAim(Transform target) {
		// Calculate the direction of the target from the defense
		Vector2 aim = new Vector2 (target.position.x - transform.position.x, target.position.y - transform.position.y);
		// Do a atan2 and convert to degree
		aimposition = Mathf.Atan2 (aim.y, aim.x) * Mathf.Rad2Deg;
		// Apply rotation to the child
		weaponRotation = weapon.transform.eulerAngles.z;
		if (weaponRotation > aimposition) {
			weaponRotation -= Time.deltaTime * 100;
			if (weaponRotation == 0) {
				weaponRotation = 360;
			}
		} else if (weaponRotation < aimposition) {
			weaponRotation += Time.deltaTime * 100;
			if (weaponRotation == 360) {
				weaponRotation = 0;
			}
		}
		weapon.transform.rotation = Quaternion.Euler (0, 0, weaponRotation);
	}

	public void SetSelection (int selection) {
		this.selection = (defenseType)selection;
	}

	public void SetType(defenseType type) {
		this.weapon = Instantiate (weaponObject, transform.position, Quaternion.identity) as GameObject;
		this.weapon.transform.parent = transform;
		switch (selection) {
		case defenseType.DEF_CANNON:
			{
				this.damage = 10;
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
				this.firerate = 0.2f;
				this.gameObject.name = "Turret";
				break;
			}
			case defenseType.DEF_SLOW:
			{
				this.damage = 5;
				this.cost = 300;
				this.firerate = 3.0f;
				this.weapon.GetComponent<SpriteRenderer>().sprite = slow;
				this.GetComponent<CircleCollider2D>().radius = 7;
				this.gameObject.name = "Slow";
				break;
			}
			case defenseType.DEF_ANTIAIR:
			{
				this.gameObject.name = "Anti-Air";
				break;
			}
			this.fireratecounter = this.firerate;
			this.level = 1;
			this.gameObject.tag = "Defense";
		}
	}

	public int GetCost(defenseType type) {
		return cost;
	}

	void OnClick() {
		// Render circle
	}
}
