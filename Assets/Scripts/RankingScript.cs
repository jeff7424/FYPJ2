using UnityEngine;
using System.Collections;

public class RankingScript : MonoBehaviour {

	public Sprite[] sprites;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateSprite(int rank) {
		GetComponent<SpriteRenderer> ().sprite = sprites [rank - 1];
	}
}
