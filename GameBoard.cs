﻿using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

using UnityEngine.UI;

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


public class GameLinkedList
{
    public GameNode first;
    public GameNode last;

    public GameNode find(GameObject cubeToFind, List<GameNode> otherNodes)
    {
        var searchNode = first;

        while (searchNode != null && searchNode.value != cubeToFind)
            searchNode = searchNode.next;

        if(searchNode == null)
            searchNode = otherNodes.Where(n => n.value == cubeToFind)
                                   .FirstOrDefault();

        return searchNode;
    }
}


public class GameBoard : MonoBehaviour {

    private const int numStartingNodes = 10;
	private GameObject[,] board = new GameObject[10, 10];
    private System.Random rand = new System.Random();

    private static List<GameNode> newElements = new List<GameNode>();

    public GameObject nullCube;
    public NodePointer nullPointer;
    public NodePointer currentPointer;
    public NodePointer nextPointer;
    public NodePointer nextNextPointer;

    private static GameLinkedList nodes = new GameLinkedList();
    public static GameNode drawCube = null;

    // Use this for initialization
    void Start()
    {
        genNodes();

        currentPointer.pointToAndActivate(nodes.first);

        nextPointer.pointTo(currentPointer.node.next);

        nextNextPointer.pointTo(currentPointer.node.next.next);

        nullPointer.pointTo(nullCube.GetComponent<GameNode>());

    }

    // Update is called once per frame
    void Update()
    {
        currentPointer.setNodeActive(true);
        if (nextPointer.isActive) nextPointer.setNodeActive(true);
        if (nextNextPointer.isActive) nextNextPointer.setNodeActive(true);
    }

    public static void lineDrawing(GameNode selectedCube)
    {
        Debug.Log("Clicked");

        if (selectedCube == null) return;

        //Debug.Log("Selected Something!");
        if (drawCube == null)
        {
            Debug.Log("Drawing cube selected!");
            drawCube = selectedCube.GetComponent<GameNode>();
        }
        else if (drawCube == selectedCube)
        {
            Debug.Log("Drawing Cube deselected!");
            drawCube = null;
        }
        else if (drawCube != selectedCube && drawCube != null)
        {
            var lineNode = nodes.find(drawCube.gameObject, newElements);
            var selectedNode = nodes.find(selectedCube.transform.gameObject, newElements);

            if (lineNode != null && selectedNode != null)
            {
                Debug.Log("Nodes connected!");
                lineNode.next = selectedNode;
            }

            drawCube = null;
        }

    }

    GameObject createNodeAt(int val, int x, int z)
	{
		// load the prefab "NodeCube" and create one on the board
		board [x, z] = Instantiate(Resources.Load("NodeCube")) as GameObject;
		board [x, z].transform.position = new Vector3 (x-5,0,z-5);

        var nodeText = board[x, z].GetComponentInChildren<Text>() as Text;

        nodeText.text = val.ToString();

        board[x, z].SetActive(false);

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

		return createNodeAt(rand.Next(0,20), newCoord.first, newCoord.second);
	}

    private void genNodes()
    {
        for (int i = 0; i < numStartingNodes; i++)
        {
            if (nodes.first == null)
            {
                var newNode = createNewRandomNode();
                nodes.first = newNode.GetComponent<GameNode>();
                nodes.last = nodes.first;
            }
            else
            {
                nodes.last.next = createNewRandomNode().GetComponent<GameNode>();
                nodes.last = nodes.last.next;
            }
        }
    }

    public void addNewNode()
    {
        var node = createNewRandomNode().GetComponent<GameNode>();

        node.value.SetActive(true);
        node.newElementSound.Play();
        nodes.last = node;
        newElements.Add(node);
    }

    private void moveToNull()
    {
        currentPointer.togglePointer();
        nullPointer.toggleNode();
    }

    public void moveCurrentPointer()
    {
        if (currentPointer.node.next == null)
        {
            moveToNull();
        }
        else
        {
            currentPointer.pointToAndActivate(currentPointer.node.next);
            currentPointer.sound.Play();
        }
    }

    public void toggleNextPointer()
    {
        if(currentPointer.node.next == null)
        {
            nullPointer.togglePointer();
        }
        else
        {
            nextPointer.pointTo(currentPointer.node.next);
            nextPointer.togglePointer();
            nextPointer.sound.Play();
        }
    }

    public void toggleNextNextPointer()
    {
        if(currentPointer.node.next == null || currentPointer.node.next.next == null)
        {
            nullPointer.togglePointer();
        }
        else
        {
            nextNextPointer.pointTo(currentPointer.node.next.next);
            nextNextPointer.togglePointer();
            nextNextPointer.sound.Play();
        }
    }
}
