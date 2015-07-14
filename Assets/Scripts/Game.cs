using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

	//public GUITexture PauseButton;
	public GameObject infoPanel;

	public Text resourceText;
	public GameObject Core;
	public Enemy enemy;
	public bool isPause = false;
	public bool mouseOverTile = false;
	public bool mouseOverPanel = false;

	public int selection = 1;
	public int resources = 200;

	Vector2 InputPos;

	// Use this for initialization
	void Start () {
		//Instantiate (enemy, new Vector2 (10, 0), Quaternion.identity);
		resourceText.text = "Resources: " + resources;
		infoPanel = GameObject.Find ("Info panel");
		infoPanel.GetComponent<InfoPanelScript> ().DisablePanel ();
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
		if (Input.GetMouseButtonDown(0) && (!mouseOverTile && !mouseOverPanel)) {
			DisableInfoPanel();
		}

		#endif
	}

//	void OnMouseDown() {
//		// Check if mouse over tile when tapping.
//		// If mouse not over tile when tapping disable info panel is called
//		if (!mouseOverTile) {
//			DisableInfoPanel ();
//		}
//	}

	public void SetPause() {
		isPause = !isPause;
		if (isPause == true) {
			Time.timeScale = 0.0f;
		} else {
			Time.timeScale = 1.0f;
		}
	}

	public void SetSelection(int selection) {
		this.selection = selection;
	}

	public int GetSelection() {
		return selection;
	}

	public void EnableInfoPanel() {
		infoPanel.GetComponent<InfoPanelScript> ().EnablePanel ();
	}

	public void DisableInfoPanel() {
		infoPanel.GetComponent<InfoPanelScript> ().DisablePanel ();
	}

	public void SetInfoPanelTarget(Defense defense) {
		infoPanel.GetComponent<InfoPanelScript> ().defense = defense;
	}

	public void SetMouseOverPanel (bool isOver) {
		mouseOverPanel = isOver;
	}
}
