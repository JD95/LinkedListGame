using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class Board
{
    public GameObject[,] spaces = new GameObject[10, 10];
	public GameBoard game;

    public Board()
    {

    }

	public Board(GameBoard game){
		this.game = game;
	}

    public bool emptyBoard()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (spaces[i, j] != null) return false;
            }
        }

        return true;
    }

	bool leak(GameObject g, GameLinkedList nodes)
    {

        bool test = !(g == null 
             || g.GetComponent<GameNode>().deleted
             || g.activeSelf
             || !g.activeSelf && nodes.find(g, new List<GameNode>()) != null
             || game.currentPointer.node != null && game.currentPointer.node.value == g
             || game.nextPointer.node != null && game.nextPointer.node.value == g
             || game.nextNextPointer.node != null && game.nextNextPointer.node.value == g);

        if (test) Debug.Log(g.GetComponent<GameNode>().nodeValue + " is a leak!");
        return test;
             
    }

	public bool noLeaks(GameLinkedList nodes)
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (leak(spaces[i, j], nodes)) return false;
            }
        }

        return true;
    }
}

