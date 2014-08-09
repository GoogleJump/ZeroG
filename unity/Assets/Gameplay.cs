using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class Gameplay : MonoBehaviour {
	public TextAsset boardNodesTextAsset;
	public State gameState;
	public GameLogic gameLogic;
	public Message message;
	public Detective detective1, detective2, detective3;
	public MrX mrX;
	public int turnCount;

	public static Gameplay instance;
	public Gameplay(){
	}

	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		turnCount = 0;
		gameLogic = new GameLogic (boardNodesTextAsset, mrX, detective1, detective2, detective3);
		//move players to proper locations
		Node mrXNode = GameObject.Find("Node" + gameLogic.GameBoard.Players[0].Location.Id).GetComponent<Node>();
		Node detective1Node = GameObject.Find("Node" + gameLogic.GameBoard.Players[1].Location.Id).GetComponent<Node>();
		Node detective2Node = GameObject.Find("Node" + gameLogic.GameBoard.Players[2].Location.Id).GetComponent<Node>();
		Node detective3Node = GameObject.Find("Node" + gameLogic.GameBoard.Players[3].Location.Id).GetComponent<Node>();


		gameLogic.GameBoard.Players[0].transform.position = mrXNode.transform.position;
		gameLogic.GameBoard.Players[0].renderer.enabled = false;

		gameLogic.GameBoard.Players[1].transform.position = detective1Node.transform.position;
		gameLogic.GameBoard.Players[2].transform.position = detective2Node.transform.position;
		gameLogic.GameBoard.Players[3].transform.position = detective3Node.transform.position;

		goToState (State.d1Turn);

	}
	
	// Main loop - Update is called once per frame
	void Update () {

	}

	public void goToState(State state){
		Debug.Log ("goto: " + state);
		gameState = state;
		switch (state) {
			case State.d1Turn:
				Debug.Log ("d1turn");
				centerCameraOnDetective(1);
				message.PopUp("Detective 1's turn...", "Ok");
				break;
			case State.d2Turn:
				Debug.Log ("d2turn");
				centerCameraOnDetective(2);
				message.PopUp("Detective 2's turn...", "Ok");
				break;
			case State.d3Turn:
				Debug.Log ("d3turn");
				centerCameraOnDetective(3);
				message.PopUp("Detective 3's turn...", "Ok");
				break;
			case State.dLoss:
				Debug.Log ("dloss");
				message.PopUp("Detectives lose...", "Reset game");
				gameLogic.GameBoard.reset();
				break;
			case State.dWin:
				Debug.Log ("dwin");
				message.PopUp ("Detectives win!", "Reset game");
				break;
			case State.mrXTurn:
				Debug.Log ("mrxTurn");
				//gameState = State.mrXTurn;
				HashSet<Node> possibleMoves = gameLogic.GetPossibleMoves(0, gameLogic.GameBoard);
				Node destination = possibleMoves.ElementAt(new System.Random().Next(possibleMoves.Count));
				//Node destination = MinMax.minMax(gameLogic, 0, 1).Players[0].Location;
				Debug.Log ("mr x moving, from " + gameLogic.GameBoard.Players[0].Location.Id + " to " + destination.Id);
				moveMrX(destination.gameObject);
				++turnCount;
				Debug.Log ("turncount: " + turnCount);
				if(turnCount == 3){
					gameLogic.GameBoard.Players[0].renderer.enabled = true;
					turnCount = 0;
				} else {
					gameLogic.GameBoard.Players[0].renderer.enabled = false;
				}
				goToNextTurn();
				
				break;
			default:
				Debug.Log ("default");
				break;
		}
	}

	public void goToNextTurn(){
		Debug.Log ("gotonext current gameState: " + gameState);
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

	private void moveMrX(GameObject node){
		Node destination = node.GetComponent<Node>();
		Vector3 position = node.GetComponent<Collider2D>().bounds.center;
		Player currentPlayer = gameLogic.GameBoard.Players[currentPlayerId()];
		Node source = gameLogic.GameBoard.Players[0].Location;
		currentPlayer.moveGameObject(position);
		currentPlayer.move(destination, destination.getTransportationConnection(source));
		
		int winningPlayerId;
		if (gameLogic.winnerExists(gameLogic.GameBoard.Players, out winningPlayerId)) {
			if(winningPlayerId == 0){
				goToState(State.dLoss);
			} else {
				goToState(State.dWin);
			}
			return;
		}
	}
	
	public void TryMovePlayer(GameObject node){
		Node destination = node.GetComponent<Node>();
		Vector3 position = node.GetComponent<Collider2D>().bounds.center;
		Player currentPlayer = gameLogic.GameBoard.Players[currentPlayerId()];
		Node source = currentPlayer.Location;
		if (gameLogic.canMoveToNode (getCurrentPlayer(), destination.Id)) {
			currentPlayer.moveGameObject(position);
			currentPlayer.move(destination, destination.getTransportationConnection(source));
		} else {
			Debug.Log ("cannot move: id: " + getCurrentPlayer() + " destination: " + destination.Id);
			message.PopUp("Invalid move", "Try again");
			return;
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