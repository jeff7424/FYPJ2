using UnityEngine;
using System.Collections;

public class RankingScript : MonoBehaviour {

	public Sprite[] sprites;

	public void UpdateSprite(int rank) {
		GetComponent<SpriteRenderer> ().sprite = sprites [rank - 1];
	}
}
