import java.util.*;

public class Player {
	private String name;
	private int id;
	private int[] tickets;
	private LinkedList<String> moveLog;
	private Node location;
	
	public Player(String name, int id){
		this.name = name;
		this.id = id;
		moveLog = new LinkedList<String>();
	}
	
	public boolean move(){
		return false;
	}
	
	public Node getLocation(){
		return location;
	}
	
	public void setLocation(Node location){
		this.location = location;
	}
}
