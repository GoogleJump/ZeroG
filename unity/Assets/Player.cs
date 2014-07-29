using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour {
	protected String _name;
	protected int _id;
	protected IDictionary<TransportType, int> _tickets = new Dictionary<TransportType, int>();
	protected Node _location;
	protected List<String> _moveLog;

	void Start(){
		//a hack because can't use virtual methods...
		if (this.GetType () == typeof(Detective)) {
						_tickets.Add (TransportType.taxi, 10);
						_tickets.Add (TransportType.bus, 8);
						_tickets.Add (TransportType.underground, 4);
				} else {
			_tickets.Add(TransportType.taxi, 4);
			_tickets.Add(TransportType.bus, 3);
			_tickets.Add(TransportType.underground, 3);
			_tickets.Add(TransportType.blackCard, 2);
				}
	}

	public void createPlayer(String name, int id){
		_name = name;
		_id = id;
		_moveLog = new List<String>();
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

	public string getName(){
		return _name;
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
}