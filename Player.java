import java.util.*;

public class Player {
	protected String name;
	protected int id;
	protected int[] tickets;
	protected Node location;
	protected List<Node> moveLog;
	
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
