using UnityEngine;
using System.Collections;
using System;

public class Gameplay : MonoBehaviour {
	public State gameState { get; set; }
	public GameLogic gameLogic;

	// Use this for initialization
	void Start () {
		gameState = State.d1Turn;
		gameLogic = GetComponent<GameLogic> ();
	}
	
	// Main loop - Update is called once per frame
	void Update () {

	}

	public void incrementState(){
				gameState++;
				switch (gameState) {
				case State.d1Turn:
					Debug.Log ("state 1");
						break;
				case State.d2Turn:
					Debug.Log ("state 2");
						break;
				case State.d3Turn:
						break;
				case State.dLoss:
						break;
				case State.dWin:
						break;
				case State.mrXTurn:
						break;
				default:
						Console.WriteLine ("default");
						break;
				}
		}
}