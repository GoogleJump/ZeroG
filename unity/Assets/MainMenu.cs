using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	private int btnWidth, btnHeight;
	private int playBtnX;
	private int groupWidth, groupHeight, groupX, groupY;
	// Use this for initialization
	void Start () {
		btnWidth = 80;
		btnHeight = 30;
		groupWidth = 200;
		groupHeight = 100;
		groupX = Screen.width/2 - groupWidth/2;
		groupY = Screen.height/2 - groupHeight/2;
		playBtnX = groupWidth / 2 - btnWidth/2;
	}


	void OnGUI () {
		// Make a group on the center of the screen
		GUI.BeginGroup (new Rect (groupX, groupY, groupWidth, groupHeight));
		// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.
		

		GUI.Box (new Rect (0,0,groupWidth,groupHeight), "Scotland Yard by Zero G");
		if (GUI.Button (new Rect (playBtnX, 40, btnWidth, btnHeight), "Play")) {
			Application.LoadLevel(1);
		}

		GUI.EndGroup ();
	}
}
