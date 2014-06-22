using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node {

	private int id;
	private HashSet<Node> taxiEdges, busEdges, ugEdges; //.net 3.5 doesn't support iset<t>
	
	public Node(int id) {
		this.id = id;
		taxiEdges = new HashSet<Node>();
		busEdges = new HashSet<Node>();
		ugEdges = new HashSet<Node>();
	}
	
//	public void addEdge(Node n, TransportType type) {
//		switch (type) {
//		case TransportType.taxi:
//			taxiEdges.Add(n);
//			break;
//		case TransportType.bus:
//			busEdges.Add(n);
//			break;
//		case TransportType.underground:
//			ugEdges.Add(n);
//			break;
//		//default:
//		//	throw new IllegalArgumentException();
//		}
//	}
	
	/** Returns the position of this node */
	public int getId() {
		return id;
	}
	
	/** Returns the adjacent edges of this node */
//	public HashSet<Node> getEdges(TransportType type) {
//		switch (type) {
//		case TransportType.taxi:
//			return taxiEdges;
//		case TransportType.bus:
//			return busEdges;
//		case TransportType.underground:
//			return ugEdges;
//		//default:
//		//	throw new IllegalArgumentException();
//		}
//	}
	
	public HashSet<Node> getAllEdges(){
		HashSet<Node> allEdges = new HashSet<Node>();
//		allEdges.addAll(taxiEdges);
//		allEdges.addAll(busEdges);
//		allEdges.addAll(ugEdges);
		return allEdges;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
