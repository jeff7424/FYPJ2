using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerInfo : MonoBehaviour {

	public Text defense;
	public Text damage;
	public Text firingrate;
	public Text range;
	public Text cost;

	int selection;
	bool activated;

	// Use this for initialization
	void Start () {
		isActivated (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (activated) {
#if UNITY_EDITOR
			Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position = new Vector2(worldPoint.x, worldPoint.y);
#elif UNITY_ANDROID
			foreach (Touch touch in Input.touches) {
				Vector3 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
				transform.position = new Vector2(worldPoint.x, worldPoint.y);
			}
#endif
			switch (selection) {
			case 1:
			{
				defense.text = "Cannon";
				damage.text = 3.ToString();
				firingrate.text = 1.0f.ToString();
				range.text = 3.ToString();
				cost.text = 15.ToString();
				break;
			}
			case 2:
			{
				defense.text = "Turret";
				damage.text = 1.ToString() + " * 3";
				firingrate.text = 1.0f.ToString();
				range.text = 2.ToString();
				cost.text = 20.ToString();
				break;
			}
			case 3:
			{
				defense.text = "Slow";
				damage.text = 2.ToString();
				firingrate.text = 2.0f.ToString();
				range.text = 5.ToString();
				cost.text = 30.ToString();
				break;
			}
			case 4:
			{
				defense.text = "Anti-air";
				damage.text = 7.ToString();
				firingrate.text = 1.0f.ToString();
				range.text = 5.ToString();
				cost.text = 50.ToString();
				break;
			}
			case 5:
			{
				defense.text = "Flame";
				damage.text = 1.ToString();
				firingrate.text = 0.2f.ToString();
				range.text = 3.ToString();
				cost.text = 30.ToString();
				break;
			}
			}
		}
	}

	public void SetSelection(int sel) {
		selection = sel;
	}

	public void isActivated(bool act) {
		activated = act;
		if (activated) {
			GetComponent<Image>().enabled = true;
			for (int i = 0; i < transform.childCount; i++) {
				transform.GetChild(i).gameObject.SetActive(true);
			}
		} else {
			GetComponent<Image>().enabled = false;
			for (int i = 0; i < transform.childCount; i++) {
				transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}
}
