using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchHandler : MonoBehaviour {
	IList<Detective> detectives;
	Detective d;
	bool alternateTouch; //prevents mouse from registering a tocuh twice
	public GameLogic gameLogic;
	public CameraMovement cameraMovement;

	// Use this for initialization
	void Start () {
		Detective d = GameObject.Find ("Detective").GetComponent<Detective>();
		Debug.Log ("d:" + d.ToString ());
		GameObject[] gameObjectDetectives = GameObject.FindGameObjectsWithTag("Detective");
		detectives = new List<Detective>();
		foreach(GameObject gameObject in gameObjectDetectives){
			detectives.Add(gameObject.GetComponent<Detective>());
		}
		bool alternate = false;

		gameLogic = GetComponent<GameLogic> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) 
		{
			alternateTouch = !alternateTouch;
			Vector3 inputPosition  = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x,Input.GetTouch(0).position.y, 0));
			RaycastHit2D hit = Physics2D.Raycast(inputPosition, Vector2.zero);

			if (hit.collider != null) {
				Debug.Log ("I'm hitting "+hit.collider.name);
				if(hit.collider.name == "Detective"){
					Detective d = hit.collider.gameObject.GetComponent<Detective>();
					d.Select();
				}

				Vector3 center = hit.collider.bounds.center;
				Camera.main.transform.position = new Vector3(center.x, center.y, Camera.main.transform.position.z);
			}

			foreach(Detective d in detectives){
				if (d.isSelected() && alternateTouch){
					d.moveGameObject(inputPosition.x, inputPosition.y);
					d.Deselect();
				}
			}
		}
	}
	

}
