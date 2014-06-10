package zerog_scotlandyard;

import static org.junit.Assert.*;

import org.junit.Test;
import org.junit.runner.JUnitCore;
import org.junit.runner.Result;
import org.junit.runner.notification.Failure;

import java.util.List;
import java.util.ArrayList;
import java.util.Map;
import java.util.HashMap;
import java.util.Set;
import java.util.HashSet;

public class ScotlandYardTest{
	@Test
	public void basicNodeTest(){
		Node[] nodes = constructBasicBoard();
		Map<Integer, List<Set<Node>>> edgeMap = initializeEdgeMap();
		
		edgeMap.get(0).get(0).add(nodes[2]);
		edgeMap.get(0).get(0).add(nodes[3]);
		edgeMap.get(0).get(0).add(nodes[4]);
		edgeMap.get(0).get(1).add(nodes[1]);
		edgeMap.get(0).get(1).add(nodes[3]);
		edgeMap.get(0).get(2).add(nodes[2]);
		
		edgeMap.get(1).get(0).add(nodes[2]);
		edgeMap.get(1).get(0).add(nodes[4]);
		edgeMap.get(1).get(1).add(nodes[0]);
		edgeMap.get(1).get(1).add(nodes[3]);
		
		edgeMap.get(2).get(0).add(nodes[0]);
		edgeMap.get(2).get(0).add(nodes[1]);
		edgeMap.get(2).get(0).add(nodes[4]);
		edgeMap.get(2).get(2).add(nodes[0]);
		edgeMap.get(2).get(2).add(nodes[3]);
		
		edgeMap.get(3).get(0).add(nodes[0]);
		edgeMap.get(3).get(0).add(nodes[4]);
		edgeMap.get(3).get(1).add(nodes[0]);
		edgeMap.get(3).get(1).add(nodes[1]);
		edgeMap.get(3).get(2).add(nodes[2]);
		
		edgeMap.get(4).get(0).add(nodes[0]);
		edgeMap.get(4).get(0).add(nodes[1]);
		edgeMap.get(4).get(0).add(nodes[2]);
		edgeMap.get(4).get(0).add(nodes[3]);
		
		assertEquals(edgeMap.get(0).get(0), nodes[0].getEdges(TransportType.taxi));
		assertEquals(edgeMap.get(1).get(0), nodes[1].getEdges(TransportType.taxi));
		assertEquals(edgeMap.get(2).get(0), nodes[2].getEdges(TransportType.taxi));
		assertEquals(edgeMap.get(3).get(0), nodes[3].getEdges(TransportType.taxi));
		assertEquals(edgeMap.get(4).get(0), nodes[4].getEdges(TransportType.taxi));
		
		assertEquals(edgeMap.get(0).get(1), nodes[0].getEdges(TransportType.bus));
		assertEquals(edgeMap.get(1).get(1), nodes[1].getEdges(TransportType.bus));
		assertEquals(edgeMap.get(2).get(1), nodes[2].getEdges(TransportType.bus));
		assertEquals(edgeMap.get(3).get(1), nodes[3].getEdges(TransportType.bus));
		assertEquals(edgeMap.get(4).get(1), nodes[4].getEdges(TransportType.bus));
		
		assertEquals(edgeMap.get(0).get(2), nodes[0].getEdges(TransportType.underground));
		assertEquals(edgeMap.get(1).get(2), nodes[1].getEdges(TransportType.underground));
		assertEquals(edgeMap.get(2).get(2), nodes[2].getEdges(TransportType.underground));
		assertEquals(edgeMap.get(3).get(2), nodes[3].getEdges(TransportType.underground));
		assertEquals(edgeMap.get(4).get(2), nodes[4].getEdges(TransportType.underground));
	}
	
