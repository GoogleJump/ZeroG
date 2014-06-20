using UnityEngine;
using System.Collections;

public class Detective : MonoBehaviour {

	private bool selected;
	// Use this for initialization
	void Start () {
		selected = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Select() {
		selected = true;
	}

	public void Deselect() {
		selected = false;
	}

	public bool isSelected(){
		return selected;
	}

	public void move (float x, float y){
		transform.position = new Vector3 (x,y);
		Debug.Log ("x: " + x + "y: " + y);
		Debug.Log ("moved" );
	}
}
