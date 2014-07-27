using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour {
	protected String _name;
	protected int _id;
	protected IDictionary<TransportType, int> _tickets;
	protected Node _location;
	protected IList<String> _moveLog;
	
	public Player(String name, int id){
		_name = name;
		_id = id;
		_moveLog = new List<String>();
		_tickets = new Dictionary<TransportType, int>();
	}
	
	//This method is only called if the move is valid
	//GameController class will check if there are enough tickets
	public void move(Node n, TransportType ticket){
		int numOfTickets;
		if(_tickets.TryGetValue(ticket, out numOfTickets)){
			_tickets[ticket] = 	--numOfTickets;
			setLocation(n);
		}
	}
	
	public int getId(){
		return _id;
	}
	
	public Node getLocation(){
		return _location;
	}
	
	public void setLocation(Node location){
		_location = location;
		_moveLog.Add(location.ToString());
	}
	
	public int getTickets(TransportType ticket){
		int numOfTickets;
		if(_tickets.TryGetValue(ticket, out numOfTickets)){
			return numOfTickets;
		} else {
			return 0;
		}
	}
	
	public int getMoveLogSize(){
		return _moveLog.Count();
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}