using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(ParticleSystem))]
public class Defense : MonoBehaviour {
	//declaration
	public enum defenseType {
		DEF_CANNON 	= 1,	// 1 target 1 shot, normal
		DEF_TURRET 	= 2,	// 1 target 3 shots, fast
		DEF_SLOW 	= 3,	// slow target 
		DEF_ANTIAIR = 4,	// air target only
		DEF_FLAME	= 5		// burn target overtime 
	}

	public defenseType selection;
	public float damage;
	private int cost;
	private int rank;
	private int health;
	private int bulletcount = 0;
	private float bulletcounttimer = 0.1f;
	private float firerate;
	private float fireratecounter;
	private float originalfirerate;
	private float aimposition;
	private float weaponRotationSpeed = 20.0f;
	private GameObject game;
	private GameObject player;
	public GameObject ranking;
	private GameObject rankImage;
	public GameObject muzzleFlare;

	public float rage_duration;
	public float rage_value;
	
	public Transform target;
	public Bullets bullet;
	public Bullets slowbullet;
	public Bullets firebullet;
	public Rect healthbar;
	public Tile tile;

	private GameObject weapon;
	private GameObject pivot;	// Pivot for the weapon rotation
	public GameObject weaponObject;

	public Sprite[] barrel;
	public Sprite[] body;
	public Sprite[] projectile;

	public AudioClip shoot;
	
	// Use this for initialization
	void Start () {
		game = GameObject.Find ("Game");
		if (Application.loadedLevelName == "Game")
			player = GameObject.Find ("Player");
		else if (Application.loadedLevelName == "Multiplayer") {
			if (gameObject.tag == "Player 1")
				player = GameObject.Find ("Player 1");
			else if (gameObject.tag == "Player 2")
				player = GameObject.Find ("Player 2");
		}
		
		selection = (defenseType)player.GetComponent<Player1> ().selection;
		SetType (selection);
		GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat ("volume");
	}

	// Update is called once per frame
	void Update () {
		// If target is available (not null)
		if (!game.GetComponent<Game>().GetPause () && !game.GetComponent<Game>().endGame) {
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
			if (rage_duration > 0.0f) {
				RageCountdown();
				GetComponent<SpriteRenderer>().color = Color.red;
			} else {
				GetComponent<SpriteRenderer>().color = Color.white;
			}
		}
	}
	
	public float GetDamage() {
		return damage;
	}

	void OnTriggerEnter2D(Collider2D co)
	{
		// Check if defense has no target then assign new one
		if (target == null) {
			if (this.gameObject.name == "Anti-Air") {
				if ((co.gameObject.tag == "Enemy" && co.gameObject.name == "Fly")) {
					target = co.gameObject.transform;
				} 
			} else {
				if (co.gameObject.tag == "Enemy" || co.GetComponent<Enemy>()) {
					target = co.gameObject.transform;
				} 
			}
		}
	}

	void OnTriggerStay2D(Collider2D co) {
		if (target == null) {
			if (this.gameObject.name == "Anti-Air") {
				if ((co.gameObject.tag == "Enemy" && co.gameObject.name == "Fly")) {
					target = co.gameObject.transform;
				} 
			} else {
				if (co.gameObject.tag == "Enemy" || co.GetComponent<Enemy>()) {
					target = co.gameObject.transform;
				} 
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
			slowbullet.GetComponent<SpriteRenderer>().sprite = projectile[(int)selection - 1];
		} else if (this.gameObject.name == "Flame") {
			Instantiate (firebullet, weapon.transform.position, weapon.transform.rotation);
			firebullet.name = this.gameObject.name + " bullet";
			firebullet.damage = damage;
			firebullet.GetComponent<SpriteRenderer>().sprite = projectile[(int)selection - 1];
		} else {
			Instantiate (bullet, weapon.transform.position, weapon.transform.rotation);
			bullet.name = this.gameObject.name + " bullet";
			bullet.damage = damage;
			bullet.GetComponent<SpriteRenderer>().sprite = projectile[(int)selection - 1];
		}
		FireFlare ();
		PlayShootSound();
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
			bullet.GetComponent<SpriteRenderer>().sprite = projectile[(int)selection - 1];
			FireFlare ();
			PlayShootSound();
		}

		if (bulletcount >= 3) {
			bulletcount = 0;
			this.fireratecounter = this.firerate;
		}
	}

	void FireFlare() {
		GameObject flash = Instantiate (muzzleFlare, weapon.transform.position, weapon.transform.rotation) as GameObject;
		flash.transform.parent = weapon.transform;
		flash.transform.localPosition = new Vector2 (0.5f, 0.0f);
	}

	void PlayShootSound() {
		GetComponent<AudioSource>().clip = shoot;
		GetComponent<AudioSource>().Play ();
	}

