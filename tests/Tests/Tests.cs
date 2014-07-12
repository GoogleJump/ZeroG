using System;
using NUnit.Framework;
using System.Collections.Generic;


namespace Game
{
	[TestFixture]
	public class GameLogicTests
	{
		[Test]
		public void basicNodeTest(){
			Node[] nodes = constructBasicBoard();
			Dictionary<int, List<HashSet<Node>>> edgeMap = initializeEdgeMap(nodes);

			edgeMap[0][0].Add(nodes[2]);
			edgeMap[0][0].Add(nodes[3]);
			edgeMap[0][0].Add(nodes[4]);
			edgeMap[0][1].Add(nodes[1]);
			edgeMap[0][1].Add(nodes[3]);
			edgeMap[0][2].Add(nodes[2]);

			edgeMap[1][0].Add(nodes[2]);
			edgeMap[1][0].Add(nodes[4]);
			edgeMap[1][1].Add(nodes[0]);
			edgeMap[1][1].Add(nodes[3]);

			edgeMap[2][0].Add(nodes[0]);
			edgeMap[2][0].Add(nodes[1]);
			edgeMap[2][0].Add(nodes[4]);
			edgeMap[2][2].Add(nodes[0]);
			edgeMap[2][2].Add(nodes[3]);

			edgeMap[3][0].Add(nodes[0]);
			edgeMap[3][0].Add(nodes[4]);
			edgeMap[3][1].Add(nodes[0]);
			edgeMap[3][1].Add(nodes[1]);
			edgeMap[3][2].Add(nodes[2]);

			edgeMap[4][0].Add(nodes[0]);
			edgeMap[4][0].Add(nodes[1]);
			edgeMap[4][0].Add(nodes[2]);
			edgeMap[4][0].Add(nodes[3]);

			Assert.AreEqual(edgeMap[0][0], nodes[0].getEdges(TransportType.taxi));
			Assert.AreEqual(edgeMap[1][0], nodes[1].getEdges(TransportType.taxi));
			Assert.AreEqual(edgeMap[2][0], nodes[2].getEdges(TransportType.taxi));
			Assert.AreEqual(edgeMap[3][0], nodes[3].getEdges(TransportType.taxi));
			Assert.AreEqual(edgeMap[4][0], nodes[4].getEdges(TransportType.taxi));

			Assert.AreEqual(edgeMap[0][1], nodes[0].getEdges(TransportType.bus));
			Assert.AreEqual(edgeMap[1][1], nodes[1].getEdges(TransportType.bus));
			Assert.AreEqual(edgeMap[2][1], nodes[2].getEdges(TransportType.bus));
			Assert.AreEqual(edgeMap[3][1], nodes[3].getEdges(TransportType.bus));
			Assert.AreEqual(edgeMap[4][1], nodes[4].getEdges(TransportType.bus));

			Assert.AreEqual(edgeMap[0][2], nodes[0].getEdges(TransportType.underground));
			Assert.AreEqual(edgeMap[1][2], nodes[1].getEdges(TransportType.underground));
			Assert.AreEqual(edgeMap[2][2], nodes[2].getEdges(TransportType.underground));
			Assert.AreEqual(edgeMap[3][2], nodes[3].getEdges(TransportType.underground));
			Assert.AreEqual(edgeMap[4][2], nodes[4].getEdges(TransportType.underground));
		}

		[Test]
		public void randomizedNodeTest(){
			Random randomGenerator = new Random ();
			int numNodes = 50;
			int numEdges = 200;
			Node[] nodes = new Node[numNodes];
			for(int i = 0; i < numNodes; i++){
				nodes[i] = new Node(i);
			}
			Dictionary<int, List<HashSet<Node>>> edgeMap = initializeEdgeMap(nodes);

			int id1 = 0;	//id of nodes to connect
			int id2 = 0;
			int transport;
			for(int i = 0; i < numEdges; i++){
				while(id1 == id2){
					id1 = (int)(randomGenerator.NextDouble() * numNodes);
					id2 = (int)(randomGenerator.NextDouble() * numNodes);
				}
				transport = (int)(randomGenerator.NextDouble() * 3);
				if(transport == 0){
					nodes[id1].addEdge(nodes[id2], TransportType.taxi);
					nodes[id2].addEdge(nodes[id1], TransportType.taxi);
					edgeMap[id1][transport].Add(nodes[id2]);
					edgeMap[id2][transport].Add(nodes[id1]);
				}
				else if(transport == 1){
					nodes[id1].addEdge(nodes[id2], TransportType.bus);
					nodes[id2].addEdge(nodes[id1], TransportType.bus);
					edgeMap[id1][transport].Add(nodes[id2]);
					edgeMap[id2][transport].Add(nodes[id1]);
				}
				else if(transport == 2){
					nodes[id1].addEdge(nodes[id2], TransportType.underground);
					nodes[id2].addEdge(nodes[id1], TransportType.underground);
					edgeMap[id1][transport].Add(nodes[id2]);
					edgeMap[id2][transport].Add(nodes[id1]);
				}
				id1 = 0;
				id2 = 0;
			}

			for(int i = 0; i < numNodes; i++){
				Assert.AreEqual(edgeMap[i][0], nodes[i].getEdges(TransportType.taxi));
				Assert.AreEqual(edgeMap[i][1], nodes[i].getEdges(TransportType.bus));
				Assert.AreEqual(edgeMap[i][2], nodes[i].getEdges(TransportType.underground));
			}
		}

