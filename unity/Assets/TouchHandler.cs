using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchHandler : MonoBehaviour {
	IList<Detective> detectives;
	Detective d;
	bool alternateTouch; //prevents mouse from registering a tocuh twice
	GameLogic gameLogic;

	// Use this for initialization
	void Start () {
		gameLogic = new GameLogic ();
		Detective d = GameObject.Find ("Detective").GetComponent<Detective>();
		Debug.Log ("d:" + d.ToString ());
		GameObject[] gameObjectDetectives = GameObject.FindGameObjectsWithTag("Detective");
		detectives = new List<Detective>();
		foreach(GameObject gameObject in gameObjectDetectives){
			detectives.Add(gameObject.GetComponent<Detective>());
		}
		bool alternate = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) 
		//if (Input.GetMouseButtonDown(0))
		{
			alternateTouch = !alternateTouch;
			Vector3 inputPosition  = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x,Input.GetTouch(0).position.y, 0));
			//Vector3 inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(inputPosition, Vector2.zero);

			if (hit.collider != null) {
				Debug.Log ("I'm hitting "+hit.collider.name);
				if(hit.collider.name == "Detective"){
					Detective d = hit.collider.gameObject.GetComponent<Detective>();
					d.Select();
				}
			}

			foreach(Detective d in detectives){
				if (d.isSelected() && alternateTouch){
					d.move(inputPosition.x, inputPosition.y);
					d.Deselect();
				}
			}
		}
	}
}
