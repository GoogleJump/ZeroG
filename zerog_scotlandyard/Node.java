package zerog_scotlandyard;
import java.util.HashSet;
import java.util.Set;


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
	
	public Set<Node> getAllEdges(){
		Set<Node> allEdges = new HashSet<Node>();
		allEdges.addAll(taxiEdges);
		allEdges.addAll(busEdges);
		allEdges.addAll(ugEdges);
		return allEdges;
	}
}