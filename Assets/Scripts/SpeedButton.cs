using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SpeedButton : MonoBehaviour {

	public Text Speed;
	int speed;

	// Use this for initialization
	void Start () {
		speed = 1;
		Speed.text = "X" + 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (speed == 1) {
			Time.timeScale = 1;
			Speed.text = "X" + 1;
		}
		else if (speed == 2) {
			Time.timeScale = 2;
			Speed.text = "X" + 2;
		}
		else if (speed == 3) {
			Time.timeScale = 3;
			Speed.text = "X" + 3;
		}
	}

	public void Onclick()
	{
		speed++;
		if (speed > 3) {
			speed = 1;
		}
	}
}
