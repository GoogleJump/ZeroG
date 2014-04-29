import java.util.*;

public class Detective extends Player {
	private String name;
	private int id;
	private int[] tickets;
	private Node location;
	private LinkedList<Node> moveLog;
	
	public Detective(String name, int id, String color){
		super(name, id);
		this.color = color;
		tickets = {10, 8, 4};
	}
}