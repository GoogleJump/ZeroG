using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Gameplay : MonoBehaviour {
	public State gameState { get; set; }
	public GameLogic gameLogic;
	GameObject[] gameObjectDetectives;

	// Use this for initialization
	void Start () {
		Debug.Log ("gamplay initialized");
		gameState = State.d1Turn;
		gameLogic = GetComponent<GameLogic> ();

		gameObjectDetectives = GameObject.FindGameObjectsWithTag("Detective");
		//gameObject.GetComponent<Detective>()
	}
	
	// Main loop - Update is called once per frame
	void Update () {

	}

	public void goToState(State state){
		switch (state) {
			case State.d1Turn:
				Debug.Log ("d1turn");
				centerCameraOnDetective(1);
				break;
			case State.d2Turn:
				Debug.Log ("d2turn");
				centerCameraOnDetective(2);
				break;
			case State.d3Turn:
				Debug.Log ("d3turn");
				centerCameraOnDetective(3);
				break;
			case State.dLoss:
				Debug.Log ("dloss");
				break;
			case State.dWin:
				Debug.Log ("dwin");
				break;
			case State.mrXTurn:
				Debug.Log ("mrxTurn");
				break;
			default:
				Console.WriteLine ("default");
				break;
		}
		gameState = state;
	}

	public void centerCameraOnDetective(int detectiveNumber){
		int detectiveIndex = detectiveNumber - 1; 
		Camera.main.transform.position = new Vector3(gameObjectDetectives[detectiveIndex].transform.position.x, gameObjectDetectives[detectiveIndex].transform.position.x, Camera.main.transform.position.z);
	}

	public Player getCurrentPlayer(){
		return gameLogic.GameBoard.Players[currentPlayerId()];
	}

	public int currentPlayerId(){
		switch (gameState) {
			case State.d1Turn:
				return 1;
			case State.d2Turn:
				return 2;
			case State.d3Turn:
				return 3;
			case State.mrXTurn:
				return 0;
			default:
				return 0;
		}
	}
}