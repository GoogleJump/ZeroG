using System;
using System.Collections.Generic;
using UnityEngine;

public class GamePosition : MonoBehaviour {
	public Dictionary<int, Node> Board = new Dictionary<int, Node>();
	public Dictionary<int, Player> Players = new Dictionary<int, Player>();
	public MrX mrX;
	public Detective detective1, detective2, detective3;

	void Awake(){
		Players.Add (0, mrX);
		Players.Add (1, detective1);
		Players.Add (2, detective2);
		Players.Add (3, detective3);

		Players[0].Location = Board[1];
		Players[1].Location = Board[63];
		Players[2].Location = Board[95];
		Players[3].Location = Board[101];

//		Players[0].createPlayer("Mr X", 0);
//		Players[1].createPlayer("Detective 1", 1);
//		Players[2].createPlayer("Detective 2", 2);
//		Players[3].createPlayer("Detective 3", 3);
	}
}