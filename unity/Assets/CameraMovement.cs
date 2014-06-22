using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public float cameraLimitXMin;
	public float cameraLimitXMax;
	public float cameraLimitYMin;
	public float cameraLimitYMax;
	private Vector2 prevMousePosition;
	private Vector2 delta;

	// Use this for initialization
	void Start () {
	 	prevMousePosition = new Vector2 (0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		scrollMap ();
	}

	void scrollMap ()
	{
			if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved){
				Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;			
				transform.Translate(-touchDeltaPosition.x * 0.02f, -touchDeltaPosition.y *0.02f, 0);
				transform.position = new Vector3(Mathf.Clamp(transform.position.x, cameraLimitXMin, cameraLimitXMax),
				                                 Mathf.Clamp(transform.position.y, cameraLimitYMin, cameraLimitYMax),
			                                 -10);
			}
	}
}
