using UnityEngine;
using System.Collections;

public class Detective :  Player {

	private bool selected;

	public void Select() {
		selected = true;
	}

	public void Deselect() {
		selected = false;
	}

	public bool isSelected(){
		return selected;
	}

//	public void moveGameObject (float x, float y){
//		transform.position = new Vector3 (x,y);
//		Debug.Log ("x: " + x + "y: " + y);
//		Debug.Log ("moved" );
//	}
}