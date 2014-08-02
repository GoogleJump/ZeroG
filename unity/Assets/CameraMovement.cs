using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public float cameraLimitXMin;
	public float cameraLimitXMax;
	public float cameraLimitYMin;
	public float cameraLimitYMax;
	private Vector2 delta;
	

	public float zoomSpeed = 5.0f;
	//buckets for caching our touch positions
	private Vector2 currTouch1 = Vector2.zero,
	lastTouch1 = Vector2.zero,
	currTouch2 = Vector2.zero,
	lastTouch2 = Vector2.zero;

	//used for holding our distances and calculating our zoomFactor
	private float currDist = 0.0f,
	lastDist = 0.0f,
	zoomFactor = 0.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved) {
			scrollMap ();
		} else if (Input.touchCount == 2){
			zoom ();
		}
	}

	void scrollMap ()
	{
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;			
		Camera.main.transform.Translate(-touchDeltaPosition.x * 0.03f, -touchDeltaPosition.y *0.03f, 0);
		Camera.main.transform.position = new Vector3(Mathf.Clamp(transform.position.x, cameraLimitXMin, cameraLimitXMax),
			                                 Mathf.Clamp(transform.position.y, cameraLimitYMin, cameraLimitYMax),
		                                 -10);
	}

	void zoom(){
		//Caches touch positions for each finger
		switch(Input.touchCount)
		{
		case 0://first touch
			currTouch1 = Input.GetTouch(0).position;
			lastTouch1 = currTouch1 - Input.GetTouch(0).deltaPosition;
			break;
		case 1://second touch
			currTouch2 = Input.GetTouch(1).position;
			lastTouch2 = currTouch2 - Input.GetTouch(1).deltaPosition;
			break;
		}
		//finds the distance between your moved touches
		//we dont want to find the distance between 1 finger and nothing
		if(Input.touchCount >= 1)
		{
			currDist = Vector2.Distance(currTouch1, currTouch2);
			lastDist = Vector2.Distance(lastTouch1, lastTouch2);
		}
		else
		{
			currDist = 0.0f;
			lastDist = 0.0f;
		}
		//Calculate the zoom magnitude
		zoomFactor = Mathf.Clamp(lastDist - currDist, -30.0f, 30.0f);
		//apply zoom to our camera
		Camera.main.transform.Translate(Vector3.forward * zoomFactor * zoomSpeed * Time.deltaTime);
	}

	public void moveToPoint(Vector2 position){
		Camera.main.transform.position = new Vector3 (0, 0, 0);
	}
}
