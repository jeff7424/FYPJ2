using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashScreenScript : MonoBehaviour {

	Image screenFade;

	Color transparent;
	Color black;

	float waitTime;
	float fadeSpeed;

	bool start;

	// Use this for initialization
	void Start () {
		screenFade = GameObject.Find ("Screenfade").GetComponent<Image>();
		transparent = Color.clear;
		black = Color.black;
		waitTime = 1.0f;
		fadeSpeed = 1.5f;
		start = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (start) {
			FadeToClear ();
			if (screenFade.color.a <= 0.05f) {
				screenFade.color = Color.clear;
				screenFade.enabled = false;
				waitTime -= Time.deltaTime;
				if (waitTime <= 0.0f)
					start = false;
			}
		} else if (!start) {
			screenFade.enabled = true;
			FadeToBlack();
			if (screenFade.color.a >= 0.95f) {
				Application.LoadLevel("Main Menu");
			}
		}
	}

	void FadeToClear() {
		screenFade.color = Color.Lerp (screenFade.color, transparent, fadeSpeed * Time.deltaTime);
	}

	void FadeToBlack() {
		screenFade.color = Color.Lerp (screenFade.color, black, fadeSpeed * Time.deltaTime);
	}	
}
