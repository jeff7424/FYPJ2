using UnityEngine;
using System.Collections;

public class DefenseMuzzleFlash : MonoBehaviour {

	float displayTime = 0.1f;
	float fadeTime;
	Color visible;
	Color invisible;

	// Use this for initialization
	void Awake () {
		visible = GetComponent<SpriteRenderer>().color;
		invisible = new Color(visible.r, visible.g, visible.b, 0.0f);
		GetComponent<SpriteRenderer>().color = invisible;
		fadeTime = displayTime / 2;
	}
	
	// Update is called once per frame
	void Update () {
		FireMuzzle ();
	}

	void FireMuzzle() {
		displayTime -= Time.deltaTime;
		if (displayTime >= fadeTime)
			FadeIn();
		else
			FadeOut ();
		if (displayTime <= 0.0f) {
			Destroy (gameObject);
		}
	}

	void FadeIn() {
		for (float time = 0.0f; time < fadeTime; time += Time.deltaTime) {
			GetComponent<SpriteRenderer>().color = Color.Lerp (invisible, visible, time/fadeTime);
		}
	}

	void FadeOut() {
		for (float time = 0.0f; time < fadeTime; time += Time.deltaTime) {
			GetComponent<SpriteRenderer>().color = Color.Lerp (visible, invisible, time/fadeTime);

		}
	}
}