	void CalculateAim(Transform target) {
		// Calculate the direction of the target from the defense
		Vector2 aim = new Vector2 (target.position.x - transform.position.x, target.position.y - transform.position.y);
		// Do a atan2 and convert to degree
		aimposition = Mathf.Atan2 (aim.y, aim.x) * Mathf.Rad2Deg;
		// Apply rotation to the child		
		Vector3 angles = pivot.transform.eulerAngles;
		angles.z = Mathf.LerpAngle (pivot.transform.eulerAngles.z, aimposition, weaponRotationSpeed * Time.deltaTime);
		pivot.transform.eulerAngles = angles;
	}

	public void SetSelection (int selection) {
		this.selection = (defenseType)selection;
	}

	public int GetSelection () {
		return (int)selection;
	}

	public void SetType(defenseType type) {
		GameObject temp = new GameObject();
		this.pivot = Instantiate (temp, transform.position, Quaternion.identity) as GameObject;
		this.pivot.transform.parent = this.transform;
		this.pivot.gameObject.name = "Pivot";
		Destroy (temp);
		this.weapon = Instantiate (weaponObject, transform.position, Quaternion.identity) as GameObject;
		this.weapon.transform.parent = pivot.transform;
		this.weapon.gameObject.name = "Barrel";
		this.weapon.transform.localPosition = new Vector2 (0.25f, 0.0f);
		switch (selection) {
		case defenseType.DEF_CANNON:
			{
				this.damage = 3;
				this.cost = (int)Game.upgradeCost.DEF_CANNON;
				this.firerate = 1.0f;
				this.GetComponent<CircleCollider2D>().radius = 3;
				this.gameObject.name = "Cannon";
				break;
			}
			case defenseType.DEF_TURRET:
			{
				this.damage = 1;
				this.cost = (int)Game.upgradeCost.DEF_TURRET;
				this.firerate = 1.0f;
				this.GetComponent<CircleCollider2D>().radius = 2;
				this.gameObject.name = "Turret";
				break;
			}
			case defenseType.DEF_SLOW:
			{
				this.damage = 2;
				this.cost = (int)Game.upgradeCost.DEF_SLOW;
				this.firerate = 2.0f;
				this.GetComponent<CircleCollider2D>().radius = 5;
				this.gameObject.name = "Slow";
				break;
			}
			case defenseType.DEF_ANTIAIR:
			{
				this.damage = 7;
				this.cost = (int)Game.upgradeCost.DEF_ANTIAIR;
				this.firerate = 1.0f;
				this.GetComponent<CircleCollider2D>().radius = 5;
				this.gameObject.name = "Anti-Air";
				break;
			}
			case defenseType.DEF_FLAME:
			{
				this.damage = 1;
				this.cost = (int)Game.upgradeCost.DEF_FLAME;
				this.firerate = 0.2f;
				this.GetComponent<CircleCollider2D>().radius = 3;
				this.gameObject.name = "Flame";
				break;
			}
		}
		this.weapon.GetComponent<SpriteRenderer>().sprite = barrel[(int)selection - 1];
		this.GetComponent<SpriteRenderer>().sprite = body[(int)selection - 1];
		this.fireratecounter = this.firerate;
		this.originalfirerate = this.firerate;
		this.health = 10;
		this.rank = 1;
		this.rankImage = Instantiate (ranking, transform.position, Quaternion.identity) as GameObject;
		this.rankImage.transform.parent = transform;
		this.rankImage.transform.localPosition = new Vector2 (-0.3f, -0.3f);
		this.gameObject.tag = "Defense";
	}

	public int ReturnType() {
		return (int)selection;
	}

	public void SetCost(int newCost) {
		cost = newCost;
	}

	public int GetCost() {
		return cost;
	}

	public void SetRank(int newRank) {
		rank = newRank;
	}

	public int GetRank() {
		return rank;
	}

	public float GetFireRate() {
		return firerate;
	}

	public float GetRange() {
		return GetComponent<CircleCollider2D>().radius;
	}

	public int GetLevel() {
		return rank;
	}

	public void RankUp() {
		if (rank < 3) {
			rank += 1;
			rankImage.GetComponent<RankingScript>().UpdateSprite(rank);
			switch (selection) {
				case defenseType.DEF_CANNON:
				{
					damage *= 1.5f;
					break;
				}
				case defenseType.DEF_TURRET:
				{
					firerate -= 0.1f;
					break;
				}
				case defenseType.DEF_SLOW:
				{
					firerate -= 0.2f;
					break;
				}
				case defenseType.DEF_ANTIAIR:
				{
					damage *= 1.2f;
					break;
				}
				case defenseType.DEF_FLAME:
				{
					damage *= 1.1f;
					break;
				}
			}
			this.GetComponent<CircleCollider2D>().radius += 0.5f;
		}
	}

	public void Rage(float duration, float value) {
		rage_duration = duration;
		firerate = firerate / value;
	}

	void RageCountdown() {
		rage_duration -= Time.deltaTime;
		if (rage_duration <= 0.0f) {
			rage_duration = 0.0f;
			firerate = originalfirerate;
		}
	}
}
