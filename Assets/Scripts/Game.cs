using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public GUITexture PauseButton = null;

	public Tile[] tile;
	public GameObject Core;
	private GameObject TilePressed = null;
	public Enemy enemy;
	public bool Pause = false;

	Vector2 InputPos;

	// Use this for initialization
	void Start () {
		//Instantiate (enemy, new Vector2 (10, 0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
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
		//if (Input.GetMouseButton (0)) {
		//	if (PauseButton.HitTest (InputPos)) {
		//		Pause = !Pause;
		//	}
		//}
		//if (TilePressed == null) {
		//	Vector3 ScreenToWorld = Camera.main.ScreenToWorldPoint (InputPos);
		//	RaycastHit2D Pressed = Physics2D.Raycast (new Vector2(ScreenToWorld.x, ScreenToWorld.y),
		//	                                          Vector2.zero);

		//	if (Pressed != null) {
		//		if (Pressed.collider.gameObject.tag == "Tile") {
		//			TilePressed = Pressed.collider.gameObject;
		//		}
		//	}
		//}
		//else if (TilePressed != null) {
		//	tile = TilePressed.GetComponents<Tile>();
		//	foreach (Tile tiles in tile) {
				//if (tiles.isOccupied == false) {
				//	tiles.BuildDefense();
				//}
		//		Debug.Log ("Clicked");
		//	}
		//}
		#endif
	}
}
