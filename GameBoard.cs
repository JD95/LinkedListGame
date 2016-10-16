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
    List<GameObject> nodes = new List<GameObject>();
	System.Random rand = new System.Random();

    public GameObject newElementLight;
    public GameObject currentPointerLight;
    public GameObject nextPointerLight;
    public GameObject nextNextPointerLight;

    public AudioSource newElementSound;
    public AudioSource currentPointerSound;

    private int currentNode = 0;

    GameObject createNodeAt(int x, int z)
	{
		// load the prefab "NodeCube" and create one on the board
		board [x, z] = Instantiate(Resources.Load("NodeCube")) as GameObject;
		board [x, z].transform.position = new Vector3 (x-5,0,z-5);

        nodes.Add(board[x, z]);

        return board[x, z];
	}

	Tuple<int,int> genRandCoord(System.Random rand)
	{
		return new Tuple<int,int> (rand.Next (0, 10), rand.Next (0, 10));
	}

    GameObject createNewRandomNode()
	{
		var newCoord = genRandCoord (rand);

		while (board [newCoord.first, newCoord.second] != null) {
			newCoord = genRandCoord (rand);
		}

		return createNodeAt (newCoord.first, newCoord.second);
	}

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 10; i++) {
			createNewRandomNode ();
		}

        moveLight(currentPointerLight, nodes[0].transform.position);

        nextPointerLight.SetActive(false);
        nextNextPointerLight.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void moveLight(GameObject light, Vector3 position)
    {
        light.transform.position = new Vector3(position.x, 5.55f, position.z);
    }

    public void addNewNode()
    {
        var node = createNewRandomNode();
        moveLight(newElementLight, node.transform.position);
        newElementSound.Play();
    }

    public void moveCurrentPointer()
    {
        currentNode += currentNode < nodes.Count - 1 ? 1 : 0;
        moveLight(currentPointerLight, nodes[currentNode].transform.position);
        currentPointerLight.SetActive(true);
        currentPointerSound.Play();
    }
}
