using UnityEngine;
using System.Collections;
using System;

public class MrX : Player {
	public MrX(String name, int id) : base(name,id){
		_tickets.Add(TransportType.taxi, 4);
		_tickets.Add(TransportType.bus, 3);
		_tickets.Add(TransportType.underground, 3);
		_tickets.Add(TransportType.blackCard, 2);
	}
}