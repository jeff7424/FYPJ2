using UnityEngine;
using System.Collections;

public class Core : MonoBehaviour {

	TextMesh tm;

	// Use this for initialization
	void Start () {
		tm = GetComponent<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public int CurrentHealth() {
		return tm.text.Length;
	}

	public void DecreaseHealth() {
		if (CurrentHealth () > 1)
			tm.text = tm.text.Remove (tm.text.Length - 1);
		else
			Destroy (transform.parent.gameObject);
	}
}
