import java.util.*;

public class Player {
	private String name;
	private int id;
	private int[] tickets;
	private Node location;
	private LinkedList<Node> moveLog;
	
	public Player(String name, int id){
		this.name = name;
		this.id = id;
		this.moveLog = new LinkedList<String>();
	}
	
	public boolean move(){
		
	}
	
	public Node getLocation(){
		return location;
	}
	
	public void setLocation(Node location){
		this.location = location;
		moveLog.add(location);
	}
}
