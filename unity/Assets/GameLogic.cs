using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour {

	public HashSet<Node> gameBoard;
	public GameLogic(){
		gameBoard = new HashSet<Node> ();
	}

	public void initialize(){
		gameBoard.Add ((new Node (81)));
	}
}