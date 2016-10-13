using UnityEngine;
using System.Collections.Generic;
using System;



public class Tuple<X,Y>
{
	public X first;
	public Y second;

	public Tuple(X first, Y second)
	{
		this.first = first;
		this.second = second;
	}
}

public class GameBoard : MonoBehaviour {

	GameObject[,] board = new GameObject[10, 10];
	System.Random rand = new System.Random();

	void createNodeAt(int x, int z)
	{
		// load the prefab "NodeCube" and create one on the board
		board [x, z] = Instantiate(Resources.Load("NodeCube")) as GameObject;
		board [x, z].transform.position = new Vector3 (x-5,0,z-5);
	}

	Tuple<int,int> genRandCoord(System.Random rand)
	{
		return new Tuple<int,int> (rand.Next (0, 10), rand.Next (0, 10));
	}

	void createNewRandomNode()
	{
		var newCoord = genRandCoord (rand);

		while (board [newCoord.first, newCoord.second] != null) {
			newCoord = genRandCoord (rand);
		}

		createNodeAt (newCoord.first, newCoord.second);
	}

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 10; i++) {
			createNewRandomNode ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