		[Test]
		public void playerTest(){
			Node[] nodes = constructBasicBoard();
			Player mrX = new MrX("Mr. X", 0);
			Player detective = new Detective("Detective", 1);

			mrX.setLocation(nodes[0]);
			detective.setLocation(nodes[4]);

			Assert.AreEqual(nodes[0], mrX.getLocation());
			Assert.AreEqual(nodes[4], detective.getLocation());

			Assert.AreEqual(4, mrX.getTickets(TransportType.taxi));
			Assert.AreEqual(3, mrX.getTickets(TransportType.bus));
			Assert.AreEqual(3, mrX.getTickets(TransportType.underground));
			Assert.AreEqual(2, mrX.getTickets(TransportType.blackCard));

			Assert.AreEqual(10, detective.getTickets(TransportType.taxi));
			Assert.AreEqual(8, detective.getTickets(TransportType.bus));
			Assert.AreEqual(4, detective.getTickets(TransportType.underground));

			detective.move(nodes[2], TransportType.taxi);
			Assert.AreEqual(nodes[2], detective.getLocation());
			Assert.AreEqual(9, detective.getTickets(TransportType.taxi));

			detective.move(nodes[3], TransportType.underground);
			Assert.AreEqual(nodes[3], detective.getLocation());
			Assert.AreEqual(3, detective.getTickets(TransportType.underground));

			detective.move(nodes[1], TransportType.bus);
			Assert.AreEqual(nodes[1], detective.getLocation());
			Assert.AreEqual(7, detective.getTickets(TransportType.bus));

			mrX.move(nodes[4], TransportType.blackCard);
			Assert.AreEqual(nodes[4], mrX.getLocation());
			Assert.AreEqual(1, mrX.getTickets(TransportType.blackCard));
		}

		[Test]
		public void basicGameLogicTest(){
			Node[] nodes = constructBasicBoard();
			Player mrX = new MrX("Mr. X", 0);
			Player detective = new Detective("Detective", 1);

			mrX.setLocation(nodes[0]);
			detective.setLocation(nodes[4]);

			Assert.True(GameLogic.canMove(mrX));
			Assert.True(GameLogic.canMove(detective));

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

			Assert.False(GameLogic.canMove(mrX));

			Player blockade1 = new Detective("Blockade 1", 2);
			Player blockade2 = new Detective("Blockade 2", 3);
			Player blockade3 = new Detective("Blockade 3", 4);

			blockade1.setLocation(nodes[1]);
			blockade2.setLocation(nodes[2]);
			blockade3.setLocation(nodes[3]);

			Assert.True(GameLogic.canMove(detective));
		}

		[Test]
		public void winLossTest(){
			Node[] nodes = constructBasicBoard();
			Player mrX = new MrX("Mr. X", 0);
			Player detective = new Detective("Detective", 1);
			Dictionary<int, Player> players = new Dictionary<int, Player>();
			players.Add(mrX.getId(), mrX);
			players.Add(detective.getId(), detective);

			mrX.setLocation(nodes[0]);
			detective.setLocation(nodes[4]);

			int dummyWinningPlayerId;
			Assert.False(GameLogic.checkWin(players, out dummyWinningPlayerId));

			detective.move(nodes[0], TransportType.taxi);

			int firstWinningPlayerId;
			bool gameWon = GameLogic.checkWin (players, out  firstWinningPlayerId);

			Assert.True(gameWon);
			Assert.AreEqual(1, firstWinningPlayerId);

			for(int i = 1; i < 10; i++){
				detective.move(nodes[4], TransportType.taxi);
			}

			int secondWinningPlayerId;
			gameWon = GameLogic.checkWin (players, out secondWinningPlayerId);

			Assert.True(gameWon);
			Assert.AreEqual(0, secondWinningPlayerId);

			for(int i = 0; i < 8; i++){
				detective.move(nodes[3], TransportType.bus);
			}
			for(int i = 0; i < 4; i++){
				detective.move(nodes[3], TransportType.underground);
			}

			int thirdWinningPlayerId;
			gameWon = GameLogic.checkWin (players, out thirdWinningPlayerId);
			Assert.True(gameWon);
			Assert.AreEqual(0, thirdWinningPlayerId);
		}

		public Node[] constructBasicBoard(){
			Node[] nodes = new Node[5];
			for(int i = 0; i < nodes.GetLength(0); i++){
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

		public Dictionary<int, List<HashSet<Node>>> initializeEdgeMap(Node[] nodes){
			Dictionary<int, List<HashSet<Node>>> edgeMap = new Dictionary<int, List<HashSet<Node>>>();
			for(int i = 0; i < nodes.GetLength(0); i++){
				HashSet<Node> edgeSet1 = new HashSet<Node>();
				HashSet<Node> edgeSet2 = new HashSet<Node>();
				HashSet<Node> edgeSet3 = new HashSet<Node>();
				List<HashSet<Node>> edgeList = new List<HashSet<Node>>();
				edgeList.Add(edgeSet1);	//taxi
				edgeList.Add(edgeSet2);	//bus
				edgeList.Add(edgeSet3);	//underground
				edgeMap.Add(i, edgeList);
			}
			return edgeMap;
		}

	}
}

namespace UnitTestDemo
{
	public class Game
	{
		//needed for class to compile
		public static void Main()
		{
		}
	}
}