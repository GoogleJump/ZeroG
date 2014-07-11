using System.Collections;

public class Detective :  Player {
	public Detective (string name, int id) : base(name,id){
		_tickets.Add(TransportType.taxi, 10);
		_tickets.Add(TransportType.bus, 8);
		_tickets.Add(TransportType.underground, 4);
	}
}
