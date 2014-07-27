using UnityEngine;
using System.Collections;

public class GameplayGUI : MonoBehaviour {

	private int btnWidth, btnHeight;
	private int menuBtnX, menuBtnY;
	private int groupWidth, groupHeight, groupX, groupY;
	private Message PopUp;
	// Use this for initialization
	void Start () {
		btnWidth = 80;
		btnHeight = 30;
		groupWidth = 100;
		groupHeight = 300;
		groupX = Screen.width - groupWidth;
		groupY = Screen.height/2 - groupHeight/2;
		menuBtnX = 10;
		menuBtnY = 40;
		PopUp = GetComponent<Message> ();
	}
	void OnGUI ()
	{
		// Make a group on the center of the screen
		GUI.BeginGroup (new Rect (groupX, groupY, groupWidth, groupHeight));
		// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.


		GUI.Box (new Rect (0, 0, groupWidth, groupHeight), "Move log");
		if (GUI.Button (new Rect (menuBtnX, menuBtnY, btnWidth, btnHeight), "Main menu")) {
				Application.LoadLevel (0);
		}

		Vector2 scrollPosition = Vector2.zero;

		Rect scrollViewDisplayPositionAndSize = new Rect (0, menuBtnY + btnHeight + 10, 100, 200);
		Rect scrollViewInteriorPositionAndSize = new Rect (0, 0, 200, 200);
		GUI.BeginScrollView(scrollViewDisplayPositionAndSize, scrollPosition, scrollViewInteriorPositionAndSize);
		GUI.Button(new Rect(0, 0, 100, 20), "Top");
		GUI.Button(new Rect(120, 180, 100, 20), "Bottom-right");
		GUI.EndScrollView();

		GUI.EndGroup ();
	}
}
