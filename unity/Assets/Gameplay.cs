using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Gameplay : MonoBehaviour {
	public State gameState;
	public GameLogic gameLogic;
	public Message message;

	// Use this for initialization
	void Start () {
		goToState (State.d1Turn);

		//move players to proper locations
		Debug.Log ("Node" + gameLogic.GameBoard.Players [0].Location.Id);
		Node mrXNode = GameObject.Find("Node" + gameLogic.GameBoard.Players[0].Location.Id).GetComponent<Node>();
		Node detective1Node = GameObject.Find("Node" + gameLogic.GameBoard.Players[1].Location.Id).GetComponent<Node>();
		Node detective2Node = GameObject.Find("Node" + gameLogic.GameBoard.Players[2].Location.Id).GetComponent<Node>();
		Node detective3Node = GameObject.Find("Node" + gameLogic.GameBoard.Players[3].Location.Id).GetComponent<Node>();

		//TODO: create new vector3's from x and y
		gameLogic.GameBoard.Players[0].transform.position = mrXNode.transform.position;
		gameLogic.GameBoard.Players[1].transform.position = detective1Node.transform.position;
		gameLogic.GameBoard.Players[2].transform.position = detective2Node.transform.position;
		gameLogic.GameBoard.Players[3].transform.position = detective3Node .transform.position;

	}
	
	// Main loop - Update is called once per frame
	void Update () {

	}

	public void goToState(State state){
		switch (state) {
			case State.d1Turn:
				Debug.Log ("d1turn");
				centerCameraOnDetective(1);
				Message.PopUp("Detective 1's turn...", "Ok");
				break;
			case State.d2Turn:
				Debug.Log ("d2turn");
				centerCameraOnDetective(2);
				Message.PopUp("Detective 2's turn...", "Ok");
				break;
			case State.d3Turn:
				Debug.Log ("d3turn");
				centerCameraOnDetective(3);
				Message.PopUp("Detective 3's turn...", "Ok");
				break;
			case State.dLoss:
				Debug.Log ("dloss");
				Message.PopUp("Detectives lose...", "Reset game");
				break;
			case State.dWin:
				Debug.Log ("dwin");
				Message.PopUp ("Detectives win!", "Reset game");
				break;
			case State.mrXTurn:
				Debug.Log ("mrxTurn");
				Message.PopUp ("MrX's turn...", "Ok");
				break;
			default:
				Debug.Log ("default");
				break;
		}
		gameState = state;
	}

	public void goToNextTurn(){
		switch (gameState) {
			case State.d1Turn:
				goToState(State.d2Turn);
				break;
			case State.d2Turn:
				goToState(State.d3Turn);
				break;
			case State.d3Turn:
				goToState(State.mrXTurn);
				break;
			case State.mrXTurn:
				goToState(State.d1Turn);
				break;
			default: //should never happen
				break;
			}
	}

	public void centerCameraOnDetective(int detectiveNumber){
		Camera.main.transform.position = new Vector3(gameLogic.GameBoard.Players[detectiveNumber].transform.position.x, 
		                                             gameLogic.GameBoard.Players[detectiveNumber].transform.position.y, 
		                                             Camera.main.transform.position.z);
	}


	public void TryMovePlayer(int nodeID, Vector3 position){
		if (gameLogic.canMoveToNode (getCurrentPlayer (), nodeID)) {
			gameLogic.GameBoard.Players[currentPlayerId()].moveGameObject(position);
		}

		int winningPlayerId;
		if (gameLogic.winnerExists(gameLogic.GameBoard.Players, out winningPlayerId)) {
			if(winningPlayerId == 0){
				goToState(State.dLoss);
			} else {
				goToState(State.dWin);
			}
			return;
		}
		goToNextTurn();
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
	public Player getCurrentPlayer(){
		return gameLogic.GameBoard.Players[currentPlayerId()];
	}
}