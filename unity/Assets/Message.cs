using UnityEngine;
using System.Collections;

public class Message : MonoBehaviour {
	private static Rect windowRect;
	public bool showWindow;
	private static string Title;
	private static string Button;
	private static int groupHeight = 100;
	private static int groupWidth = 300;
	public static bool buttonPressed;
	
	// Use this for initialization
	void Start () {
		showWindow = false;
		windowRect = new Rect(Screen.width/2 - groupWidth/2, Screen.height/2 - groupHeight/2, groupWidth, groupHeight);
	}
	
	void OnGUI() {
		if (showWindow){
			GUI.Window(0, windowRect, DoMyWindow, Title);
		}
	}
	
	void DoMyWindow(int windowID) {
		if (GUI.Button (new Rect (75, 25, 150, 50), Button)) {
			showWindow = false;
			buttonPressed = true;
		}
	}
	
	public void PopUp (string title, string button){
		Title = title;
		Button = button;
		showWindow = true;
		buttonPressed = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}