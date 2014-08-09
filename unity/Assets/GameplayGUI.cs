using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

[ExecuteInEditMode]
public class GameplayGUI : MonoBehaviour {
	
	private int btnWidth, btnHeight;
	private int menuBtnX, menuBtnY;
	private int groupWidth, groupHeight, groupWidth2, groupHeight2, groupX, groupY, groupX2, groupY2;
	private Vector2 scrollPosition = Vector2.zero;
	private Gameplay gameplay;
	
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
		groupWidth2 = 300;
		groupHeight2 = 75;
		groupX2 = Screen.width/3 - groupWidth2/2;
		groupY2 = Screen.height - groupHeight2;

		gameplay = Gameplay.instance;
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

			List<string> moveLog = gameplay.gameLogic.GameBoard.Players[0]._moveLog;
			

			StringBuilder innerText = new StringBuilder ();
			
			if (gameplay.turnCount == 1) {
				foreach (string move in moveLog) {
					innerText.Insert(0,move + "\n");
				}
			}


			Rect scrollViewDisplayPositionAndSize = new Rect (0, menuBtnY + btnHeight + 10, 100, 200);
			Rect scrollViewInteriorPositionAndSize = new Rect (0, 0, 200, 200);

			scrollPosition = GUI.BeginScrollView (scrollViewDisplayPositionAndSize, scrollPosition, scrollViewInteriorPositionAndSize);

			GUI.TextArea (scrollViewInteriorPositionAndSize, innerText.ToString());

			GUI.EndScrollView ();

			GUI.EndGroup ();

			// Makes a new group to hold the ticket information
			GUI.BeginGroup (new Rect (groupX2, groupY2, groupWidth2, groupHeight2));
			

			Player currentPlayer = gameplay.getCurrentPlayer ();
			string currentPlayerName = currentPlayer.getName (); //replace with a function to get current player from main loop
			int[] playerTickets = currentPlayer.getTickets ();
			GUI.Box (new Rect (0, 0, groupWidth2, groupHeight2), "Tickets: " + currentPlayerName);
			GUI.Label (new Rect (0, 15, groupWidth2 / 3, groupHeight2 / 4), "Taxi");
			GUI.Label (new Rect (0, 30, groupWidth2 / 3, groupHeight2 / 4), "" + playerTickets [(int)TransportType.taxi]);
			GUI.Label (new Rect (groupWidth2 / 3, 15, groupWidth2 / 3, groupHeight2 / 4), "Bus");
			GUI.Label (new Rect (groupWidth2 / 3, 30, groupWidth2 / 3, groupHeight2 / 4), "" + playerTickets [(int)TransportType.bus]);
			GUI.Label (new Rect (2 * groupWidth2 / 3, 15, groupWidth2 / 3, groupHeight2 / 4), "Underground");
			GUI.Label (new Rect (2 * groupWidth2 / 3, 30, groupWidth2 / 3, groupHeight2 / 4), "" + playerTickets [(int)TransportType.underground]);
			GUI.EndGroup ();
	}
}