import java.util.*;
public class Node {

	private int id;
	private Set<Node> taxiEdges, busEdges, ugEdges;

	
	Node(int id) {
		this.id = id;
		taxiEdges = new HashSet<Node>();
		busEdges = new HashSet<Node>();
		ugEdges = new HashSet<Node>();
	}

	public void addEdge(Node n, TransportType type) {
		switch (type) {
			case taxi:
				taxiEdges.add(n);
				break;
			case bus:
				busEdges.add(n);
				break;
			case underground:
				ugEdges.add(n);
				break;
			default:
				throw new IllegalArgumentException();
		}
	}

	/** Returns the position of this node */
	public int getId() {
		return id;
	}

	/** Returns the adjacent edges of this node */
	public Set<Node> getEdges(TransportType type) {
		switch (type) {
			case taxi:
				return taxiEdges;
			case bus:
				return busEdges;
			case underground:
				return ugEdges;
			default:
				throw new IllegalArgumentException();
		}
	}
}