	@Test
	public void randomizedNodeTest(){
		int numNodes = 50;
		int numEdges = 200;
		Node[] nodes = new Node[numNodes];
		for(int i = 0; i < numNodes; i++){
			nodes[i] = new Node(i);
		}
		Map<Integer, List<Set<Node>>> edgeMap = initializeEdgeMap();
		
		int id1 = 0;	//id of nodes to connect
		int id2 = 0;
		int transport;
		for(int i = 0; i < numEdges; i++){
			while(id1 == id2){
				id1 = (int)(Math.random() * numNodes);
				id2 = (int)(Math.random() * numNodes);
			}
			transport = (int)(Math.random() * 3);
			if(transport == 0){
				nodes[id1].addEdge(nodes[id2], TransportType.taxi);
				nodes[id2].addEdge(nodes[id1], TransportType.taxi);
				edgeMap.get(id1).get(transport).add(nodes[id2]);
				edgeMap.get(id2).get(transport).add(nodes[id1]);
			}
			else if(transport == 1){
				nodes[id1].addEdge(nodes[id2], TransportType.bus);
				nodes[id2].addEdge(nodes[id1], TransportType.bus);
				edgeMap.get(id1).get(transport).add(nodes[id2]);
				edgeMap.get(id2).get(transport).add(nodes[id1]);
			}
			else if(transport == 2){
				nodes[id1].addEdge(nodes[id2], TransportType.underground);
				nodes[id2].addEdge(nodes[id1], TransportType.underground);
				edgeMap.get(id1).get(transport).add(nodes[id2]);
				edgeMap.get(id2).get(transport).add(nodes[id1]);
			}
			id1 = 0;
			id2 = 0;
		}
		
		for(int i = 0; i < numNodes; i++){
			assertEquals(edgeMap.get(i).get(0), nodes[i].getEdges(TransportType.taxi));
			assertEquals(edgeMap.get(i).get(1), nodes[i].getEdges(TransportType.bus));
			assertEquals(edgeMap.get(i).get(2), nodes[i].getEdges(TransportType.underground));
		}
	}
	
	@Test
	public void playerTest(){
		Node[] nodes = constructBasicBoard();
		Player mrX = new MrX("Mr. X", 0);
		Player detective = new Detective("Detective", 1);
		
		mrX.setLocation(nodes[0]);
		detective.setLocation(nodes[4]);
		
		assertEquals(nodes[0], mrX.getLocation());
		assertEquals(nodes[4], detective.getLocation());
		
		assertEquals(4, mrX.getTickets(TransportType.taxi));
		assertEquals(3, mrX.getTickets(TransportType.bus));
		assertEquals(3, mrX.getTickets(TransportType.underground));
		assertEquals(2, mrX.getTickets(TransportType.blackCard));
		
		assertEquals(10, detective.getTickets(TransportType.taxi));
		assertEquals(8, detective.getTickets(TransportType.bus));
		assertEquals(4, detective.getTickets(TransportType.underground));
		
		detective.move(nodes[2], TransportType.taxi);
		assertEquals(nodes[2], detective.getLocation());
		assertEquals(9, detective.getTickets(TransportType.taxi));
		
		detective.move(nodes[3], TransportType.underground);
		assertEquals(nodes[3], detective.getLocation());
		assertEquals(3, detective.getTickets(TransportType.underground));
		
		detective.move(nodes[1], TransportType.bus);
		assertEquals(nodes[1], detective.getLocation());
		assertEquals(7, detective.getTickets(TransportType.bus));
		
		mrX.move(nodes[4], TransportType.blackCard);
		assertEquals(nodes[4], mrX.getLocation());
		assertEquals(1, mrX.getTickets(TransportType.blackCard));
	}
	
	@Test
	public void basicGameLogicTest(){
		Node[] nodes = constructBasicBoard();
		Player mrX = new MrX("Mr. X", 0);
		Player detective = new Detective("Detective", 1);
		
		mrX.setLocation(nodes[0]);
		detective.setLocation(nodes[4]);
		
		assertTrue(GameLogic.canMove(mrX));
		assertTrue(GameLogic.canMove(detective));
		
		mrX.move(nodes[3], TransportType.taxi);
		mrX.move(nodes[0], TransportType.taxi);
		mrX.move(nodes[3], TransportType.taxi);
		mrX.move(nodes[0], TransportType.taxi);
		mrX.move(nodes[2], TransportType.underground);
		mrX.move(nodes[0], TransportType.underground);
		mrX.move(nodes[2], TransportType.underground);
		mrX.move(nodes[0], TransportType.blackCard);
		mrX.move(nodes[1], TransportType.blackCard);
		mrX.move(nodes[0], TransportType.bus);
		mrX.move(nodes[1], TransportType.bus);
		mrX.move(nodes[0], TransportType.bus);
		
		assertFalse(GameLogic.canMove(mrX));
		
		Player blockade1 = new Detective("Blockade 1", 2);
		Player blockade2 = new Detective("Blockade 2", 3);
		Player blockade3 = new Detective("Blockade 3", 4);
		
		blockade1.setLocation(nodes[1]);
		blockade2.setLocation(nodes[2]);
		blockade3.setLocation(nodes[3]);
		
		assertFalse(GameLogic.canMove(detective));
	}
	
