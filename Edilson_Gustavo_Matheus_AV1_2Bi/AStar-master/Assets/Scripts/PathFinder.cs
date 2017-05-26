using System.Collections.Generic;
using UnityEngine;

public static class PathFinder
{
	public static List<Grid.Position> FindPath( Tile[,] tiles, Grid.Position _fromPosition, Grid.Position _toPosition )
	{
		PathNode n = BreadthFirstSearch( tiles, _fromPosition, _toPosition );

		 var p = new List<Grid.Position>();

		//Adicionando na Lista Encadeada.
		while (n.anterior != null) {
			p.Add (n.position);
			n = n.anterior;
			p.Add (n.position); 
		
		} 

		p.Reverse();

		return p;
	}

	private class PathNode
	{
		public Grid.Position position;
		public PathNode anterior;

		public PathNode( Grid.Position position, PathNode aterior )
		{
			this.position = position;
			this.anterior = aterior;
		}
	}

	private static PathNode BreadthFirstSearch( Tile[,] tiles, Grid.Position _fromPosition, Grid.Position _toPosition )
	{

		var q = new Queue<PathNode>();

		var root = new PathNode( _fromPosition, null );


		q.Enqueue( root );

		while( q.Count > 0 )
		{
			PathNode node = q.Dequeue();
			if( node.position.x == _toPosition.x && node.position.y == _toPosition.y )
			{
				Debug.Log ("AChEI");
				return node;
			}
			else
			{
				TryEnqueueNode( tiles, q, node, new Grid.Position( node.position.x + 1, node.position.y ) );
				TryEnqueueNode( tiles, q, node, new Grid.Position( node.position.x - 1, node.position.y ) );
				TryEnqueueNode( tiles, q, node, new Grid.Position( node.position.x, node.position.y + 1 ) );
				TryEnqueueNode( tiles, q, node, new Grid.Position( node.position.x, node.position.y - 1 ) );
			}
		}

		return null;
	}

	private static void TryEnqueueNode( Tile[,] tiles, Queue<PathNode> queue, 
		PathNode nodeAterior, Grid.Position newPosition )
	{
		//Limitando as posições do Grid 
		if (newPosition.x < 5 && newPosition.x >= 0 && newPosition.y < 5 && newPosition.y >= 0) {
			
			if (!tiles [newPosition.x, newPosition.y].isWall) {
				queue.Enqueue (new PathNode (newPosition, nodeAterior));
				Debug.Log ("vida");
			}
		}

	}

}

