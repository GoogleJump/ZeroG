using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Gameplay : MonoBehaviour {
	public State gameState;
	public GameLogic gameLogic;

	// Use this for initialization
	void Start () {
		gameState = State.d1Turn;

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
				Debug.Log ("default");
				break;
		}
		gameState = state;
	}

	public void centerCameraOnDetective(int detectiveNumber){
		Camera.main.transform.position = new Vector3(gameLogic.GameBoard.Players[detectiveNumber].transform.position.x, 
		                                             gameLogic.GameBoard.Players[detectiveNumber].transform.position.y, 
		                                             Camera.main.transform.position.z);
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