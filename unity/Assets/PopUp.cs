using UnityEngine;
using System.Collections;

public class Message : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public Rect windowRect = new Rect(20, 20, 120, 50);

	void OnGUI() {
		windowRect = GUI.Window(0, windowRect, DoMyWindow, "My Window");
	}


	void PopUp(string phrase){
		windowRect = GUI.Window(0, windowRect, DoMyWindow, phrase);
	}
	
	void DoMyWindow(int windowID) {
		if (GUI.Button(new Rect(10, 20, 100, 20), phrase))
			print("OK");
	}

	// Update is called once per frame
	void Update () {
	
	}
}