	/**
	*	This test checks the 2 win cases:
	*		1) A detective finds Mr. X
	*		2) A detective can no longer move. Can happen by the following methods:
	*			a) All tickets have been exhausted
	*			b) There are no tickets to leave the detective's current location. (ie no taxi tickets on a node that only has taxi connections)
	*/
	@Test
	public void winLossTest(){
		Node[] nodes = constructBasicBoard();
		Player mrX = new MrX("Mr. X", 0);
		Player detective = new Detective("Detective", 1);
		Map<PlayerID, Player> players = new HashMap<PlayerID, Player>();
		players.put(new PlayerID(mrX.getID()), mrX);
		players.put(new PlayerID(detective.getID()), detective);
		
		mrX.setLocation(nodes[0]);
		detective.setLocation(nodes[4]);
		
		assertFalse(GameLogic.checkWin(players));
		
		detective.move(nodes[0], TransportType.taxi);
		
		assertTrue(GameLogic.checkWin(players));
		assertEquals(new PlayerID(1), GameLogic.getWinner());
		
		for(int i = 1; i < 10; i++){
			detective.move(nodes[4], TransportType.taxi);
		}
		
		assertTrue(GameLogic.checkWin(players));
		assertEquals(new PlayerID(0), GameLogic.getWinner());
		
		for(int i = 0; i < 8; i++){
			detective.move(nodes[3], TransportType.bus);
		}
		for(int i = 0; i < 4; i++){
			detective.move(nodes[3], TransportType.underground);
		}
		
		assertTrue(GameLogic.checkWin(players));
		assertEquals(new PlayerID(0), GameLogic.getWinner());
	}
	
	public Node[] constructBasicBoard(){
		Node[] nodes = new Node[5];
		for(int i = 0; i < nodes.length; i++){
			nodes[i] = new Node(i);
		}
		nodes[0].addEdge(nodes[2], TransportType.taxi);
		nodes[0].addEdge(nodes[3], TransportType.taxi);
		nodes[0].addEdge(nodes[4], TransportType.taxi);
		
		nodes[1].addEdge(nodes[2], TransportType.taxi);
		nodes[1].addEdge(nodes[4], TransportType.taxi);
		
		nodes[2].addEdge(nodes[0], TransportType.taxi);
		nodes[2].addEdge(nodes[1], TransportType.taxi);
		nodes[2].addEdge(nodes[4], TransportType.taxi);
		
		nodes[3].addEdge(nodes[0], TransportType.taxi);
		nodes[3].addEdge(nodes[4], TransportType.taxi);
		
		nodes[4].addEdge(nodes[0], TransportType.taxi);
		nodes[4].addEdge(nodes[1], TransportType.taxi);
		nodes[4].addEdge(nodes[2], TransportType.taxi);
		nodes[4].addEdge(nodes[3], TransportType.taxi);
		
		nodes[0].addEdge(nodes[1], TransportType.bus);
		nodes[1].addEdge(nodes[0], TransportType.bus);
		
		nodes[0].addEdge(nodes[3], TransportType.bus);
		nodes[3].addEdge(nodes[0], TransportType.bus);
		
		nodes[1].addEdge(nodes[3], TransportType.bus);
		nodes[3].addEdge(nodes[1], TransportType.bus);
		
		nodes[0].addEdge(nodes[2], TransportType.underground);
		nodes[2].addEdge(nodes[0], TransportType.underground);
		
		nodes[2].addEdge(nodes[3], TransportType.underground);
		nodes[3].addEdge(nodes[2], TransportType.underground);
		
		return nodes;
	}
	
	public Map<Integer, List<Set<Node>>> initializeEdgeMap(){
		Map<Integer, List<Set<Node>>> edgeMap = new HashMap<Integer, List<Set<Node>>>();
			for(int i = 0; i < nodes.length; i++){
				Set<Node> edgeSet1 = new HashSet<Node>();
				Set<Node> edgeSet2 = new HashSet<Node>();
				Set<Node> edgeSet3 = new HashSet<Node>();
				List<Set<Node>> edgeList = new ArrayList<Set<Node>>();
				edgeList.add(edgeSet1);		//taxi
				edgeList.add(edgeSet2);		//bus
				edgeList.add(edgeSet3);		//underground
				edgeMap.put(i, edgeList);
			}
		return edgeMap;
	}
	
	public static void main(String[] args) {
    	Result result = JUnitCore.runClasses(ScotlandYardTest.class);
    	System.out.println("---------------");
    	for (Failure failure : result.getFailures()) {
      		System.out.println(failure.toString());
            System.out.println(failure.getTrace());
      	}
      	if (result.getFailures().size() == 0) {
      		System.out.println("All tests passed!!");
      	} else {
            System.out.println("SOME TESTS FAILED :(");
        }
    }
}