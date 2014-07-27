using UnityEngine;
using System.Collections;

public class Detective :  Player {

	private bool selected;
	// Use this for initialization
	void Start () {
		selected = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Detective (string name, int id) : base(name,id){
		_tickets.Add(TransportType.taxi, 10);
		_tickets.Add(TransportType.bus, 8);
		_tickets.Add(TransportType.underground, 4);
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

	public void moveGameObject (float x, float y){
		transform.position = new Vector3 (x,y);
		Debug.Log ("x: " + x + "y: " + y);
		Debug.Log ("moved" );
	}
}
