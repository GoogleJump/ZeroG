using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour {
	public String _name;
	public int _id;
	public IDictionary<TransportType, int> _tickets = new Dictionary<TransportType, int>();
	public Node Location;
	public List<String> _moveLog;

	void Start(){

	}

	public void createPlayer(String name, int id){
		_name = name;
		_id = id;
		_moveLog = new List<String>();
	}

	public void resetTickets(){
		//a hack because can't use virtual methods...just change this to property of a player
		if (this.GetType () == typeof(Detective)) {
			_tickets[TransportType.taxi] = 10;
			_tickets[TransportType.bus] = 8;
			_tickets[TransportType.underground] = 4;
		} else {
			_tickets[TransportType.taxi] = 4;
			_tickets[TransportType.bus] = 3;
			_tickets[TransportType.underground] = 3;
			_tickets[TransportType.blackCard] = 2;
		}
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
		return Location;
	}

	public string getName(){
		return _name;
	}
	
	public void setLocation(Node location){
		Location = location;
		_moveLog.Add(location.Id.ToString());
	}
	
	public int getTickets(TransportType ticket){
		int numOfTickets;
		if(_tickets.TryGetValue(ticket, out numOfTickets)){
			return numOfTickets;
		} else {
			return 0;
		}
	}

	public int[] getTickets(){
		int typesOfTickets = Enum.GetNames(typeof(TransportType)).Length;
		int[] tickets = new int[typesOfTickets];
		foreach (var transportType in _tickets) {
			tickets[(int) transportType.Key] = _tickets[transportType.Key];
		}
		return tickets;
	}
	
	public int getMoveLogSize(){
		return _moveLog.Count();
	}

	public void moveGameObject(Vector3 position){
		//have to create new vector3, cannot just assign properties
		Debug.Log ("pos: " + position);
		Debug.Log ("old pos: " + transform.position);
		transform.position = new Vector3 (position.x, position.y, transform.position.z); 
	}
}