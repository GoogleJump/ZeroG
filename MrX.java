import java.util.*;

public class MrX extends Player {
	private String name;
	private int id;
	private int[] tickets;
	private Node location;
	private LinkedList<Node> moveLog;
	
	public MrX(String name, int id){
		super(name, id);
		tickets = {4, 3, 3, 2};
	}
}