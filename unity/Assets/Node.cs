using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node {

	public int Id { get; set; }
	private HashSet<Node> _taxiEdges, _busEdges, _ugEdges; //.net 3.5 doesn't support iset<t>

	public Node(int id) {
		Id = id;
		_taxiEdges = new HashSet<Node>();
		_busEdges = new HashSet<Node>();
		_ugEdges = new HashSet<Node>();
	}
	
	public void addEdge(Node n, TransportType type) {
		switch (type) {
		case TransportType.taxi:
			_taxiEdges.Add(n);
			break;
		case TransportType.bus:
			_busEdges.Add(n);
			break;
		case TransportType.underground:
			_ugEdges.Add(n);
			break;
		default:
			break;
		}
	}


	/** Returns the adjacent edges of this node */
	public HashSet<Node> getEdges(TransportType type) {
		switch (type) {
			case TransportType.taxi:
				return _taxiEdges;
			case TransportType.bus:
				return _busEdges;
			case TransportType.underground:
				return _ugEdges;
			default:
				return new HashSet<Node>();
		}
	}
	
	public HashSet<Node> getAllEdges(){
		HashSet<Node> allEdges = new HashSet<Node>();
		allEdges.UnionWith(_taxiEdges);
		allEdges.UnionWith(_busEdges);
		allEdges.UnionWith(_ugEdges);
		return allEdges;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
