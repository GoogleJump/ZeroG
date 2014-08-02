using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour{

	public int Id;

	//For AI, priorty queue
	public double Priority;
	public long InsertionIndex;
	public int QueueIndex;
	public Color Color;
	public int Value;
	public Node Parent;

	private HashSet<Node> _taxiEdges, _busEdges, _ugEdges; //.net 3.5 doesn't support iset<t>

	void Awake(){
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

	public void addEdges(Node otherNode, TransportType type) {
		addEdge (otherNode, type);
		otherNode.addEdge (this, type);
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

	//only called when player can make a move
	public TransportType getTransportationConnection(Node originatingNode){
		if(_taxiEdges.Contains(originatingNode)){
			return TransportType.taxi;
		}
		if (_busEdges.Contains (originatingNode)){
			return TransportType.bus;
		}
		if (_ugEdges.Contains (originatingNode)) {
			return TransportType.underground;
		}

		//should only happen for mr x
		return TransportType.blackCard;
	}

	// Use this for initialization
	void Start () {
	
	}
}
