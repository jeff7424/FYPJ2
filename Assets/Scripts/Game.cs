using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

	//public GUITexture PauseButton;
	public GameObject Button_Pause;
	public GameObject Button_DefenseCannon;
	public GameObject Button_DefenseTurret;
	public GameObject Button_DefenseSlow;
	public GameObject Button_DefenseAntiAir;

	public Text resourceText;
	public Tile[] tile;
	public GameObject Core;
	//private GameObject TilePressed = null;
	public Enemy enemy;
	public bool isPause = false;

	public int selection = 1;
	public int resources = 300;

	Vector2 InputPos;

	// Use this for initialization
	void Start () {
		//Instantiate (enemy, new Vector2 (10, 0), Quaternion.identity);
		resourceText.text = "Resources: " + resources;
	}
	
	// Update is called once per frame
	void Update () {
		resourceText.text = "Resources: " + resources;
		InputPos = new Vector2 (0, 0);
		#if UNITY_ANDROID
		if (Input.touchCount > 0) {
			InputPos = Input.GetTouch(0).position;
			if (PauseButton.HitTest(InputPos)) {
				Pause = !Pause;
			}
			if (!Pause) {
				if (TilePressed == null) {
					Vector3 ScreenToWorld = Camera.main.ScreenToWorldPoint(InputPos);
					RaycastHit2D Pressed = Physics2D.Raycast(new Vector2(ScreenToWorld.x, ScreenToWorld.y),
					                                         Vector2.zero);

					if (Pressed != null) {
						if (Pressed.collider.gameObject.tag == "Tile") {
							TilePressed = Pressed.collider.gameObject;
						}
					}
				}
				else if (TilePressed != null) {
					if (TilePressed.GetComponent<Tile>().isOccupied == false) {
						TilePressed.GetComponent<Tile>().BuildDefense();
					}
				}
			}
		}
		#elif UNITY_EDITOR


		#endif
	}

	public void SetPause(bool Pause) {
		isPause = Pause;
	}

	public void SetSelection(int selection) {
		this.selection = selection;
	}

	public int GetSelection() {
		return selection;
	}
}
