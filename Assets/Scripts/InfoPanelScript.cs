using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoPanelScript : MonoBehaviour {

	public Defense defense;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Find ("Tower name").GetComponent<Text>().text = defense.name;
		transform.Find ("Damage").GetComponent<Text>().text = "Damage: " + defense.damage;
		transform.Find ("Firing rate").GetComponent<Text>().text = "Fire rate: " + defense.GetFireRate();
		transform.Find ("Range").GetComponent<Text>().text = "Range: " + defense.GetRange();
	}
}
