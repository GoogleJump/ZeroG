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
}