using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TouchHandler : MonoBehaviour {
	bool alternateTouch; //prevents mouse from registering a touch twice
	public Gameplay gamePlay;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount != 1){
			return;
		}
		Touch firstFinger = Input.GetTouch (0);
		if (firstFinger.phase == TouchPhase.Ended && firstFinger.tapCount == 1) 
		{
			alternateTouch = !alternateTouch;
			Vector3 inputPosition  = Camera.main.ScreenToWorldPoint(new Vector3(firstFinger.position.x,firstFinger.position.y, 0));
			RaycastHit2D hit = Physics2D.Raycast(inputPosition, Vector2.zero);

			if (hit.collider != null) {
				Debug.Log ("I'm hitting "+hit.collider.name);
				if(hit.collider.name == "Detective"){
					Detective d = hit.collider.gameObject.GetComponent<Detective>();
					d.Select();
				}

				//Vector3 center = hit.collider.bounds.center;

				gamePlay.goToState(State.d1Turn);
			}

//			foreach(Detective d in detectives){
//				if (d.isSelected() && alternateTouch){
//					d.moveGameObject(inputPosition.x, inputPosition.y);
//					d.Deselect();
//				}
//			}
		}
	}
}