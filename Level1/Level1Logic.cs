using UnityEngine;
using System.Collections;
using System;

public class Level1Logic : WinCondition {

    public GameBoard board;

    public GameObject firstNode;

    public GameObject[] groupOfNodes = new GameObject[3];

    public override bool canProgress()
    {
        return board.emptyBoard();
    }

    public override void progress()
    {
        Debug.Log("Delete the rest of the nodes!");
        firstNode.SetActive(false);
        firstNode.GetComponent<GameNode>().deleteNode();
        foreach (var node in groupOfNodes)
        {
            node.SetActive(true);
        }
    }

    public override bool win()
    {
        foreach (var node in groupOfNodes)
        {
            if (node != null) return false;
        }

        return true;
    }

    // Use this for initialization
    void Start () {
        Debug.Log("Please delete the node!");

        board.board[0, 0] = firstNode;
        board.board[0, 0].GetComponent<GameNode>().popupText = board.GetComponent<AddPopupText>();

        for (int i = 0; i < 3; i++)
        {
            board.board[0, i + 1] = groupOfNodes[i];
            board.board[0, i + 1].GetComponent<GameNode>().popupText = board.GetComponent<AddPopupText>();
        }

        firstNode.SetActive(true);
        foreach(var node in groupOfNodes)
        {
            node.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
